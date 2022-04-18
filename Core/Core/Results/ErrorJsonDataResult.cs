using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public class ErrorJsonDataResult<T> : JsonDataResult<T>
    {
        public ErrorJsonDataResult(T data, string message) : base(data, false, message)
        {

        }

        public ErrorJsonDataResult(T data) : base(data, false)
        {

        }

        public ErrorJsonDataResult(string message) : base(default, false, message)
        {

        }

        public ErrorJsonDataResult() : base(default, false)
        {

        }
    }
}
