using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;

        }
        
        public Expression<Func<T, bool>> Criteria
        {
            get; 
            set;
        }

        public List<Expression<Func<T, object>>> Includes
        {
            get;
        }  = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExp)
        {
            OrderBy = orderByExp;
        }
        
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByExpDes)
        {
            OrderByDescending = orderByExpDes;
        }
    }
}