using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleSeo.Services
{
    public interface ISampleDataStore
    {
        Task<IEnumerable<Fruit>> GetFruitsAsync();
    }
}