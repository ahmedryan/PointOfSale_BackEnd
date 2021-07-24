using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Services.IServices
{
    public interface ITokenService
    {
        Task<string> CreateToken(IdentityUser user);
    }
}
