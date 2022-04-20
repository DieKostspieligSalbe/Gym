namespace Gym.MVC.Models
{
    public class TrainingProgramSaveModel
    {   
        public string[] IdList { get; set; }
        public string Intensity { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; }
        public bool IsPublic { get; set; }
    }
}
