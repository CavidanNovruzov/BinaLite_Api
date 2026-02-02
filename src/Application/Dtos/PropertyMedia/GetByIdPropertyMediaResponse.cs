 
namespace Application.Dtos.PropertyMedia;

public class GetByIdPropertyMediaResponse
{
    public int Id { get; set; }
    public int PropertyAdId { get; set; }
    public string MediaName { get; set; }
    public string Order { get; set; }
    public string MediaUrl { get; set; }
    public Domain.Entities.PropertyAd PropertyAd { get; set; }
}
