using Xunit;
using Moq;
using BizCover.Repository.Cars;
using System.Collections.Generic;
using System.Threading.Tasks;
using BizCover.Application;
using System.Linq;

namespace BizCover.Application.Tests
{
    public class GetCarListQueryTest
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
            get { 
                if(this._Cars == null)
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

        public GetCarListQueryTest()
        {
            
        }

        [Fact]
        public void GetCarItemModels_WithEmptyList()
        {
            var carListQuery = new GetCarListQuery(this.CarRepository);
            var carItemModelList = carListQuery.Execute().GetAwaiter().GetResult();
            
            Xunit.Assert.NotNull(carItemModelList);
            Assert.True(carItemModelList.Count() == 0);
            
        }

        [Fact]
        public void GetCarItemModels_WithList()
        {
            var cars = this.Cars;
            var carListQuery = new GetCarListQuery(this.CarRepository);
            var carItemModelList = carListQuery.Execute().GetAwaiter().GetResult();

            Xunit.Assert.NotNull(carItemModelList);
            Assert.Equal(cars.Count, carItemModelList.Count());

            foreach (var expectedCar in Cars)
            {
                var actualCar = carItemModelList.SingleOrDefault(x => x.Id == expectedCar.Id);
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
}
