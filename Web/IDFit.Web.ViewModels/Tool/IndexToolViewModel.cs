namespace IDFit.Web.ViewModels.Tool
{
    public class IndexToolViewModel
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Details { get; set; }

        public string Url => $"/f/{this.Name.Replace(' ', '-')}";
    }
}