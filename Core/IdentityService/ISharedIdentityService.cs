using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IdentityService
{
    public interface ISharedIdentityService
    {
        public string GetUserId();
    }
}
