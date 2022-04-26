using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations.SurveyConfigurations.AnswersConfigurations;

public class AnswerVariantConfiguration : IEntityTypeConfiguration<AnswerVariant>
{
    public void Configure(EntityTypeBuilder<AnswerVariant> builder)
    {
        builder
            .Property(a => a.Title)
            .HasMaxLength(AnswerVariant.TextMaxLength)
            .IsRequired();
        builder
            .HasOne(a => a.Question)
            .WithMany(q => q.AnswerVariants)
            .HasForeignKey(a => a.QuestionId )
            .IsRequired();
    }
}