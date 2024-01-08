using System;

namespace Extension.Domain.Entities.Core
{
    [Serializable]
    public abstract class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>
    {
        //
        // Summary:
        //     Last modification date of this entity.
        public virtual DateTime? LastModificationTime { get; set; }

        //
        // Summary:
        //     Last modifier user of this entity.
        public virtual long? LastModifierUserId { get; set; }
    }
}
