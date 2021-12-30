using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Extera;
using Marketer.Domain.Entities.Extera;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Extera
{
    public interface ISettingRepository : IRepository<Setting>
    {
        Task<Setting> GetEntity();
        Task<SettingVM> GetSetting();
    }
}