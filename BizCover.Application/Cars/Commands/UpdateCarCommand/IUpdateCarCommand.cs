using System.Threading.Tasks;

namespace BizCover.Application
{
    public interface IUpdateCarCommand
    {
        Task Execute(int id, UpdateCarModel updateCarModel);
    }
}
