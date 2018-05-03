
namespace AvalonServer.Entities
{

    public interface IAccount
    {
        int Id { get; set; }

        int PlayerId { get; set; }
        string Username { get; set; }
        string Password { get; set; }    
    }
}