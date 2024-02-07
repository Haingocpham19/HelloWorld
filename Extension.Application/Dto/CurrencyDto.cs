using AutoMapper;
using Extension.Domain.Entities;

namespace Extension.Application.Dto
{
    [AutoMap(typeof(Currency), ReverseMap = true)]
    public class CurrencyDto
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
