using Extension.Domain.Entities.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Extension.Domain.Entities
{
    public class Product : FullAuditedEntity<int>
    {
        [Key]
        public long Id { get; set; }
        public string ProductTitle { get; set; }
        public string Url { get; set; }
        public string ProductImageSrc { get; set; }
        public string Availability { get; set; }
        public string Price { get; set; }
        public decimal? LastPrice { get; set; }
        public bool? Status { get; set; }
        public int SourcePageId { get; set; }
        public SourcePage SourcePage { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        [ForeignKey("ClientCard")]
        public Guid ClientCardId { get; set; }
        public ClientCard ClientCard { get; set; }
    }
}
