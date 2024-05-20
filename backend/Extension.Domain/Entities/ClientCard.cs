using Extension.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Extension.Domain.Entities
{
    public class ClientCard : FullAuditedEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
   
        public string Status { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
