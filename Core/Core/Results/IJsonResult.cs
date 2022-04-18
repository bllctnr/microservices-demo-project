﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public interface IJsonResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
