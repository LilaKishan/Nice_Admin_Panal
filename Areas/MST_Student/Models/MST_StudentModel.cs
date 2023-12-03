using System.ComponentModel.DataAnnotations;

namespace Nice_Admin_Panal.Areas.MST_Student.Models
{
    public class MST_StudentModel
    {
        public int? StudentID { get; set; }
        [Required(ErrorMessage ="select branch")]
        public int? BranchID { get; set; }
        [Required(ErrorMessage = "select city")]
        public int? CityID { get; set; }
        [Required]
        public string? StudentName { get; set; }
        public string? MobileNoStudent { get; set; }
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? MobileNoFather { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public string? Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
    public class MST_StudentFilterModel
    {
        public int? StudentID { get; set; }
        public string? StudentName { get; set; }
        public int? CityID { get; set; }
        public int? BranchID { get; set; }
    }
}
