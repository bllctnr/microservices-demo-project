using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IdentityService
{
    public class SharedIdentityManager : ISharedIdentityService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public SharedIdentityManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId() 
        {
            return _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
        }
    }
}
