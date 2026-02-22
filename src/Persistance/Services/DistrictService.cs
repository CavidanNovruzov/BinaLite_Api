using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.Dtos.DistrictDtos;
using Domain;
using Domain.Entities;
using FluentValidation;


namespace Persistance.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;
        public DistrictService(IDistrictRepository districtRepository,
            IValidator<CreateDistrictRequest> validator,
            IValidator<UpdateDistrictRequest> updateValidator)
        {
            _districtRepository = districtRepository;
        }
        public async Task<bool> CreateDistrictAsync(CreateDistrictRequest request, CancellationToken ct = default)
        {
            var exists = await _districtRepository.ExistsByNameDistrictAsync(request.Name, 0, ct);
            if (exists)
                return false;


            var district = new District
            {
                Name = request.Name,
                CityId = request.CityId

            };

            await _districtRepository.AddAsync(district, ct);
            await _districtRepository.SaveChangesAsync(ct);

            return true;
        }

        public async Task<bool> DeleteDistrictAsync(int Id, CancellationToken ct = default)
        {
            var district = await _districtRepository.GetByIdAsync(Id, ct);
            if (district is null)
                return false;

            _districtRepository.Delete(district, ct);
            await _districtRepository.SaveChangesAsync(ct);

            return true;
        }

        public async Task<bool> ExistsByNameDistrictAsync(string name, CancellationToken ct = default)
        {
            name = (name ?? "").Trim();
            if (name.Length == 0)
                return false;

            return await _districtRepository.ExistsByNameDistrictAsync(name, 0, ct);
        }

        public async Task<List<GetAllDistrictResponse>> GetAllDistrictAsync(CancellationToken ct = default)
        {
            var cities = await _districtRepository.GetAllAsync(ct);

            return cities.Select(c => new GetAllDistrictResponse
            {
                Id = c.Id,
                Name = c.Name,

            }).ToList();
        }

        public async Task<GetAllDistrictResponse> GetDistrictByIdsAsync(int Id, CancellationToken ct = default)
        {
            var cities = await _districtRepository.GetAllAsync(ct);

            var city = cities.FirstOrDefault(c => c.Id == Id);
            if (city == null)
                return null;

            return new GetAllDistrictResponse
            {
                Id = city.Id,
                Name = city.Name,

            };
        }

        public async Task<List<GetAllDistrictResponse>> GetDistrictByNameAsync(string name, CancellationToken ct = default)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            name = name.Trim().ToLower();
            if (name.Length == 0)
                return new List<GetAllDistrictResponse>();

            var cities = await _districtRepository.GetAllAsync(ct);

            return cities
                .Where(c =>
                    !string.IsNullOrEmpty(c.Name) &&
                    c.Name.Trim().ToLower().Contains(name))
                .Select(c => new GetAllDistrictResponse
                {
                    Id = c.Id,
                    Name = c.Name,

                })
                .ToList();
        }

        public async Task<bool> UpdateDistrictAsync(int Id, UpdateDistrictRequest request, CancellationToken ct = default)
        {
            var district = await _districtRepository.GetByIdAsync(Id, ct);
            if (district is null)
                return false;

            var name = (request.Name ?? "").Trim();
            if (name.Length == 0)
                throw new InvalidOperationException("City Name cannot be empty");

            var exists = await _districtRepository.ExistsByNameDistrictAsync(name, Id, ct);
            if (exists)
                throw new InvalidOperationException("This city name already exists.");

            district.Name = name;
            district.UpdatedAt = DateTime.UtcNow;
            district.CityId = request.CityId;

            _districtRepository.Update(district, ct);
            await _districtRepository.SaveChangesAsync(ct);

            return true;
        }

    }
}
