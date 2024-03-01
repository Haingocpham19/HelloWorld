using Extension.Domain.Entities;

namespace Extension.Application.Dto
{
    public class SourcePageDto
    {
        public int SourcePageId { get; set; }
        public string PageName { get; set; }
        public string Domain { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
