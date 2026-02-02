using Domain.Entities;

namespace Application.Dtos.PropertyMedia;

public class CreatePropertyMediaRequest
{
    public int PropertyAdId { get; set; }
    public string MediaName { get; set; }
    public string Order { get; set; }
    public string MediaUrl { get; set; }
}
