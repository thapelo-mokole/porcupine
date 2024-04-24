using Porcupine.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porcupine.Core.Entities
{
    public class Group : BaseEntity, IAuditedEntity
    {
        public virtual string? ShortDescription { get; set; }
        public virtual string? LongDescription { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}