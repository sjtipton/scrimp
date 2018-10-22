using scrimp.Domain;
using scrimp.Helpers.Timestamps;
using System;

namespace scrimp.Entities
{
    public class User : IAuditableExtendedModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public Guid GreenlitApiId { get; set; }
        public string Timezone { get; set; }
        public Weekday WeekStartDay { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLoggedInAt { get; set; }
        public DateTime? LastActivityAt { get; set; }
    }
}
