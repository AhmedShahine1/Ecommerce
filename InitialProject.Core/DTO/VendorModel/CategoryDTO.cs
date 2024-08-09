using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTO.VendorModel
{
    public class CategoryDTO
    {
        public string Id { get; set; } =Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
