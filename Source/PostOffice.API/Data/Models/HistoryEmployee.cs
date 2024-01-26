namespace PostOffice.API.Data.Models
{
    public class HistoryEmployee
    {
        public int track_id { get; set; }
        public Guid employee_id { get; set; }   

        public TrackHistory TrackHistory { get; set; }
        public AppUser Employee { get; set; }

    }
}
