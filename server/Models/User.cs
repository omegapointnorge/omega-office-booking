namespace server.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Booking> Bookings { get; set; } = new List<Booking>();

        public User(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}