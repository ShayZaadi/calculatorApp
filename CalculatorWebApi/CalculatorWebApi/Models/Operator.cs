using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorWebApi.Models
{
    public abstract class Operator
    {
        public static Operator Create(decimal number1, decimal number2, string op)
        {
            if (op == "+")
                return new operatorPlus(number1, number2);

            else if(op == "-")
                return new operatorMinus(number1, number2);

            else if (op == "*")
                return new operatorMul(number1, number2);

            else
                return new operatorDiv(number1, number2);
        }

        public string m_Result { get; set; }

    }

    public class operatorPlus: Operator
    {
        public operatorPlus(decimal number1, decimal number2)
        {
            m_Result = "0";
            m_Result = (number1 + number2).ToString();
        }
    }

    public class operatorMinus : Operator
    {
        public operatorMinus(decimal number1, decimal number2)
        {
            m_Result = "0";
            m_Result = (number1 - number2).ToString();
        }
    }

    public class operatorMul : Operator
    {
        public operatorMul(decimal number1, decimal number2)
        {
            m_Result = "0";
            m_Result = (number1 * number2).ToString();
        }
    }

    public class operatorDiv : Operator
    {
        public operatorDiv(decimal number1, decimal number2)
        {
            if (number2 != 0)
                m_Result = (number1 / number2).ToString();
            else
                m_Result = "Infinity";
        }
    }
}
