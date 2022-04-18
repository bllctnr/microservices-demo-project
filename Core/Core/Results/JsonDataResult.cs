using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public class JsonDataResult<T> : JsonResult, IJsonDataResult<T>
    {
        public JsonDataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }

        public JsonDataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
