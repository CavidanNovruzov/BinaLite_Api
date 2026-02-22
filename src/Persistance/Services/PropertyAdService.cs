using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.Dtos.PropertyAd;
using Domain.Entities;
using FluentValidation;


namespace Persistance.Services;

public class PropertyAdService : IPropertyAdService
{
    private readonly IPropertyAdRepository _repository;
    public PropertyAdService(IPropertyAdRepository repository)
    {
        _repository = repository;
    }
    public async Task<GetByIdPropertyAdResponse> CreatePropertyAdAsync(CreatePropertyAdRequest request, CancellationToken ct = default)
    {
        var propertyAd = new PropertyAd
        {
            Title = request.Title,
            Description = request.Description,
            RoomCount = request.RoomCount,
            Area = request.Area,
            Price = request.Price,
            IsExtract = request.IsExtract,
            IsMortgage = request.IsMortgage,
            OfferType = request.OfferType,
            PropertyCategory = request.PropertyCategory,
            UpdatedAt = DateTime.Now
        };
  
        await _repository.AddAsync(propertyAd, ct);
        await _repository.SaveChangesAsync(ct);
        return new GetByIdPropertyAdResponse
        {
            Id = propertyAd.Id,
            Title =  propertyAd.Title,
            Description = propertyAd.Description,
            RoomCount = propertyAd.RoomCount,
            Area = propertyAd.Area,
            Price = propertyAd.Price,
            IsExtract = propertyAd.IsExtract,
            IsMortgage = propertyAd.IsMortgage,
            OfferType = propertyAd.OfferType,
            PropertyCategory = propertyAd.PropertyCategory
        };
    }

    public async Task<List<GetAllPropertyAdResponse>> GetAllPropertyAdAsync(CancellationToken ct = default)
    {
       var propertyAds = await _repository.GetAllAsync(ct);

        return propertyAds.Select(pa => new GetAllPropertyAdResponse
        {
            Id = pa.Id,
            Title = pa.Title,
            Description = pa.Description
        }).ToList();
    }

    public async Task<GetByIdPropertyAdResponse> GetByIdPropertyAdAsync(int id, CancellationToken ct = default)
    {
        var propertyAd = await _repository.GetByIdAsync(id, ct);
        if (propertyAd == null)
            throw new KeyNotFoundException($"PropertyAd with id {id} not found.");

        return new GetByIdPropertyAdResponse
        {
            Id = propertyAd.Id,
            Title = propertyAd.Title,
            Description = propertyAd.Description,
            RoomCount = propertyAd.RoomCount,
            Area = propertyAd.Area,
            Price = propertyAd.Price,
            IsExtract = propertyAd.IsExtract,
            IsMortgage = propertyAd.IsMortgage,
            OfferType = propertyAd.OfferType,
            PropertyCategory = propertyAd.PropertyCategory
        };
    }

    public async Task<UpdatePropertyAdResponse> UpdateAsync(int id, UpdatePropertyAdRequest request, CancellationToken ct = default)
    {
        var propertyAd = await _repository.GetByIdAsync(id, ct);
        if (propertyAd == null)
          throw new KeyNotFoundException($"PropertyAd with id {id} not found.");   
        
        propertyAd.Title = request.Title;    
        propertyAd.Description = request.Description;
        propertyAd.RoomCount = request.RoomCount;
        propertyAd.Area = request.Area;
        propertyAd.Price = request.Price;
        propertyAd.IsExtract = request.IsExtract;
        propertyAd.IsMortgage = request.IsMortgage;
        propertyAd.OfferType = request.OfferType;
        propertyAd.PropertyCategory = request.PropertyCategory;

        _repository.Update(propertyAd);
        await _repository.SaveChangesAsync(ct);

        return new UpdatePropertyAdResponse 
        {
            Id = propertyAd.Id,
            Title = propertyAd.Title,
            Description = propertyAd.Description,
            RoomCount = propertyAd.RoomCount,
            Area = propertyAd.Area,
            Price = propertyAd.Price,
            IsExtract = propertyAd.IsExtract,
            IsMortgage = propertyAd.IsMortgage,
            OfferType = propertyAd.OfferType,
            PropertyCategory = propertyAd.PropertyCategory,
            UpdatedAt = propertyAd.UpdatedAt
        };  
        
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var propertyAd =  await _repository.GetByIdAsync(id, ct);

        if (propertyAd == null)
             throw new KeyNotFoundException($"PropertyAd with id {id} not found.");

        _repository.Delete(propertyAd);
        await _repository.SaveChangesAsync(ct);
    }
}
