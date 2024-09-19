using System;
using System.Collections.Generic;
using System.Text;

namespace ZenMind.DAL.DB
{
    public class TestResultDTO
    {
        public string Patient { get; set; }
        public int UserTestId { get; set; }
        public int TestId { get; set; }
        public int? UserId { get; set; }       
        public int CorrectAnswers { get; set; }
        public int InCorrectAnswers { get; set; }
        public string PerformedOn { get; set; }
        public string Test { get; set; }
    }
}
