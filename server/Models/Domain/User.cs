namespace server.Models.Domain
{
    public class User
    {
        public String Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Booking> Bookings { get; set; } = new List<Booking>();

        public User(String id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public User(String id, string name, string email, List<Booking> booking)
        {
            Id = id;
            Name = name;
            Email = email;
            Bookings = booking;
        }
        
        public User()
        {
            Id = string.Empty;
        }
    }
}