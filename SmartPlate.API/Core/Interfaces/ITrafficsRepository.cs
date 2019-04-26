using SmartPlate.API.Dto.Traffics;
using SmartPlate.API.Models;
using System.Threading.Tasks;

namespace SmartPlate.API.Core.Interfaces
{
    public interface ITrafficsRepository
    {
        Task<TrafficForDetailsDto> AddTrafficAsync(TrafficForCreateDto trafficForCreateDto);

        Task<Traffic> GetTrafficAsync(int trafficId);
        Task<TrafficForDetailsDto> GetTrafficMappedAsync(int trafficId);

        Task<TrafficForDetailsDto> EditTrafficAsync(int trafficId, TrafficForUpdateDto trafficForUpdateDto);

        //Task<TrafficForDetailsDto> DeleteTrafficAsync(int trafficId);


    }
}
