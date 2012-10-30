namespace ItsaWeb.Models
{
    public class UserViewModel
    {
        public bool AllowComments { get; set; }
        public bool ModerateComments { get; set; }
        public string UserName { get; set; }
        public string BlogTitle { get; set; }
        public string BlogSubTitle { get; set; }
    }
}