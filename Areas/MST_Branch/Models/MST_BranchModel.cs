using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nice_Admin_Panal.Areas.MST_Branch.Models
{
    public class MST_BranchModel
    {

        public int? BranchID { get; set; }
        [Required, DisplayName("branchname")]
        public string? BranchName { get; set; }
        [Required, DisplayName("BranchCode")]
        public string? BranchCode { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
    public class MST_BranchDropDownModel
    {
        public int BranchID { get; set; }
        public string? BranchName { get; set; }
    }
    public class MST_BranchFilterModel
    {
        public int BranchID { get; set; }
        public string? BranchName { get; set; }
        public string? BranchCode { get; set; }
    }
}
