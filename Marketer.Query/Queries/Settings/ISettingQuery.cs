using System.Threading.Tasks;

namespace Marketer.Query.Queries.Settings
{
    public interface ISettingQuery
    {
        Task<SettingQueryVM> Get();
        Task<string> GetSummaryText();
        Task<string> GetAboutUsTextAsync();
    }
}