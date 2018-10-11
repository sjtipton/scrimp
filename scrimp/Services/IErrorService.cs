using scrimp.Dtos;
using scrimp.Helpers;

namespace scrimp.Services
{
    public interface IErrorService
    {
        ErrorList NotFound(string entity, int identifier);
        ErrorList BadRequest(string entity, int identifier);
        ErrorList BadRequest(AppException appException);
    }
}
