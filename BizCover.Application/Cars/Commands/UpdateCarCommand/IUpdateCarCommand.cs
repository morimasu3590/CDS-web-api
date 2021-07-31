using System.Threading.Tasks;

namespace BizCover.Application
{
    public interface IUpdateCarCommand
    {
        Task Execute(UpdateCarModel updateCarModel);
    }
}
