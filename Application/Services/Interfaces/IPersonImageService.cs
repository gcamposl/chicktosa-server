using Application.DTOs.PersonImage;

namespace Application.Services.Interfaces
{
    public interface IPersonImageService
    {
        Task<ResultService> CreateImageBase64Async(PersonImageDTO personImageDTO);
    }
}