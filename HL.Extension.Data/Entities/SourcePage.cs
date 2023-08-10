using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Extension.Domain.Entities
{
    public class SourcePage
    {
        [Key]
        public int SourcePageId { get; set; }
        public string PageName { get; set; }
        public string Domain { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
