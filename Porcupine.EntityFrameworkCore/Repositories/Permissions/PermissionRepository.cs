using Porcupine.Core.Entities;
using Porcupine.EntityFrameworkCore.EntityFrameworkCore;
using Porcupine.EntityFrameworkCore.Repositories.Base;

namespace Porcupine.EntityFrameworkCore.Repositories.Permissions
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(DatabaseContext context) : base(context)
        {
        }
    }
}