﻿using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<ProductBrand> brandRepo;
        private readonly IGenericRepository<ProductType> typeRepo;
        private readonly IMapper mapper;
        private IGenericRepository<Product> productRepo;

        public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> brandRepo, 
            IGenericRepository<ProductType> typeRepo, IMapper mapper)
        {
            this.brandRepo = brandRepo; 
            this.typeRepo = typeRepo;
            this.mapper = mapper;
            this.productRepo= productRepo;
        }
        
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturn>>> GetProducts([FromQuery]ProductSpecificationParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await productRepo.CountAsync(countSpec);
            
            var products = await productRepo.ListAsync(spec);

            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturn>>(products);
            
            return Ok(new Pagination<ProductToReturn>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturn>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product =  await productRepo.GetEntityWithSpec(spec);

            if (product == null)
                return NotFound(new ApiResponse(404));

            return mapper.Map<Product, ProductToReturn>(product);
        }
        
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await brandRepo.GetListAllAsync());
        }
        
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await typeRepo.GetListAllAsync());
        }
    }
}
