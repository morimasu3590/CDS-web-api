using BizCover.Repository.Cars;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BizCover.Application.Tests.Cars.Queries
{
    public class GetCarQueryTest
    {
        private List<Car> _Cars;
        private ICarRepository _CarRepository;
        public ICarRepository CarRepository
        {
            get
            {
                if (this._CarRepository == null)
                {
                    Mock<ICarRepository> mockCarRepository = new();
                    mockCarRepository.Setup(x => x.GetAllCars()).Returns(Task.FromResult(this._Cars));
                    this._CarRepository = mockCarRepository.Object;
                }

                return this._CarRepository;
            }

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
        public void GetCarQueryTest_WithEmptyList()
        {
            var carQuery = new GetCarQuery(this.CarRepository);
            Xunit.Assert.Throws<ArgumentNullException>( () => carQuery.Execute(1).GetAwaiter().GetResult());
            
        }

        [Fact]
        public void GetCarQueryTest_WithList()
        {
            var cars = this.Cars;
            var carQuery = new GetCarQuery(this.CarRepository);
            var actualCar = carQuery.Execute(1).GetAwaiter().GetResult();
            var expectedCar = cars.First();
            
            Assert.NotNull(actualCar);
            Assert.Equal(expectedCar.Id, actualCar.Id);
            Assert.Equal(expectedCar.Make, actualCar.Make);
            Assert.Equal(expectedCar.Model, actualCar.Model);
            Assert.Equal(expectedCar.Year, actualCar.Year);
            Assert.Equal(expectedCar.Price, actualCar.Price);
            Assert.Equal(expectedCar.Colour, actualCar.Colour);
            Assert.Equal(expectedCar.CountryManufactured, actualCar.CountryManufactured);

        }
    }
}
