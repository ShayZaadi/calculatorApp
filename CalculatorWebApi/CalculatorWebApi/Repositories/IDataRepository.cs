using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculatorWebApi.Models;

namespace CalculatorWebApi.Repositories
{
    public interface IDataRepository
    {
        Task<IEnumerable<Data>> Get();
        Task<Data> Get(int id);
        Task<Data> Create(Data data);
        Task Update(Data data);
        Task Delete(int id);
    }
}
