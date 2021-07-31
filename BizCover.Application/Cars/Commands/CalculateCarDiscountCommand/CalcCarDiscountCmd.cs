
using System.Collections.Generic;
using System.Linq;

namespace BizCover.Application
{
    public record Order(decimal Cost, int Items, int Year);

    public class CalcCarDiscountCmd : ICalcCarDiscountCmd
    {
        public decimal Execute(System.Collections.Generic.List<CarDiscountModel> carDiscountModelJSONs)
        {
            decimal totalCost = carDiscountModelJSONs.Sum(x => x.Price);
            int countItems = carDiscountModelJSONs.Count;
            int minOldCarYear = carDiscountModelJSONs.Min(x => x.Year);

            decimal discount = CalculateDiscount(
                new Order(Cost: totalCost, Items: countItems, Year: minOldCarYear)
            );

            return discount;
        }

        private static decimal CalculateDiscount(Order order) =>
            order switch
            {
                (Cost: > 100000, Items: > 2, Year: < 2000) => 0.18m,
                (Cost: > 100000, Items: > 2, Year: > 2000) => 0.08m,
                (Cost: > 100000, Items: < 3, Year: > 2000) => 0.05m,
                (Cost: < 100000, Items: < 3, Year: > 2000) => 0.00m,
                Order { Cost: > 100000 } => 0.05m,
                Order { Items: > 2 } => 0.03m,
                Order { Year: < 2000 } => 0.10m,
                _ => throw new System.NotImplementedException(),
            };
    }
}
