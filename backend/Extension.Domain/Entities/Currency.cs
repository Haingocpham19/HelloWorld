using Extension.Domain.Entities.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Extension.Domain.Entities
{
    public class Currency : FullAuditedEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
