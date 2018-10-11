using scrimp.Entities;
using System.Collections.Generic;

namespace scrimp.Dtos
{
    public class ErrorList
    {
        public IEnumerable<Error> Errors { get; set; }
    }
}
