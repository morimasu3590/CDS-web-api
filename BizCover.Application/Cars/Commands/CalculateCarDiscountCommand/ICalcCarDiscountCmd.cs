using System.Threading.Tasks;

namespace BizCover.Application
{
    public interface ICalcCarDiscountCmd
    {
        Task<decimal> Execute(int[] carIds);
    }
}
