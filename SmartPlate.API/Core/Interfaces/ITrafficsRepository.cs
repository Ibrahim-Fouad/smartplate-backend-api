using System.Collections.Generic;
using SmartPlate.API.Dto.Traffics;
using SmartPlate.API.Models;
using System.Threading.Tasks;
using SmartPlate.API.Dto;

namespace SmartPlate.API.Core.Interfaces
{
    public interface ITrafficsRepository
    {
        Task<TrafficForDetailsDto> AddTrafficAsync(TrafficForCreateDto trafficForCreateDto);

        Task<Traffic> GetTrafficAsync(int trafficId);
        Task<TrafficForDetailsDto> GetTrafficMappedAsync(int trafficId);

        Task<TrafficForDetailsDto> EditTrafficAsync(int trafficId, TrafficForUpdateDto trafficForUpdateDto);

        Task<IEnumerable<TrafficForDetailsDto>> SortTraffics(SortDto sortDto);

        Task<bool> TrafficExists(int trafficId);

        //Task<TrafficForDetailsDto> DeleteTrafficAsync(int trafficId);
    }
}