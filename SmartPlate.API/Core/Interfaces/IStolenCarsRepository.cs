using SmartPlate.API.Dto.StolenCars;
using System.Threading.Tasks;

namespace SmartPlate.API.Core.Interfaces
{
    public interface IStolenCarsRepository
    {
        Task<StolenCarForDetailsDto> AddNewStolenCar(StolenCarForCreationDto stolenCarDto);
        Task<StolenCarForDetailsDto> CheckForCar(int carId);
    }
}
