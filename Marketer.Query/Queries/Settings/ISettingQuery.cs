using System.Threading.Tasks;

namespace Marketer.Query.Queries.Settings
{
    public interface ISettingQuery
    {
        Task<string> GetSummaryText();
    }
}