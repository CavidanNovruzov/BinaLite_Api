using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.PropertyMedia;

public class UpdatePropertyMediaRequest
{
    public string MediaName { get; set; }
    public string MediaUrl { get; set; }
    public string Order { get; set; }
}
