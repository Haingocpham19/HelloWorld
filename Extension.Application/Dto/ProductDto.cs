using AutoMapper;
using Extension.Domain.Entities;

namespace Extension.Application.Dto
{
    [AutoMap(typeof(Product))] // AutoMap attribute from AutoMapper
    public class ProductDto
    {
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
        public Guid ClientCardId { get; set; }
        public ClientCard ClientCard { get; set; }
    }
}
