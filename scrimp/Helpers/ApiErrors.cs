using scrimp.Dtos;
using scrimp.Entities;
using System.Collections.Generic;

namespace scrimp.Helpers
{
    public static class ApiErrorExtensions
    {
        public static ErrorList ToErrorList(this IEnumerable<Error> errors) => new ErrorList { Errors = errors };
        public static ErrorList ToErrorList(this Error error) => new ErrorList { Errors = new List<Error> { error } };
    }
}
