using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SampleSeo.Services
{
    public class SampleDataStore : ISampleDataStore
    {
        public async Task<IEnumerable<Fruit>> GetFruitsAsync()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Processing...");
            });

            return new[]
            {
                new Fruit
                {
                    Id = 345,
                    Name = "apple"
                },
                new Fruit
                {
                    Id = 222,
                    Name = "orange"
                },
                new Fruit
                {
                    Id = 666,
                    Name = "watermelon"
                }
            };
        }
    }
}