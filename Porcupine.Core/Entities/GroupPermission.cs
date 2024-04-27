using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porcupine.Core.Entities
{
    [Table("GroupPermissions")]
    public class GroupPermission
    {
        public Guid GroupId { get; set; }

        public Guid PermissionId { get; set; }
    }
}