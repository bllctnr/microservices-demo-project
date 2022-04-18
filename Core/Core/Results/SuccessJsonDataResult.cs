using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public class SuccessJsonDataResult<T> : JsonDataResult<T>
    {
        public SuccessJsonDataResult(T data, string message) : base(data, true, message)
        {

        }

        public SuccessJsonDataResult(T data) : base(data, true)
        {

        }

        SuccessJsonDataResult(string message) : base(default, true, message)
        {

        }

        public SuccessJsonDataResult() : base(default, true)
        {

        }

    }
}