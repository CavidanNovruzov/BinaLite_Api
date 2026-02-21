using Application.Abstracts.Services;
using Application.Dtos.PropertyAd;
using Application.Shared.Helpers.Responses;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyAdController : ControllerBase
    {
        private readonly IPropertyAdService _service;

        public PropertyAdController(IPropertyAdService service)
        {
            _service = service;
        }

        [Authorize(Policy = Policies.ManageProperties)]
        [HttpPost]
        public async Task<BaseResponse<GetByIdPropertyAdResponse>> Create([FromBody] CreatePropertyAdRequest request, CancellationToken ct)
        {
            var result = await _service.CreatePropertyAdAsync(request, ct);
            return BaseResponse<GetByIdPropertyAdResponse>.Ok(result, "Property ad created successfully.");
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<GetByIdPropertyAdResponse>> GetById(int id, CancellationToken ct)
        {
            var result = await _service.GetByIdPropertyAdAsync(id, ct);
            if (result == null)
                return BaseResponse<GetByIdPropertyAdResponse>.Fail($"PropertyAd with id {id} not found.");

            return BaseResponse<GetByIdPropertyAdResponse>.Ok(result);
        }

        [HttpGet]
        public async Task<BaseResponse<List<GetAllPropertyAdResponse>>> GetAll(CancellationToken ct)
        {
            var result = await _service.GetAllPropertyAdAsync(ct);
            return BaseResponse<List<GetAllPropertyAdResponse>>.Ok(result);
        }

        [Authorize(Policy = Policies.ManageProperties)]
        [HttpPut("{id}")]
        public async Task<BaseResponse<UpdatePropertyAdResponse>> Update(int id, [FromBody] UpdatePropertyAdRequest request, CancellationToken ct)
        {
            var result = await _service.UpdateAsync(id, request, ct);
            return BaseResponse<UpdatePropertyAdResponse>.Ok(result, "Property ad updated successfully.");
        }

        [Authorize(Policy = Policies.ManageProperties)]
        [HttpDelete("{id}")]
        public async Task<BaseResponse> Delete(int id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return BaseResponse.Ok("Property ad deleted successfully.");
        }
    }
}
