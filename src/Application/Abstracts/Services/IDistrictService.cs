using Application.Dtos.DistrictDtos;


namespace Application.Abstracts.Services
{
    public interface IDistrictService
    {
        Task<bool> CreateDistrictAsync(CreateDistrictRequest request, CancellationToken ct = default);
        Task<List<GetAllDistrictResponse>> GetAllDistrictAsync(CancellationToken ct = default);
        Task<bool> DeleteDistrictAsync(int Id, CancellationToken ct = default);
        Task<bool> UpdateDistrictAsync(int Id, UpdateDistrictRequest request, CancellationToken ct = default);
        Task<List<GetAllDistrictResponse>> GetDistrictByNameAsync(string name, CancellationToken ct = default);
        Task<GetAllDistrictResponse> GetDistrictByIdsAsync(int Id, CancellationToken ct = default);
        Task<bool> ExistsByNameDistrictAsync(string name, CancellationToken ct = default);
    }
}
