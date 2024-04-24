using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Porcupine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porcupine.EntityFrameworkCore.EntityFrameworkCore.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(ti => ti.UserName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ti => ti.EmailConfirmed)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ti => ti.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}