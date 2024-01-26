using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Context;
using PostOffice.API.DTOs.Common;
using PostOffice.API.DTOs.ParcelOrder;
using PostOffice.API.DTOs.TrackHistory;
using PostOffice.API.DTOs.User;
using PostOffice.API.Data.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PostOffice.API.Repositorities.TrackHistory
{
	public class HistoryRepository : IHistoryRepository
	{
		private readonly AppDbContext _context;

		public HistoryRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<List<TrackHistoryViewDTO>> GetHistoryByOrderId(int id)
		{

			var query = from t in _context.TrackHistories where t.order_id ==id select t;
			
			//3. Paging
			int totalRow = await query.CountAsync();

			if(totalRow == 0)
			{
			
			}
			var data = await query.Select(p => new TrackHistoryViewDTO()
			{
				track_id = p.track_id,
				order_id = p.order_id,
				new_status =p.new_status,
				update_time =p.update_time,
				new_location = p.new_location,
				employee_id = p.employee_id
			}).ToListAsync();

			return data;
		}
		
	}
}
