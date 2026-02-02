using Application.Dtos.PropertyAd;
using Application.Dtos.PropertyMedia;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstracts.Services;

public interface IPropertyMediaService
{
    Task<List<GetAllPropertyMediaResponse>> GetAllPropertyMediaAsync(CancellationToken ct = default);
    Task<GetByIdPropertyMediaResponse> GetByIdPropertyMediaAsync(int id, CancellationToken ct = default);
    Task<GetByIdPropertyMediaResponse> CreatePropertyMediaAsync(CreatePropertyMediaRequest request, CancellationToken ct = default);
    Task<UpdatePropertyMediaResponse> UpdatePropertyMediaAsync(int id, UpdatePropertyMediaRequest request, CancellationToken ct = default);
    Task DeletePropertyMediaAsync(int id, CancellationToken ct = default);

}
