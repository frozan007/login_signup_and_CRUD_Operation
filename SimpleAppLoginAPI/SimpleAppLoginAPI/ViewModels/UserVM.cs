using SimpleAppLoginAPI.Models;

namespace SimpleAppLoginAPI.ViewModels
{    
    public class UserLoginModel
    {
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }        
    }
    public class LoginResponseModel
    {
        public virtual User User { get; set; }
        public virtual bool IsSuccess { get; set; }
        public virtual string Response { get; set; }
    }  
}
