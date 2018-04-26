using System.Data.Entity;
using AvalonServer.Entities;
using MySql.Data.Entity;

namespace AvalonDatalayer
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class AvalonContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }

        // TODO: Use connection string, but it's broken
        public AvalonContext() : base("server=localhost;userid=root;password=password;database=avalon;persistsecurityinfo=True") { }
    }
}