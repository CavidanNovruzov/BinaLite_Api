using Application.Dtos.PropertyAd;

namespace Application.Abstracts.Services;

public interface IPropertyAdService
{
    Task<List<GetAllPropertyAdResponse>> GetAllPropertyAdAsync(CancellationToken ct=default);
    Task<GetByIdPropertyAdResponse> GetByIdPropertyAdAsync(int id, CancellationToken ct=default);
    Task<GetByIdPropertyAdResponse> CreatePropertyAdAsync(CreatePropertyAdRequest request, CancellationToken ct = default);
    Task<UpdatePropertyAdResponse> UpdateAsync(int id, UpdatePropertyAdRequest request, CancellationToken ct = default);   
    Task DeleteAsync(int id, CancellationToken ct = default);
}
