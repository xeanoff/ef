namespace ef
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}