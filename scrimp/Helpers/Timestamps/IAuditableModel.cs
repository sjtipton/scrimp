using System;

namespace scrimp.Helpers.Timestamps
{
    public interface IAuditableModel
    {
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; set; }
    }
}
