using SmartPlate.API.Dto.Cars;
using System.Threading.Tasks;
using SmartPlate.API.Models;

namespace SmartPlate.API.Core.Interfaces
{
    public interface ICarsRepository
    {
        Task<CarForDetailsDto> AddCarAsync(CarForCreationDto carForCreationDto);

        Task<Car> GetCar(int id);
        Task<Car> GetCar(string plateNumber);

        Task<CarForDetailsDto> GetCarMapped(int id);
        Task<CarForDetailsDto> GetCarMapped(string plateNumber);

        Task<CarForDetailsDto> UpdateCarDetails(int carId, CarForUpdateDto carForUpdateDto);
    }
}