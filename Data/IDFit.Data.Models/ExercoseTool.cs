﻿namespace IDFit.Data.Models
{
    public class ExercoseTool
    {
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public int ToolId { get; set; }
        public Tool Tool { get; set; }
    }
}
