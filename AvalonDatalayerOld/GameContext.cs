using System.Data.Entity;
using AvalonServer.Entities;

namespace AvalonDatalayer
{
    class GameContext: DbContext
    {
        public DbSet<Game> Games { get; set; }
    }
}
