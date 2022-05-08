using Stepometer.Models.Login;
using System.Threading.Tasks;

namespace Stepometer.Service.LoginServices
{
    public interface ILoginService
    {
        public Task<bool> CreateNewAccount(RegisterModel registerModel);
    }
}
