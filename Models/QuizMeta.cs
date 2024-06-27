using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseManagementSystemV1.Models
{
    public class QuizMeta
    {

        /*
         * 
         * The Quiz Meta Table can be used to store additional
         * information of tests or quiz including
         * the quiz banner URL etc
         */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        [Required]
        [StringLength(50)]
        public string Key { get; set; }

        public string Content { get; set; }
    }
}
