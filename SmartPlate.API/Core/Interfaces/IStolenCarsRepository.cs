using System.Collections.Generic;
using SmartPlate.API.Dto.StolenCars;
using System.Threading.Tasks;
using SmartPlate.API.Dto;

namespace SmartPlate.API.Core.Interfaces
{
    public interface IStolenCarsRepository
    {
        Task<StolenCarForDetailsDto> AddNewStolenCarAsync(StolenCarForCreationDto stolenCarDto);
        Task<StolenCarForDetailsDto> CheckForCarAsync(int carId);
        Task<IEnumerable<StolenCarForDetailsDto>> FilterStolenCarsAsync(SortDto sortDto);


    }
}
