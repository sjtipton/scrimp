namespace scrimp.Dtos
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int? ParentId { get; set; }
        public bool IsTransfer { get; set; }
        public int UserId { get; set; }
    }
}
