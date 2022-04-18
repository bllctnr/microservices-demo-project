using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public class JsonResult : IJsonResult
    {
        public JsonResult(bool success, string message) : this(success)
        {
            Message = message;
        }

        public JsonResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}
