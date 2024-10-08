﻿using Ecommerce.Core.Entity.ApplicationData;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Ecommerce.Core.Entity.Others
{
    [DebuggerDisplay("{NameEn,nq}")]
    public class City : BaseEntity
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

        [Display(Name = " عرض المدينه")]
        public bool IsShow { get; set; } = true;

        public ICollection<ApplicationUser>? Users { get; set; }
    }
}