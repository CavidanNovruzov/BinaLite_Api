using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.CityDtos;

public class UpdateCityRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
}
