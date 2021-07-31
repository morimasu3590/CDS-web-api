using System.Threading.Tasks;

namespace BizCover.Application
{
    public interface IGetCarQuery
    {
        Task<CarItemModel> Execute(int id);
    }
}
