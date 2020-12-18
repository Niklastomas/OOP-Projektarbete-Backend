namespace OOP_Projektarbete_Backend.Models
{
    public class MovieInfo
    {
        public int Page { get; set; }
        public Result[] Results { get; set; }
        public int Total_pages { get; set; }
        public int Total_results { get; set; }
    }
}