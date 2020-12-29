namespace OOP_Projektarbete_Backend.Models
{
    public class MovieInfo
    {
        public int Page { get; set; }
        public MovieInfoResult[] Results { get; set; }
        public int Total_pages { get; set; }
        public int Total_results { get; set; }
    }

    public class MovieInfoResult
    {
        public string Poster_path { get; set; }
        public float Vote_average { get; set; }
        public string Overview { get; set; }
        public string Release_date { get; set; }
        public int Vote_count { get; set; }
        public int Id { get; set; }
        public int[] Genre_ids { get; set; }
        public string Title { get; set; }
        public string Original_language { get; set; }
    }
}