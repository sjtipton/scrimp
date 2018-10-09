namespace scrimp.Domain
{
    public enum HttpStatus
    {
        // Common usages
        Ok = 200,
        Created = 201,
        NoContent = 204,
        MovedPermanently = 301,
        Found = 302,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Gone = 410,
        InternalServerError = 500,
        NotImplemented = 501,
        ServiceUnavailable = 503
    }
}
