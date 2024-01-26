using PostOffice.API.DTOs.Common;
using PostOffice.API.DTOs.TrackHistory;

namespace PostOffice.API.Repositorities.TrackHistory
{
	public interface IHistoryRepository
	{
		Task<List<TrackHistoryViewDTO>> GetHistoryByOrderId(int id);			
	}
}
