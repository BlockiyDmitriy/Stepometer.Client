using System;

namespace Stepometer.Models.Login
{
    public class LoginModel
    {
        public Guid Uid { get; set; } = new();
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsExistAccount { get; set; } = false;
    }
}
