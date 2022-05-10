using Stepometer.Models.Login;
using System.Threading.Tasks;

namespace Stepometer.Service.LoginServices
{
    public interface ILoginService
    {
        public Task<bool> CreateNewAccount(RegisterModel registerModel);
        public Task<bool> Login(LoginModel registerModel);
        public Task<string> GetToken(LoginModel loginModel);
    }
}
