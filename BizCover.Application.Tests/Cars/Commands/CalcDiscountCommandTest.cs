using Xunit;
using Moq;
using System.Collections.Generic;
using BizCover.Repository.Cars;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace BizCover.Application.Tests
{
    public class CalcDiscountCommandTest
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


        public CalcDiscountCommandTest()
        {

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
        public void GetCarItemModels_WithEmptyList_NullParameter()
        {
            var carListQuery = new CalcCarDiscountCmd(this.CarRepository);
            Xunit.Assert.Throws<ArgumentNullException>(() => carListQuery.Execute(null).GetAwaiter().GetResult());
        }

        [Fact]
        public void GetCarItemModels_WithEmptyList()
        {
            int[] ids = new int[] { 1, 2, 3 };

            var carListQuery = new CalcCarDiscountCmd(this.CarRepository);
            Xunit.Assert.Throws<ArgumentNullException>(() => carListQuery.Execute(ids).GetAwaiter().GetResult());
        }

        [Fact]
        public void GetCarItemModels_discount()
        {
            var cars = this.Cars;
            int[] ids = new int[] { 1 };

            var carListQuery = new CalcCarDiscountCmd(this.CarRepository);
            decimal discount = carListQuery.Execute(ids).GetAwaiter().GetResult();

            Assert.Equal(0, discount);

            ids = new int[] { 1, 2 };

            carListQuery = new CalcCarDiscountCmd(this.CarRepository);
            discount = carListQuery.Execute(ids).GetAwaiter().GetResult();

            Assert.Equal(5, discount);
        }
    }
}
