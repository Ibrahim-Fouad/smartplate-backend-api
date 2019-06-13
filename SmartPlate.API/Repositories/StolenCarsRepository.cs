using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Db;
using SmartPlate.API.Dto.StolenCars;
using SmartPlate.API.Models;
using System.Threading.Tasks;
using SmartPlate.API.Dto;

namespace SmartPlate.API.Repositories
{
    public class StolenCarsRepository : IStolenCarsRepository
    {
        private readonly AppDbContext _context;
        private readonly ICarsRepository _carsRepository;
        private readonly IMapper _mapper;

        public StolenCarsRepository(AppDbContext context, ICarsRepository carsRepository, IMapper mapper)
        {
            _context = context;
            _carsRepository = carsRepository;
            _mapper = mapper;
        }

        public async Task<StolenCarForDetailsDto> AddNewStolenCarAsync(StolenCarForCreationDto stolenCarDto)
        {
            if (stolenCarDto.CarId == 0 || string.IsNullOrWhiteSpace(stolenCarDto.PlateNumber))
                return new StolenCarForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "You must enter car id or car plate number."
                };

            if (!string.IsNullOrWhiteSpace(stolenCarDto.PlateNumber))
            {
                var carIbDb = await _carsRepository.GetCarAsync(stolenCarDto.PlateNumber);
                if (carIbDb == null)
                    return new StolenCarForDetailsDto
                    {
                        Success = false,
                        ErrorMessage = "Car is not exists"
                    };

                stolenCarDto.CarId = carIbDb.Id;
            }
            else
            {
                if (!await _carsRepository.CarExistsAsync(stolenCarDto.CarId))
                    return new StolenCarForDetailsDto
                    {
                        Success = false,
                        ErrorMessage = "Car is not exists"
                    };
            }


            if (await _context.StolenCars.AnyAsync(c => c.CarId == stolenCarDto.CarId && !c.HasReturenedToOwner))
                return new StolenCarForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "Car has reported stolen before and has not reterned to owner."
                };

            var stolenCar = _mapper.Map<StolenCar>(stolenCarDto);
            await _context.StolenCars.AddAsync(stolenCar);

            var count = await _context.SaveChangesAsync();
            if (count > 0)
            {
                await _carsRepository.ChangeStolenStateAsync(stolenCar.CarId, true);
                return _mapper.Map<StolenCarForDetailsDto>(stolenCar);
            }

            return new StolenCarForDetailsDto
            {
                Success = false,
                ErrorMessage = "Data has not saved, please try again."
            };
        }

        public async Task<StolenCarForDetailsDto> CheckForCarAsync(int carId)
        {
            var stolenCarInDb = await _context.StolenCars
                .Include(s => s.Car)
                .FirstOrDefaultAsync(c => c.CarId == carId && !c.HasReturenedToOwner);

            if (stolenCarInDb == null)
                return new StolenCarForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "This car has not reported as stolen before or has reterned to its owner."
                };

            return _mapper.Map<StolenCarForDetailsDto>(stolenCarInDb);
        }

        public async Task<IEnumerable<StolenCarForDetailsDto>> FilterStolenCarsAsync(SortDto sort)
        {
            var stolenCars = _context.StolenCars
                .Include(c => c.Car)
                .AsQueryable();

            //id, car, reternedToOwner, stolenObject
            var columnMap = new Dictionary<string, Expression<Func<StolenCar, object>>>
            {
                ["id"] = c => c.Id,
                ["car"] = c => c.CarId,
                ["reternedToOwner"] = c => c.HasReturenedToOwner,
                ["stolenObject"] = c => c.CarOrPlateIsStoled,
            };

            if (sort.IsAscending)
                stolenCars = stolenCars.OrderBy(columnMap[sort.SortBy]);
            else
                stolenCars = stolenCars.OrderByDescending(columnMap[sort.SortBy]);

            stolenCars = stolenCars.Skip((sort.PageNumber - 1) * sort.PageSize).Take(sort.PageSize);

            return _mapper.Map<StolenCarForDetailsDto[]>(await stolenCars.ToListAsync());
        }
    }
}