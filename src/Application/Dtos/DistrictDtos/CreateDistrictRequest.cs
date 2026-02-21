using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.DistrictDtos;

public class CreateDistrictRequest
{
    public string Name { get; set; } = null!;
    public int CityId { get; set; }
}
