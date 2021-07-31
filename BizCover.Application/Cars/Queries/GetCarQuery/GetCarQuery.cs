using BizCover.Repository.Cars;
using Mapster;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BizCover.Application
{
    public class GetCarQuery : IGetCarQuery
    {
        private readonly ICarRepository carRepository;

        public GetCarQuery(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        public async Task<CarItemModel> Execute(int id)
        {
            var cars = await carRepository.GetAllCars();
            var car = cars.SingleOrDefault(c => c.Id == id);

            if (car == null)
                throw new ArgumentNullException(nameof(GetCarQuery));
            
            return car.Adapt<CarItemModel>();
        }
    }
}
