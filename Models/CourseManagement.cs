﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace courseManagementSystemV1.Models
{
    public class CourseManagement
    {
        // ممكن الكورس يقدمه اكتر من انستاركتور

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int courseMangID { get; set; }


        public int courseID { get; set; }
        [ForeignKey("courseID")]
        public Course course { get; set; }


        public int instructorID { get; set; }
        [ForeignKey("instructorID")]
        public Instructor instructor { get; set; }


        public DateTime DateTime { get; set; } = DateTime.UtcNow;


    }

}