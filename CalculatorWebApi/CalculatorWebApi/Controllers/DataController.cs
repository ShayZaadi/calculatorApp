using CalculatorWebApi.Models;
using CalculatorWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository m_DataRepository;

        public DataController(IDataRepository dataRepository)
        {
            m_DataRepository = dataRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<object>> GetData()
        {
            string message = "";
            IEnumerable<Data> allData = null;

            try
            {
                allData = await m_DataRepository.Get();
                message = "The GetData was succesfully";
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }

            return new List<object>()
            {
                {allData },
                {message }
            };

        }

        [HttpPost]
        public async Task<IEnumerable<object>> Create([FromBody] Data data)
        {
            string message = "";
            Data updatedData = null;

            try
            {
                updatedData = await m_DataRepository.Create(data);
                message = "The Create was succesfully";
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }

            return new List<object>()
            {
                { updatedData } ,
                { message}
            };
        }

        [HttpPut]
        public async Task<IEnumerable<object>> Update(int id, [FromBody] Data data)
        {
            string messaage = "";

            try
            {
                if (id != data.Id)
                {
                    messaage = "Bad Request!";
                    return new List<object>()
                    {
                        { messaage } 
                    };
                }

                await m_DataRepository.Update(data);
                messaage = "The Update was succesfully";
            }
            catch (Exception ex)
            {
                messaage = ex.ToString();
            }

            return new List<object>()
            {
                { messaage }
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            string message = "";

            try
            {
                var dataToDelete = await m_DataRepository.Get(id);
                //check if the data exist in db
                if (dataToDelete == null)
                    return NotFound();

                await m_DataRepository.Delete(dataToDelete.Id);
                message = "The Delete was succesfully";
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }

            return Content(message);
        }

        [HttpPost("Calculate")]
        public ActionResult<IEnumerable<object>> Calculate([FromBody] Data data)
        {
            string message = "";
            try
            {
                data.Result = Operator.Create(decimal.Parse(data.Input1), decimal.Parse(data.Input2), data.Operator).m_Result.ToString();
                message = "The calculation was succesfully";
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }

            return new List<object>()
            {
                { data } ,
                { message}
            };
        }

    }
}