using Framework.Application;
using Marketer.Application.Contract.ViewModels.Extera;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Extera
{
    public interface ISettingApplication
    {
        Task<SettingVM> GetSetting();
        Task<OperationResult> Modify(SettingVM command);
    }
}
