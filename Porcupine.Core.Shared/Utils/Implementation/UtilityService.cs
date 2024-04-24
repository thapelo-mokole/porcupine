using Microsoft.AspNetCore.Http;
using Porcupine.Core.Shared.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Porcupine.Core.Shared.Utils.Implementation
{
    public class UtilityService : IUtilityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UtilityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return GetClaim(ClaimTypes.NameIdentifier);
        }

        public string GetClaim(string key)
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(key)?.Value;
        }
    }
}