using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestApi.Entities;

namespace QuestApi.Data.Configurations;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.Property(e => e.Name)
            .HasMaxLength(200);

        builder.HasOne(d => d.Quest).WithMany(p => p.Answer)
            .HasForeignKey(d => d.QuestId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Quest_Answer");
    }
}