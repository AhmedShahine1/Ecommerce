using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTO.AuthViewModel.FilesModel
{
    public class ImagesModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Should Enter Name Images"), Display(Name = "Name Images"), StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Should Enter Path Id"), Display(Name = "Path Id"), StringLength(50)]
        public string pathId { get; set; }
    }
}
