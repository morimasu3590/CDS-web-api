
using System.Collections.Generic;

namespace BizCover.Application
{
    public interface ICalcCarDiscountCmd
    {
        decimal Execute(List<CarDiscountModel> carDiscountModelJSONs);
    }
}
