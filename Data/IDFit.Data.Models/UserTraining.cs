namespace IDFit.Data.Models
{
    public class UserTraining
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
