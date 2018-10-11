using System;
using System.Globalization;
using scrimp.Domain;
using scrimp.Dtos;
using scrimp.Entities;
using scrimp.Helpers;

namespace scrimp.Services
{
    public class ApiErrorService : IErrorService
    {
        private DataContext _context;
        private readonly TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;

        public ApiErrorService(DataContext context)
        {
            _context = context;
        }

        public ErrorList BadRequest(string entity, int identifier)
        {
            entity = _textInfo.ToTitleCase(entity);

            var error = new Error
            {
                Id = Guid.NewGuid(),
                Status = HttpStatus.BadRequest,
                Code = HttpStatus.BadRequest.ToString(),
                Title = "Bad Request",
                Detail = $"The {entity} identified by id {identifier} is not valid."
            };

            _context.Errors.Add(error);
            _context.SaveChanges();

            return error.ToErrorList();
        }

        public ErrorList BadRequest(AppException appException)
        {
            var error = new Error
            {
                Id = Guid.NewGuid(),
                Status = HttpStatus.BadRequest,
                Code = HttpStatus.BadRequest.ToString(),
                Title = "Bad Request",
                Detail = appException.Message,
                InnerException = appException
            };

            _context.Errors.Add(error);
            _context.SaveChanges();

            return error.ToErrorList();
        }

        public ErrorList NotFound(string entity, int identifier)
        {
            entity = _textInfo.ToTitleCase(entity);

            var error = new Error
            {
                Id = Guid.NewGuid(),
                Status = HttpStatus.NotFound,
                Code = HttpStatus.NotFound.ToString(),
                Title = $"{entity} Not Found",
                Detail = $"The {entity} identified by id {identifier} was not found."
            };

            _context.Errors.Add(error);
            _context.SaveChanges();

            return error.ToErrorList();
        }
    }
}
