namespace crud_backend.Models.DTO
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Program { get; set; }
        public string address { get; set; }
        public int Age { get; set; }
    }
}
