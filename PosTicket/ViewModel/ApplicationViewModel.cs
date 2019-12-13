using System;
using System.Collections.Generic;
using System.Text;
using PosTicket.DataModels;

namespace PosTicket.ViewModel
{
    public class ApplicationViewModel : NavigableControlViewModel
    {
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.MainPos;
        public ApplicationViewModel()
        {
        }
        public NavigableControlViewModel CurrentPageViewModel { get; set; }
        public void GoToPage(ApplicationPage page, NavigableControlViewModel viewModel = null)
        {
            CurrentPageViewModel = viewModel;
            // See if page has changed
            var different = CurrentPage != page;
            // Set the current page
            CurrentPage = page;

            // If the page hasn't changed, fire off notification
            // So pages still update if just the view model has changed
            if (!different)
                OnPropertyChanged(nameof(CurrentPage));

        }
    }
}
