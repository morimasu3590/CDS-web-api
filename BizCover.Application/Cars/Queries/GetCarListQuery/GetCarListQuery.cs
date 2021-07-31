

using BizCover.Repository.Cars;
using Mapster;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizCover.Application
{
    public class GetCarListQuery : IGetCarListQuery
    {
        private readonly ICarRepository carRepository;

        public GetCarListQuery(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        public async Task<IEnumerable<CarItemModel>> Execute()
        {
            var cars = await carRepository.GetAllCars();
            if (cars == null) return new List<CarItemModel>();

            var carListItemModels = cars.Select(cli => cli.Adapt<CarItemModel>());
            return carListItemModels;
                
        }
    }
}
