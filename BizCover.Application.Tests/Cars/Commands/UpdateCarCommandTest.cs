using Xunit;
using Moq;
using System.Collections.Generic;
using BizCover.Repository.Cars;
using System.Threading.Tasks;
using System;
using System.Linq;
using Mapster;

namespace BizCover.Application.Tests
{
    public class UpdateCarCommandTest
    {
        private List<Car> _Cars;
        private ICarRepository _CarRepository;
        public ICarRepository CarRepository
        {
            get
            {
                if (this._CarRepository == null)
                {
                    var dummyCar = updateCarModel_Car1.Adapt<Car>();
                    Mock<ICarRepository> mockCarRepository = new();
                    mockCarRepository.Setup(x => x.Update(dummyCar));
                    mockCarRepository.Setup(x => x.GetAllCars()).Returns(Task.FromResult(this._Cars));
                    this._CarRepository = mockCarRepository.Object;
                }

                return this._CarRepository;
            }

        }

        private UpdateCarModel updateCarModel_Car1;

        public UpdateCarCommandTest()
        {
            updateCarModel_Car1 = new UpdateCarModel()
            {
                Id = 1,
                Make = "Honda update",
                Model = "Civic Type R update",
                Price = 56000m,
                Year = 2021,
                Colour = "White Championship",
                CountryManufactured = "UK"
            };
        }
        
        public List<Car> Cars
        {
            get
            {
                if (this._Cars == null)
                {
                    int id = 0;
                    this._Cars = new List<Car>()
                    {
                        new Car
                        {
                            Id = ++id,
                            Make = "Honda",
                            Model = "Civic Type R",
                            Price = 56000m,
                            Year = 2021,
                            Colour = "White Championship",
                            CountryManufactured = "UK"
                        },
                        new Car
                        {
                            Id = ++id,
                            Make = "Toyota",
                            Model = "GR Yaris",
                            Price = 64000m,
                            Year = 2021,
                            Colour = "Metal Gray",
                            CountryManufactured = "Japan"
                        },
                        new Car
                        {
                            Id = ++id,
                            Make = "Toyota",
                            Model = "GR 86",
                            Price = 70000m,
                            Year = 2021,
                            Colour = "Blue Pearl",
                            CountryManufactured = "Japan"
                        }
                    };
                }

                return this._Cars;
            }

        }

        [Fact]
        public void UpdateCarCommand_WithNoList_NullParam()
        {
            var cmd = new UpdateCarCommand(this.CarRepository);
            Assert.ThrowsAsync<ArgumentNullException>(() => cmd.Execute(null));
        }

        [Fact]
        public void UpdateCarCommand_WithNoList()
        {
            var cmd = new UpdateCarCommand(this.CarRepository);
            Assert.ThrowsAsync<ArgumentNullException>(() => cmd.Execute(updateCarModel_Car1));
        }

        [Fact]
        public void UpdateCarCommand_WithList_NotExistData()
        {
            var cars = this.Cars;
            var updateCarModel = new UpdateCarModel();
            var cmd = new UpdateCarCommand(this.CarRepository);
            Assert.ThrowsAsync<ArgumentNullException>(() => cmd.Execute(updateCarModel));
        }

        
    }
}
