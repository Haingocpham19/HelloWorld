using Extension.Domain.Entities;

namespace Extension.Application.Dto
{
    public class ClientCardDto
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
