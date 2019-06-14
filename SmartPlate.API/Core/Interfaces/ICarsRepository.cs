using System.Collections.Generic;
using SmartPlate.API.Dto.Cars;
using System.Threading.Tasks;
using SmartPlate.API.Dto;
using SmartPlate.API.Models;

namespace SmartPlate.API.Core.Interfaces
{
    public interface ICarsRepository
    {
        Task<CarForDetailsDto> AddCarAsync(CarForCreationDto carForCreationDto);

        Task<Car> GetCarAsync(int id);
        Task<Car> GetCarAsync(string plateNumber);

        Task<bool> CarExistsAsync(int carId);

        Task<bool> CarExistsAsync(string plateNumber);
        Task<CarForDetailsDto> GetCarMappedAsync(int id);
        Task<CarForDetailsDto> GetCarMappedAsync(string plateNumber);

        Task<CarForDetailsDto> UpdateCarDetailsAsync(int carId, CarForUpdateDto carForUpdateDto);

        Task<IEnumerable<CarForDetailsDto>> GetUsersCarsAsync(string userId, SortDto sort);
        Task<IEnumerable<CarForListDto>> GetAllCarsAsync(SortDto sort);

        Task<bool> ChangeStolenStateAsync(int carId, bool newState);
    }
}