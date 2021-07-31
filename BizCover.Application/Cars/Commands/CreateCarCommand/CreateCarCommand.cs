
using BizCover.Repository.Cars;
using Mapster;
using System;
using System.Threading.Tasks;

namespace BizCover.Application
{
    public class CreateCarCommand : ICreateCarCommand
    {
        private readonly ICarRepository carRepository;

        public CreateCarCommand(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }
        public async Task<Tuple<int, CreateCarModel>> Execute(CreateCarModel createCarModel)
        {
            Car newCar = createCarModel.Adapt<Car>();
            int newCarKey = await carRepository.Add(newCar);

            return new Tuple<int, CreateCarModel>(newCarKey, createCarModel);
            
        }
    }
}
