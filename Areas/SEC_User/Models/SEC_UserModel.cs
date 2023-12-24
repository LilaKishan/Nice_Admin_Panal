using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nice_Admin_Panal.Areas.SEC_User.Models
{
    public class SEC_UserModel
    {

        public int? UserID { get; set; }
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }

        public string? PhotoPath { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
