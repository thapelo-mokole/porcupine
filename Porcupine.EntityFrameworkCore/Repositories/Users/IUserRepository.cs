using Porcupine.Core.Entities;
using Porcupine.EntityFrameworkCore.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porcupine.EntityFrameworkCore.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<User>
    { }
}