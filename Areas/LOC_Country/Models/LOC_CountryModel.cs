using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nice_Admin_Panal.Areas.LOC_Country.Models
{
    public class LOC_CountryModel
    {
        public int CountryID { get; set; }
        [Required]
        public string CountryName { get; set; } = string.Empty;
        [Required]
        public string CountryCode { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
    public class LOC_CountryDropDownModel
    {
        public int? CountryID { get; set; }
        public string? CountryName { get; set; }
    }
    public class LOC_CountryFilterModel
    {
        public int? CountryID { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }
    }
}
