using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.DistrictDtos;

public class GetAllDistrictResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CityId { get; set; }
    public string CityName { get; set; } = null!;
}
