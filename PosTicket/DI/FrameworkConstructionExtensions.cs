using Dna;
using Microsoft.Extensions.DependencyInjection;
using PosTicket.ViewModel;

namespace PosTicket
{
    public static class FrameworkConstructionExtensions
    {
        public static FrameworkConstruction AddPosTicketViewModels(this FrameworkConstruction construction)
        {
            // Bind to a single instance of Application view model
            construction.Services.AddSingleton<ApplicationViewModel>();

            // Bind to a single instance of Settings view model
            construction.Services.AddSingleton<SetDepositViewModel>();

            construction.Services.AddSingleton<PosMainViewModel>();

            // Return the construction for chaining
            return construction;
        }
    }
}
