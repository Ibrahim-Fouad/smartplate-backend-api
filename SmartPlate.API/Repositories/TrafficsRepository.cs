using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Db;
using SmartPlate.API.Dto.Traffics;
using SmartPlate.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SmartPlate.API.Dto;

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
                string.Equals(t.Name, trafficForCreateDto.Name, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(t.Governorate, trafficForCreateDto.Governorate,
                    StringComparison.CurrentCultureIgnoreCase));

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

        public async Task<IEnumerable<TrafficForDetailsDto>> SortTraffics(SortDto sortDto)
        {
            var traffics = _context.Traffics.AsQueryable();

            var columnMap = new Dictionary<string, Expression<Func<Traffic, object>>>
            {
                ["id"] = c => c.Id,
                ["name"] = c => c.Name,
                ["address"] = c => c.Address,
                ["governorate"] = c => c.Governorate,
                ["phoneNumber"] = c => c.PhoneNumber,
            };

            if (sortDto.IsAscending)
                traffics = traffics.OrderBy(columnMap[sortDto.SortBy]);
            else
                traffics = traffics.OrderByDescending(columnMap[sortDto.SortBy]);

            traffics = traffics.Skip((sortDto.PageNumber - 1) * sortDto.PageSize).Take(sortDto.PageSize);
            return _mapper.Map<TrafficForDetailsDto[]>(await traffics.ToListAsync());
        }

        public async Task<bool> TrafficExists(int trafficId)
        {
            return await _context.Traffics.AnyAsync(t => t.Id == trafficId);
        }
    }
}