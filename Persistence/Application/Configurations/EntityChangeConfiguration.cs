using Cemiyet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cemiyet.Persistence.Application.Configurations
{
    public class EntityChangeConfiguration : IEntityTypeConfiguration<EntityChange>
    {
        public void Configure(EntityTypeBuilder<EntityChange> builder)
        {
            builder.HasNoKey();
            builder.HasIndex(ec => ec.EntityId);
            builder.Property(ec => ec.EntityId).IsRequired();
            builder.Property(ec => ec.PropertyName).IsRequired();
            builder.Property(ec => ec.OldValue).IsRequired();
            builder.Property(ec => ec.NewValue).IsRequired();
            builder.Property(ec => ec.ModificationDate).IsRequired();
            builder.Property(ec => ec.ModifierId).IsRequired();
        }
    }
}
