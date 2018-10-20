using System;
using System.Threading.Tasks;

namespace scrimp.Services
{
    public interface IRestApiClient<T>
    {
        Task<T> GetRestApiEntity(Guid id, string authToken);
    }
}
