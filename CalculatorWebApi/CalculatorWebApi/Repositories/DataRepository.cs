using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculatorWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculatorWebApi.Repositories
{
    public class DataRepository: IDataRepository
    {
        private readonly DataContext m_Context;

        public DataRepository(DataContext context)
        {
            m_Context = context;
        }

        public async Task<Data> Create(Data data)
        {
            m_Context.DetaSet.Add(data);
            await m_Context.SaveChangesAsync();

            return data;
        }

        public async Task Delete(int id)
        {
            var detailToDelete = await m_Context.DetaSet.FindAsync(id);
            m_Context.DetaSet.Remove(detailToDelete);
            await m_Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Data>> Get()
        {
            return await m_Context.DetaSet.ToListAsync();
        }

        public async Task<Data> Get(int id)
        {
            return await m_Context.DetaSet.FindAsync(id);
        }

        public async Task Update(Data data)
        {
            m_Context.Entry(data).State = EntityState.Modified;
            await m_Context.SaveChangesAsync();
        }

    }
}
