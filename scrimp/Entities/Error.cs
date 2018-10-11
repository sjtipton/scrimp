using scrimp.Domain;
using System;

namespace scrimp.Entities
{
    public class Error
    {
        public Guid Id { get; set; }
        public HttpStatus Status { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public Exception InnerException { get; set; }
    }
}
