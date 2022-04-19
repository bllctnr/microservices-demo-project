using Core.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ControllerBases
{
    public class CustomControllerBase : ControllerBase
    {
        // Automatin status code implementation

        public IActionResult CreateActionResultInstance<T>(IJsonDataResult<T> response) 
        {
            return new ObjectResult(response)
            {
                StatusCode = null
            };
        }

    }
}
