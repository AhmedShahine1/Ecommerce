using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTO.AuthViewModel.CityModel
{
    public class CityModel
    {
        [StringLength(50)]
        [Display(Name = "اسم المدينة ")]
        [Required(ErrorMessage = "اسم المدينة مطلوب")]
        public string NameAr { get; set; }

        [StringLength(50)]
        [Display(Name = " اسم المدينة بالانجليزي")]
        [Required(ErrorMessage = "اسم المدينة بالانجليزي مطلوب")]
        public string NameEn { get; set; }

        [StringLength(50)]
        [Display(Name = " اسم الدولة")]
        public string CountryEn { get; set; }

        [StringLength(50)]
        [Display(Name = " اسم الدولة بالانجليزي")]
        public string CountryAr { get; set; }
    }
}
