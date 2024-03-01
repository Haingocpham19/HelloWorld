using System;

namespace Extension.Domain.Entities.Core
{
    [Serializable]
    public abstract class CreationAuditedEntity<TPrimaryKey>
    {
        //
        // Summary:
        //     Creation time of this entity.
        public virtual DateTime CreationTime { get; set; }

        //
        // Summary:
        //     Creator of this entity.
        public virtual long? CreatorUserId { get; set; }

        //
        // Summary:
        //     Constructor.
        protected CreationAuditedEntity()
        {
            CreationTime = DateTime.Now;
        }
    }
}
