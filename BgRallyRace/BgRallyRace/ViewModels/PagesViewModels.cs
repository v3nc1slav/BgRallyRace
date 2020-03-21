namespace BgRallyRace.ViewModels
{
    using System;

    public class PagesViewModels
    {
        public int CurrentPage { get; set; }

        public int PreviousPage => CurrentPage - 1;

        public int NextPage => CurrentPage + 1;

        public bool PreviousDisabled => CurrentPage == 1;

        public int Total { get; set; }

        public bool NextDisabled
        {
            get
            {
                var maxPage = Math.Ceiling(((double)Total) / 10);
                return maxPage == CurrentPage;
            }
        }
    }
}
