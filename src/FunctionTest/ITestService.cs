using System.Threading.Tasks;
using MyLab.ApiClient;

namespace FunctionTest
{
    [Api("api")]
    public interface ITestService
    {
        [Get("echo")]
        Task<string> Echo([Query]string message);
    }
}
