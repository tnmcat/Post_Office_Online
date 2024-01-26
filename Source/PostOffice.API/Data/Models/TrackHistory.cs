using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOffice.API.Data.Models
{
    public class TrackHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int track_id { get; set; }
        public int order_id { get; set; }
        public string? new_status { get; set; }
        public DateTime? update_time { get; set; }
        public string? new_location { get; set; }
        public Guid employee_id { get; set; }

        public ICollection<HistoryEmployee>? HistoryEmployees { get; set; }
        public ParcelOrder? ParcelOrder { get; set; }
    }
}
