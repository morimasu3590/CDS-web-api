using BizCover.Application;
using BizCover.Repository.Cars;
using Mapster;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BizCover.Application.Tests
{
    public class CreateCarCommandTest
    {
        private List<Car> _Cars;
        
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

        public ICarRepository carRepository 
        {
            get
            {
                return new CarRepository();
            }  
        }

        public CreateCarCommandTest()
        {
            
        }

        [Fact]
        public void CreateCarCommandTest_BestTest()
        {
            var expectedCar = new Car
            {
                Make = "Esemka",
                Model = "Pickup",
                Price = 560m,
                Year = 2020,
                Colour = "White",
                CountryManufactured = "Indo"
            };

            CreateCarModel carModel = expectedCar.Adapt<CreateCarModel>();
            var cmd = new CreateCarCommand(this.carRepository);
            var result = cmd.Execute(carModel).GetAwaiter().GetResult();

            var actualCar = result.Item2;

            Assert.NotNull(actualCar);
           
            Assert.Equal(expectedCar.Make, actualCar.Make);
            Assert.Equal(expectedCar.Model, actualCar.Model);
            Assert.Equal(expectedCar.Year, actualCar.Year);
            Assert.Equal(expectedCar.Price, actualCar.Price);
            Assert.Equal(expectedCar.Colour, actualCar.Colour);
            Assert.Equal(expectedCar.CountryManufactured, actualCar.CountryManufactured);
        }
    }
}
