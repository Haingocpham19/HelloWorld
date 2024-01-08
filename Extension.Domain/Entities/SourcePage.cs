using Extension.Domain.Entities.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Extension.Domain.Entities
{
    public class SourcePage:FullAuditedEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string PageName { get; set; }
        public string Domain { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
