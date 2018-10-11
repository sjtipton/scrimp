﻿using scrimp.Domain;
using scrimp.Dtos;
using System;
using System.Collections.Generic;

namespace scrimp.Helpers
{
    public static class ApiErrors
    {
        public static ErrorList GetErrors(HttpStatus status, string title, string detail, Exception innerException)
        {
            var error = new Error {
                Id = Guid.NewGuid(),
                Status = status,
                Code = status.ToString(),
                Title = !string.IsNullOrEmpty(title) ? title : "Bad Request",
                Detail = !string.IsNullOrEmpty(detail) ? detail : "Bad Request",
                InnerException = innerException
            };

            // TODO send to constructor?
            return new ErrorList {
                Errors = new List<Error> { error }
            };
        }
    }
}