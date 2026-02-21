using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.DistrictDtos;

public class UpdateDistrictRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CityId { get; set; }
}

