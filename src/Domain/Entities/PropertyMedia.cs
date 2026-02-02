using Domain.Entities.Common;


namespace Domain.Entities;

public class PropertyMedia :BaseEntity<int>  
{
    public int PropertyAdId { get; set; }
    public string MediaName { get; set; }
    public string Order { get; set; }
    public string MediaUrl { get; set; }
    public PropertyAd PropertyAd { get; set; }
}
