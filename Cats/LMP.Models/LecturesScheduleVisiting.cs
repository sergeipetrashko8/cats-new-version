﻿using System;
using System.Collections.Generic;
using Application.Core.Data;

namespace LMP.Models
{
    public class LecturesScheduleVisiting : ModelBase
    {
        public DateTime Date { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        public ICollection<LecturesVisitMark> LecturesVisitMarks { get; set; }
    }
}