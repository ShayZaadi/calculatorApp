using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorWebApi.Models
{
    public class Data
    {
        public int Id { get; set; }
        public string Input1 { get; set; }
        public string Input2 { get; set; }
        public string Operator { get; set; }
        public string Result { get; set; }
    }
}
