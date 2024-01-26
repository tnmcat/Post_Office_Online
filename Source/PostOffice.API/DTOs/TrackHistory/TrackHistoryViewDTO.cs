namespace PostOffice.API.DTOs.TrackHistory
{
	public class TrackHistoryViewDTO
	{
		public int track_id { get; set; }
		public int order_id { get; set; }
		public string? new_status { get; set; }
		public DateTime? update_time { get; set; }
		public string? new_location { get; set; }
		public Guid employee_id { get; set; }
	}
}
