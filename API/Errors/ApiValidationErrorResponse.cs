﻿using System.Collections.Generic;

namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse(int statusCode, string message = null) : base(statusCode, message)
        {
        }

        public ApiValidationErrorResponse() : base(400)
        {

        }

        public IEnumerable<string> Errors { get; set; }
    }
}