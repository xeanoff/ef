using Microsoft.EntityFrameworkCore;

namespace ef
{
    public class MusicAppDbContext : DbContext
    {
        public MusicAppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MusicApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Counrty> Countries { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            MusicAppDbContext context = new();

            Playlist pl1 = new()
            {
                Name = "Lil peep bangers",
                Category = "Emo-rap",
            };

            Playlist pl2 = new()
            {
                Name = "Soul",
                Category = "Alternative Rock",
            };

            Counrty usa = new Counrty() { Name = "USA" };

            Artist peep = new()
            {
                FirstName = "Gustav",
                LastName = "Ar",
                Country = usa,
            };

            Genre alt = new Genre() { Name = "Alternative Rock" };

            Album album = new()
            {
                Name = "Come Over When You're Sober, Pt.1",
                Year = 2017,
                Genre = alt,
                Artist = peep,
            };

            List<Song> songs = new()
            {
                new Song()
                {
                    Name = "Benz Truck",
                    Duration = 2.39,
                    Album = album,
                    Playlists = new List<Playlist>() { pl1 }
                },

                new Song()
                {
                    Name = "Save That Shit",
                    Duration = 3.51,
                    Album = album,
                    Playlists = new List<Playlist>() { pl2 }
                },

                new Song()
                {
                    Name = "Awful Things",
                    Duration = 3.34,
                    Album = album,
                    Playlists = new List<Playlist>() { pl1 }
                },

                new Song()
                {
                    Name = "U said",
                    Duration = 3.44,
                    Album = album,
                    Playlists = new List<Playlist>() { pl2 }
                },

                new Song()
                {
                    Name = "Better Off (Dying)",
                    Duration = 2.34,
                    Album = album,
                    Playlists = new List<Playlist>() { pl1 }
                },

                new Song()
                {
                    Name = "The Brightside",
                    Duration = 3.36,
                    Album = album,
                    Playlists = new List<Playlist>() { pl2 }
                },

                new Song()
                {
                    Name = "Problems",
                    Duration = 3.29,
                    Album = album,
                    Playlists = new List<Playlist>() { pl2 }
                }
            };
            album.Songs = songs;
            peep.Albums = new List<Album>() { album };

            pl1.Songs = new List<Song>() { songs[0], songs[2], songs[4] };
            pl2.Songs = new List<Song>() { songs[1], songs[3], songs[5], songs[6] };

            context.Genres.Add(alt);
            context.SaveChanges();

            context.Countries.Add(usa);

            context.Albums.Add(album);

            context.Playlists.Add(pl1);
            context.Playlists.Add(pl2);

            context.Artists.Add(peep);

            foreach(Song song in songs)
            {
                context.Songs.Add(song);
            }

            context.SaveChanges();
        }
    }
}