using System.Collections.Generic;

namespace PosTicket.Repository.Interface
{
    public interface IConfigRepository
    {
        IEnumerable<Config> GetConfigs();
        void AddConfigs(IEnumerable<Config> newConfig);
        void UpdateConfigByField(string field, string values);
    }
}
