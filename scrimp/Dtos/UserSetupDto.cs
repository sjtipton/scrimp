using scrimp.Domain;
using System;

namespace scrimp.Dtos
{
    public class UserSetupDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public Guid GreenlitApiId { get; set; }
        public string Timezone { get; set; }
        public Weekday WeekStartDay { get; set; }
        public string CurrencyCode { get; set; }
        public string AuthToken { get; set; }
    }
}
