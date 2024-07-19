namespace Ecommerce.Core.Entity.Files
{
    public class Paths
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
    }
}