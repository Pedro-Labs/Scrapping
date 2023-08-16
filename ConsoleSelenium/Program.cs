using ConsoleSelenium.Job;
using System.Threading.Tasks;

namespace ConsoleSelenium
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await Scrapping.MREstoque();
            ConsoleSelenium.Job.SAVarejo.Scrapping.Data();
        }
    }
}
