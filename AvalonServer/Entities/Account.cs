
namespace AvalonServer.Entities
{

    public class Account
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }    
    }
}