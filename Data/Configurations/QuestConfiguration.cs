using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestApi.Entities;

namespace QuestApi.Data.Configurations;

public class QuestConfiguration : IEntityTypeConfiguration<Quest>
{
    public void Configure(EntityTypeBuilder<Quest> builder) 
    {
        builder.Property(e => e.Name)
            .HasMaxLength(200);
    }
}