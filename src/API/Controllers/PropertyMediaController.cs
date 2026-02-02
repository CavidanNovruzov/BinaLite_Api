using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.Dtos.PropertyMedia;
using Application.Shared.Helpers.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyMediaController : ControllerBase
    {
        private readonly IPropertyMediaService _service;
        public PropertyMediaController(IPropertyMediaService service)
        {
            _service = service; 
        }
        [HttpPost]
        public async Task<BaseResponse<GetByIdPropertyMediaResponse>> Create([FromBody] CreatePropertyMediaRequest request, CancellationToken ct)
        {
            var result = await _service.CreatePropertyMediaAsync(request, ct);
            return BaseResponse<GetByIdPropertyMediaResponse>.Ok(result, "Property media created successfully.");
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<GetByIdPropertyMediaResponse>> GetById(int id, CancellationToken ct)
        {
            var result = await _service.GetByIdPropertyMediaAsync(id, ct);
            if (result == null)
                return BaseResponse<GetByIdPropertyMediaResponse>.Fail($"PropertyMedia with id {id} not found.");

            return BaseResponse<GetByIdPropertyMediaResponse>.Ok(result);
        }

        [HttpGet]
        public async Task<BaseResponse<List<GetAllPropertyMediaResponse>>> GetAll(CancellationToken ct)
        {
            var result = await _service.GetAllPropertyMediaAsync(ct);
            return BaseResponse<List<GetAllPropertyMediaResponse>>.Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<BaseResponse<UpdatePropertyMediaResponse>> Update(int id, [FromBody] UpdatePropertyMediaRequest request, CancellationToken ct)
        {
            var result = await _service.UpdatePropertyMediaAsync(id, request, ct);
            return BaseResponse<UpdatePropertyMediaResponse>.Ok(result, "Property media updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<BaseResponse> Delete(int id, CancellationToken ct)
        {
            await _service.DeletePropertyMediaAsync(id, ct);
            return BaseResponse.Ok("Property media deleted successfully.");
        }
    }
}

