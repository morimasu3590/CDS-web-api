
using BizCover.Repository.Cars;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizCover.Application
{
    

    public class CalcCarDiscountCmd : ICalcCarDiscountCmd
    {
        private readonly ICarRepository _carRepository;

        public CalcCarDiscountCmd(ICarRepository carRepository)
        {
            this._carRepository = carRepository;
        }

        public async Task<decimal> Execute(int[] carIds)
        {
            if (carIds is null)
                throw new ArgumentNullException(nameof(carIds));

            var allCars = await _carRepository.GetAllCars();
            if(allCars is null || allCars.Count == 0)
                throw new ArgumentNullException(nameof(carIds));

            var orderedCars = from carRepo in allCars
                              join carId in carIds on carRepo.Id equals carId
                              select carRepo;
            var orders = orderedCars.GroupBy(order => order.Id)
                .Select(order => new { 
                    car = order.First(),
                    quantity = order.Count()
                });



            decimal totalCost = orders.Sum(order => order.car.Price * order.quantity);
            int countItems = orders.Sum(order => order.quantity);
            int minOldCarYear = orders.Min(order => order.car.Year);

            decimal discount = CalculateDiscount(
                totalCost, countItems, minOldCarYear
            );

            return discount;
        }

        private static decimal CalculateDiscount(decimal totalCost, int totalCars, int oldestYear)
        {
            decimal totalDiscount = 0;
            if(totalCost > 100000m)
            {
                totalDiscount += 5;
            }

            if(totalCars > 2)
            {
                totalDiscount += 3;
            }

            if(oldestYear < 2000)
            {
                totalDiscount += 10;
            }

            return totalDiscount;
        }

        
            

    }
}
