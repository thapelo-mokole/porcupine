using System;
using System.Collections.Generic;
using System.Text;

namespace Porcupine.Core.Shared.Utils.Interface
{
    public interface IUtilityService
    {
        string GetUserId();

        string GetClaim(string key);
    }
}