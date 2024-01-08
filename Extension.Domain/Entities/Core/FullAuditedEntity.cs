using System;

namespace Extension.Domain.Entities.Core
{
    // Type parameters:
    //   TPrimaryKey:
    //     Type of the primary key of the entity
    [Serializable]
    public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>
    {
        //
        // Summary:
        //     Is this entity Deleted?
        public virtual bool IsDeleted { get; set; }

        //
        // Summary:
        //     Which user deleted this entity?
        public virtual long? DeleterUserId { get; set; }

        //
        // Summary:
        //     Deletion time of this entity.
        public virtual DateTime? DeletionTime { get; set; }
    }
}
