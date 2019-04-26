using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Db;
using SmartPlate.API.Dto.Traffics;
using SmartPlate.API.Models;
using System;
using System.Threading.Tasks;

namespace SmartPlate.API.Repositories
{
    public class TrafficsRepository : ITrafficsRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TrafficsRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<TrafficForDetailsDto> AddTrafficAsync(TrafficForCreateDto trafficForCreateDto)
        {
            var trafficInDb = await _context.Traffics.AnyAsync(t =>
                t.Name.ToLower() == trafficForCreateDto.Name.ToLower() &&
                t.Governorate.ToLower() == trafficForCreateDto.Governorate.ToLower());

            if (trafficInDb)
                return new TrafficForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "Traffic is already exists"
                };

            var traffic = _mapper.Map<Traffic>(trafficForCreateDto);
            await _context.Traffics.AddAsync(traffic);

            var count = await _context.SaveChangesAsync();

            if (count > 0)
                return _mapper.Map<TrafficForDetailsDto>(traffic);

            return new TrafficForDetailsDto
            {
                Success = false,
                ErrorMessage = "No data saved."
            };
        }

        public async Task<Traffic> GetTrafficAsync(int trafficId)
        {
            return await _context.Traffics.FirstOrDefaultAsync(t => t.Id == trafficId);
        }

        public async Task<TrafficForDetailsDto> GetTrafficMappedAsync(int trafficId)
        {
            var traffic = await GetTrafficAsync(trafficId);
            if (traffic == null)
                return new TrafficForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "Traffic is not found."
                };

            return _mapper.Map<TrafficForDetailsDto>(traffic);
        }

        public async Task<TrafficForDetailsDto> EditTrafficAsync(int trafficId, TrafficForUpdateDto trafficForUpdateDto)
        {
            if (await _context.Traffics.AnyAsync(t =>
                t.Name.ToLower() == trafficForUpdateDto.Name.ToLower() &&
                t.Governorate.ToLower() == trafficForUpdateDto.Governorate.ToLower()))
                return new TrafficForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "Traffic is already exists"
                };

            var traffic = await GetTrafficAsync(trafficId);
            if (traffic == null)
                return new TrafficForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "Traffic is not found"
                };

            if
            (traffic.Name.ToLower() == trafficForUpdateDto.Name.ToLower() &&
             traffic.Governorate.ToLower() == trafficForUpdateDto.Governorate.ToLower())
            {
                return new TrafficForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "New traffic name is already exists."
                };
            }

            _mapper.Map(trafficForUpdateDto, traffic);
            var count = await _context.SaveChangesAsync();
            if (count > 0)
                return _mapper.Map<TrafficForDetailsDto>(traffic);

            return new TrafficForDetailsDto
            {
                Success = false,
                ErrorMessage = "No data saved"
            };
        }

    }
}