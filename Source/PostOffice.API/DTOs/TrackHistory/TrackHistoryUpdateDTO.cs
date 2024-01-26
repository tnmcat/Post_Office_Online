namespace PostOffice.API.DTOs.TrackHistory
{
	public class TrackHistoryUpdateDTO
	{
		public int order_id { get; set; }
		public string? new_status { get; set; }
		public DateTime? update_time { get; set; }
		public string? new_location { get; set; }	  		
	}
}
