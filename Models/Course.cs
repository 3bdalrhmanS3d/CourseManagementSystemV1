﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseManagementSystemV1.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public float CourseTime { get; set; }

        public string CourseRequirements { get; set; } = "None";
        public DateTime CourseStartDate { get; set; }
        public DateTime? CourseEndDate { get; set; }
        
        public string? CoursePhoto { get; set; }

        public bool? IsAvailable { get; set; } = false;

        public string? whoAcceptedCourse { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<CourseManagement>? CourseManagements { get; set; }
        public ICollection<Bonus>? Bonuses { get; set; }
        public ICollection<HRManagement>? hRManagements { get; set; }


        

    }
}
