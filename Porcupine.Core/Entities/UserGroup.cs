using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porcupine.Core.Entities
{
    [Table("UserGroups")]
    public class UserGroup
    {
        public Guid UserId { get; set; }

        public Guid GroupId { get; set; }
    }
}