﻿using System.Collections.Generic;
using System.Linq;
using Application.Core.Data;

namespace LMP.Models.KnowledgeTesting
{
    public class Test : ModelBase
    {
        public int? TestNumber { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int TimeForCompleting { get; set; }

        public bool SetTimeForAllTest { get; set; }

        public int SubjectId { get; set; }

        public int CountOfQuestions { get; set; }

        public bool IsNecessary { get; set; }

        public bool ForSelfStudy { get; set; }

        public bool BeforeEUMK { get; set; }

        public bool ForEUMK { get; set; }

        public bool ForNN { get; set; }

        public string Data { get; set; }

        public bool Unlocked => ForSelfStudy || TestUnlocks is { } && TestUnlocks.Any();

        public Subject Subject { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<TestUnlock> TestUnlocks { get; set; }
    }
}