namespace ItsaWeb.Models.Media
{
    public class UpdateMediaViewModel : ShowMediaViewModel
    {
        public UpdateMediaViewModel()
        {
        }

        public UpdateMediaViewModel(Entities.Media media) : base(media)
        {
        }
    }
}