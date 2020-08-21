using System;

namespace CustomNetCoreIdentity.Domain.Entities.Shared
{
    /// <summary>
    /// Rather than adding date info on each entity, create a base class to use
    /// </summary>
    public class BaseEntity
    {
        public DateTime Created { get; set; }
    }
}
