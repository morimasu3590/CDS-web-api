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
            if (cars == null)
                throw new ArgumentNullException(nameof(id));

            var car = cars.SingleOrDefault(c => c.Id == id);
            if (car == null)
                throw new ArgumentNullException(nameof(id));
            
            return car.Adapt<CarItemModel>();
        }
    }
}
