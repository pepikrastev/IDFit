namespace IDFit.Data.Models
{
    public class ExerciseTool
    {
       // public int ExerciseToolId { get; set; }

        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        public int ToolId { get; set; }
        public virtual Tool Tool { get; set; }
    }
}
