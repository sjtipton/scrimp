using scrimp.Domain;
using scrimp.Dtos;
using scrimp.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace scrimp.Helpers
{
    // TODO move to Service pattern and save to DB
    public static class ApiErrors
    {
        private static ErrorList _errorList = new ErrorList();
        private static readonly TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;

        public static ErrorList NotFound(string entity, int identifier)
        {
            entity = _textInfo.ToTitleCase(entity);

            _errorList.Errors = new List<Error> {
                new Error
                {
                    Id = Guid.NewGuid(),
                    Status = HttpStatus.NotFound,
                    Code = HttpStatus.NotFound.ToString(),
                    Title = $"{entity} Not Found",
                    Detail = $"The {entity} identified by id {identifier} was not found."
                }
            };

            return _errorList;
        }

        public static ErrorList BadRequest(string entity, int identifier)
        {
            entity = _textInfo.ToTitleCase(entity);

            _errorList.Errors = new List<Error> {
                new Error
                {
                    Id = Guid.NewGuid(),
                    Status = HttpStatus.BadRequest,
                    Code = HttpStatus.BadRequest.ToString(),
                    Title = $"Bad Request",
                    Detail = $"The {entity} identified by id {identifier} is not valid."
                }
            };

            return _errorList;
        }

        public static ErrorList BadRequest(Exception innerException)
        {
            _errorList.Errors = new List<Error> {
                new Error
                {
                    Id = Guid.NewGuid(),
                    Status = HttpStatus.BadRequest,
                    Code = HttpStatus.BadRequest.ToString(),
                    Title = $"Bad Request",
                    Detail = innerException.Message,
                    InnerException = innerException
                }
            };

            return _errorList;
        }
    }
}
