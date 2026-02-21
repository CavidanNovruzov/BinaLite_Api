using Application.Dtos.DistrictDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.CityDtos;

public class CityWithDistrictResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<DistrictItemResponse> Districts { get; set; }
}
