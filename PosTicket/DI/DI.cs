using Dna;
using PosTicket.ViewModel;

namespace PosTicket
{
    public static class DI
    {
        public static ApplicationViewModel ViewModelApplication => Framework.Service<ApplicationViewModel>();
        public static SetDepositViewModel ViewModelSetDeposit => Framework.Service<SetDepositViewModel>();
        public static PosMainViewModel ViewModelPosMain => Framework.Service<PosMainViewModel>();

    }
}
