using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Db;
using SmartPlate.API.Dto.StolenCars;
using SmartPlate.API.Models;

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

        public async Task<StolenCarForDetailsDto> AddNewStolenCar(StolenCarForCreationDto stolenCarDto)
        {
            if (!await _carsRepository.CarExists(stolenCarDto.CarId))
                return new StolenCarForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "Car is not exists"
                };

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

        public async Task<StolenCarForDetailsDto> CheckForCar(int carId)
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
    }
}