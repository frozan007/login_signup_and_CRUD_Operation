using System.ComponentModel.DataAnnotations;

namespace SimpleAppLoginAPI.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        public int EmpId { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string FileName { get; set; }
    }
}
