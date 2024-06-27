using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace courseManagementSystemV1.Models
{
    public class CourseSpecificQuestions
    {

        /*
         هنا الاسئلة الخاصة بتكون ظاهرة لل منتور و
        الانتساركتور بس ولو السؤال مفيد هيخليه يظهر لكل الناس 

         
         */

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseSpecificQuestionsID { get; set; }

        [Required]
        [StringLength(500)]
        public string CourseSpecificQuestionsText { get; set; }

        public string? CourseSpecificQuestionsphoto { get; set; }
        public DateTime? CourseSpecificQuestionsDate { get; set; }
        public DateTime? CourseSpecificQuestionsUpdate { get; set; }
        // if false is hide
        public bool CourseSpecificQuestionsStates {  get; set; } = false;

        public int ? WhopublushedQuestions { get; set; }

        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        public int CourseID { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }



        public int? CourseSpecificQuestionsParentID { get; set; }
        [ForeignKey("CourseSpecificQuestionsParentID")]
        public Comments comments { get; set; }
    }
}
