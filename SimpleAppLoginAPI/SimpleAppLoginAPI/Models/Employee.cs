using System.ComponentModel.DataAnnotations;

namespace SimpleAppLoginAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public StatusEnum Status { get; set; }
    }

    public enum StatusEnum
    {
        Active = 1,
        Inactive = 2,
        Deleted = 3
    }
}
