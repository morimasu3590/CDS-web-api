using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizCover.Application
{
    public interface IGetCarListQuery
    {
        Task<IEnumerable<CarItemModel>> Execute();
    }
}
