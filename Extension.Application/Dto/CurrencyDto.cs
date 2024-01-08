using Extension.Domain.Entities;

namespace Extension.Application.Dto
{
    public class CurrencyDto
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime CreatedDate { set; get; }

    }
}
