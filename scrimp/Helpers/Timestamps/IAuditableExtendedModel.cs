using System;

namespace scrimp.Helpers.Timestamps
{
    public interface IAuditableExtendedModel : IAuditableModel
    {
        DateTime? LastLoggedInAt { get; set; }
        DateTime? LastActivityAt { get; set; }
    }
}
