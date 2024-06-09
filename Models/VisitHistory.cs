using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace courseManagementSystemV1.Models
{
    public class VisitHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VisitHistoryId { get; set; }
        public DateTime VisitHistoryDate { get; set; } = DateTime.Now;
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

    }
}
