using Microsoft.AspNetCore.Identity;
using Porcupine.Core.Common;

namespace Porcupine.Core.Entities
{
    public class User : BaseEntity, IAuditedEntity
    {
        [ProtectedPersonalData]
        public virtual string? UserName { get; set; }

        [ProtectedPersonalData]
        public virtual string? Email { get; set; }

        public virtual string? Name { get; set; }
        public virtual string? Surname { get; set; }
        public virtual string? NormalizedEmail { get; set; }

        [PersonalData]
        public virtual bool EmailConfirmed { get; set; }

        public override string ToString()
            => UserName ?? string.Empty;

        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}