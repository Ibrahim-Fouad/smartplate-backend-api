using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Db;
using SmartPlate.API.Dto.Cars;
using SmartPlate.API.Models;

namespace SmartPlate.API.Repositories
{
    public class CarsRepository : ICarsRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITrafficsRepository _trafficsRepository;

        public CarsRepository(AppDbContext context,
            IMapper mapper,
            ITrafficsRepository trafficsRepository)
        {
            _context = context;
            _mapper = mapper;
            _trafficsRepository = trafficsRepository;
        }

        public async Task<CarForDetailsDto> AddCarAsync(CarForCreationDto carForCreationDto)
        {
            if (await _context.Cars.AnyAsync(c => string.Equals(c.PlateNumber, carForCreationDto.PlateNumber,
                StringComparison.CurrentCultureIgnoreCase)))
                return new CarForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "This car is already exists."
                };

            if (carForCreationDto.StartDate > DateTime.Now)
                return new CarForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "License start date can not be greater than current date."
                };

            if (!await _trafficsRepository.TrafficExists(carForCreationDto.TrafficId))
                return new CarForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "Traffic is not exists."
                };

            var car = _mapper.Map<CarForCreationDto, Car>(carForCreationDto);
            _context.Cars.Add(car);

            var count = await _context.SaveChangesAsync();
            if (count > 0)
                return _mapper.Map<CarForDetailsDto>(car);

            return new CarForDetailsDto
            {
                Success = false,
                ErrorMessage = "No data has changed."
            };
        }

        public async Task<Car> GetCar(int id)
        {
            return await _context.Cars
                .Include(c => c.Traffic)
                .Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Car> GetCar(string plateNumber)
        {
            return await _context.Cars
                .Include(c => c.Traffic)
                .Where(c => c.PlateNumber.Equals(plateNumber, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefaultAsync();
        }

        public async Task<CarForDetailsDto> GetCarMapped(int id)
        {
            var car = await GetCar(id);
            if (car == null)
                return new CarForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "Car is not found."
                };

            return _mapper.Map<CarForDetailsDto>(car);
        }

        public async Task<CarForDetailsDto> GetCarMapped(string plateNumber)
        {
            var car = await GetCar(plateNumber);
            if (car == null)
                return new CarForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "Car is not found."
                };

            return _mapper.Map<CarForDetailsDto>(car);
        }

        public async Task<CarForDetailsDto> UpdateCarDetails(int carId, CarForUpdateDto carForUpdateDto)
        {
            var car = await GetCar(carId);
            if (car == null)
                return new CarForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "Car is not found."
                };

            if (await _context.Cars.AnyAsync(c =>
                c.PlateNumber.Equals(carForUpdateDto.PlateNumber, StringComparison.CurrentCultureIgnoreCase)))
                return new CarForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "New plate number is already exists."
                };

            if (carForUpdateDto.TrafficId != 0 && !await _trafficsRepository.TrafficExists(carForUpdateDto.TrafficId))
                return new CarForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "New traffic is not found"
                };

            _mapper.Map(carForUpdateDto, car);
            car.StartDate = DateTime.Now;
            car.EndDate = car.StartDate.AddYears(10);

            var count = await _context.SaveChangesAsync();
            if (count == 0)
                return new CarForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "No data has changed."
                };

            return _mapper.Map<CarForDetailsDto>(car);
        }
    }
}