using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondtNumber}")]
        public IActionResult GetSum(string firstNumber, string secondtNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondtNumber))
            {
                var sum = ConvertToNumeric(firstNumber) + ConvertToNumeric(secondtNumber);

                return Ok(sum.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("sub/{firstNumber}/{secondtNumber}")]
        public IActionResult GetSub(string firstNumber, string secondtNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondtNumber))
            {
                var sub = ConvertToNumeric(firstNumber) - ConvertToNumeric(secondtNumber);

                return Ok(sub.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("mult/{firstNumber}/{secondtNumber}")]
        public IActionResult GetMult(string firstNumber, string secondtNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondtNumber))
            {
                var mult = ConvertToNumeric(firstNumber) * ConvertToNumeric(secondtNumber);

                return Ok(mult.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("div/{firstNumber}/{secondtNumber}")]
        public IActionResult GetDiv(string firstNumber, string secondtNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondtNumber))
            {
                var div = ConvertToNumeric(firstNumber) / ConvertToNumeric(secondtNumber);

                return Ok(div.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("mean/{firstNumber}/{secondtNumber}")]
        public IActionResult GetMean(string firstNumber, string secondtNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondtNumber))
            {
                var mean = (ConvertToNumeric(firstNumber) + ConvertToNumeric(secondtNumber)) / 2;

                return Ok(mean.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("sqrt/{firstNumber}")]
        public IActionResult GetSqrt(string firstNumber)
        {
            if (IsNumeric(firstNumber))
            {
                var mean = Math.Sqrt((double)(ConvertToNumeric(firstNumber)));

                return Ok(mean.ToString());
            }
            return BadRequest("Invalid Input");
        }


        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumeric = double.TryParse(strNumber, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out number);

            return isNumeric;
        }
        private decimal ConvertToNumeric(string strNumber)
        {
            decimal decimalValue;

            if(decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }
            return 0;   
        }


    }
}
