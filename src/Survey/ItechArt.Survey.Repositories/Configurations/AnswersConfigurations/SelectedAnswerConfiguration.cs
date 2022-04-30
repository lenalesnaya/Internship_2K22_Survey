using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations.AnswersConfigurations;

public class SelectedAnswerConfiguration : IEntityTypeConfiguration<SelectedAnswer>
{
    public void Configure(EntityTypeBuilder<SelectedAnswer> builder)
    {
        builder
            .HasOne(q => q.User)
            .WithMany(u => u.SelectedAnswers);
        builder
            .HasOne(a => a.AnswerVariant);
    }
}