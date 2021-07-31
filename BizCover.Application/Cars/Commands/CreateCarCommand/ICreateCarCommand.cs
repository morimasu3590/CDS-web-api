
using System;
using System.Threading.Tasks;

namespace BizCover.Application
{
    public interface ICreateCarCommand
    {
        Task<Tuple<int, CreateCarModel>> Execute(CreateCarModel createCarModel);
    }
}
