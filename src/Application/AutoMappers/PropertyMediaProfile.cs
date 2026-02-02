
using Application.Dtos.PropertyAd;
using Application.Dtos.PropertyMedia;
using AutoMapper;
using Domain.Entities;

public class PropertyMediaProfile : Profile
{
    public PropertyMediaProfile()
    {
        CreateMap<CreatePropertyMediaRequest, PropertyMedia>();
        CreateMap<UpdatePropertyMediaRequest, PropertyMedia>();
        CreateMap<PropertyMedia, UpdatePropertyAdResponse>();
        CreateMap<PropertyMedia, GetByIdPropertyMediaResponse>();
        CreateMap<PropertyMedia, GetAllPropertyMediaResponse>();
    }
}

