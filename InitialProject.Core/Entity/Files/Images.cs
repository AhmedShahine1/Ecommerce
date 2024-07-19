using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Core.Entity.Files
{
    public class Images
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        [ForeignKey("path")]
        public string pathId { get; set; }

        public Paths path { get; set; }
    }
}