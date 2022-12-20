using System.ComponentModel.DataAnnotations;

namespace SimpleAppLoginAPI.Models
{
    public class Signup
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public enum GenderEnum
    {
        Male = 1,
        Female = 2
    }
}
