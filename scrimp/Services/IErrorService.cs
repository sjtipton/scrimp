using Microsoft.AspNetCore.Http;
using scrimp.Dtos;
using scrimp.Helpers;

namespace scrimp.Services
{
    public interface IErrorService
    {
        ErrorList NotFound(string entity, int identifier, HttpRequest request);
        ErrorList BadRequest(string entity, int identifier, HttpRequest request);
        ErrorList BadRequest(AppException appException, HttpRequest request);
    }
}
