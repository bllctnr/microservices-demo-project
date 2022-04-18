using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public class ErrorJsonResult : JsonResult
    {
        public ErrorJsonResult(string message) : base(false, message)
        {

        }

        public ErrorJsonResult() : base(false)
        {

        }
    }
}
