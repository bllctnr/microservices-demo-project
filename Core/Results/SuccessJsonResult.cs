using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public class SuccessJsonResult : JsonResult
    {
        public SuccessJsonResult(string message) : base(true, message)
        {

        }

        public SuccessJsonResult() : base(true)
        {

        }
    }
}