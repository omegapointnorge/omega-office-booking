namespace server.Models.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        
        public User(int id, string name, string email, List<Booking> bookings)
        {
            Id = id;
            Name = name;
            Email = email;
            Bookings = bookings;
        }
    }
}