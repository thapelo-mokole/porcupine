using Porcupine.Core.Entities;
using Porcupine.EntityFrameworkCore.EntityFrameworkCore;
using Porcupine.EntityFrameworkCore.Repositories.Base;

namespace Porcupine.EntityFrameworkCore.Repositories.Groups
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(DatabaseContext context) : base(context)
        {
        }
    }
}