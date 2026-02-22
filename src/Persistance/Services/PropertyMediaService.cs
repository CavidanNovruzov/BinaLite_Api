
using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.Dtos.PropertyAd;
using Application.Dtos.PropertyMedia;
using Application.Validations.PropertyMedia;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Options;
namespace Persistance.Services;

public class PropertyMediaService : IPropertyMediaService
{
    private readonly IPropertyMediaRepository _repository;
    private readonly IMapper _mapper;

    public PropertyMediaService(IPropertyMediaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<GetByIdPropertyMediaResponse> CreatePropertyMediaAsync(CreatePropertyMediaRequest request, CancellationToken ct = default)
    {
        // 1. request → entity mapping
        var entity = _mapper.Map<PropertyMedia>(request);

        // 2. entity repository-yə əlavə et
        await _repository.AddAsync(entity, ct);
        await _repository.SaveChangesAsync(ct); 

        // 3. entity → response mapping
        var response = _mapper.Map<GetByIdPropertyMediaResponse>(entity);

        // 4. cavabı qaytar
        return response;
    }

    public async Task DeletePropertyMediaAsync(int id, CancellationToken ct = default)
    {
        var propertyMedia= await _repository.GetByIdAsync(id, ct);
        if (propertyMedia == null)
            throw new KeyNotFoundException($"PropertyMedia with id {id} not found.");
         _repository.Delete(propertyMedia);
        await _repository.SaveChangesAsync(ct);
    }

    public async Task<List<GetAllPropertyMediaResponse>> GetAllPropertyMediaAsync(CancellationToken ct = default)
    {
        var propertyMedias = await _repository.GetAllAsync(ct);
        var response = _mapper.Map<List<GetAllPropertyMediaResponse>>(propertyMedias);
        return response;
    }

    public async Task<GetByIdPropertyMediaResponse> GetByIdPropertyMediaAsync(int id, CancellationToken ct = default)
    {
         var propertyMedia = await _repository.GetByIdAsync(id, ct);
        var response = _mapper.Map<GetByIdPropertyMediaResponse>(propertyMedia);
        return response;
    }

    public async Task<UpdatePropertyMediaResponse> UpdatePropertyMediaAsync(int id, UpdatePropertyMediaRequest request, CancellationToken ct = default)
    {     
        var propertyMedia= await _repository.GetByIdAsync(id, ct);
        if (propertyMedia == null)
            throw new KeyNotFoundException($"PropertyMedia with id {id} not found.");
        _mapper.Map(request, propertyMedia);
        _repository.Update(propertyMedia);
        await _repository.SaveChangesAsync(ct);

        var response = _mapper.Map<UpdatePropertyMediaResponse>(propertyMedia);
        return response;
    }
}
