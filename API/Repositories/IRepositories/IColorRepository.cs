using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Repositories.IRepositories
{
    public interface IColorRepository
    {
        Task<IEnumerable<Color>> GetColors();
        Task<Color> GetColor(int colorId);
    }
}
