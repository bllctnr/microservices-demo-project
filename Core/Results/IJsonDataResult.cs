using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public interface IJsonDataResult<T> : IJsonResult
    {
        T Data { get; }
    }
}
