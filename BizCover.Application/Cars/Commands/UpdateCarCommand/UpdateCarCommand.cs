
using BizCover.Repository.Cars;
using Mapster;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BizCover.Application
{
    public class UpdateCarCommand : IUpdateCarCommand
    {
        
        private readonly ICarRepository carRepository;

        public UpdateCarCommand(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        public async Task Execute(UpdateCarModel updateCarModel)
        {
            var cars = await carRepository.GetAllCars();
            var existingCar = cars.SingleOrDefault(c => c.Id == updateCarModel.Id);

            if (existingCar == null)
                throw new ArgumentNullException(nameof(updateCarModel));

            Car updatedCar = updateCarModel.Adapt<Car>();
            updatedCar.Id = existingCar.Id;

            await carRepository.Update(updatedCar);
        }
    }
}
