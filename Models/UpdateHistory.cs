using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace courseManagementSystemV1.Models
{
    public class UpdateHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int updateHistoryID { get; set; }
        public int updateHistoryBy { get; set; }
        public DateTime updateHistoryTime { get; set; } 
        public string? currentState { get; set; }
        public string? previouState { get; set; }

        public string? curruntRole { get; set; }
        
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
