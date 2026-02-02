using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.PropertyMedia;

public class UpdatePropertyMediaResponse
{
    public int Id { get; set; }

    public int PropertyAdId { get; set; }

    public string MediaName { get; set; }

    public string MediaUrl { get; set; }

    public string Order { get; set; }

    public DateTime UpdatedAt { get; set; }
}
