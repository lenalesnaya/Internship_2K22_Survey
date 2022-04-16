using ItechArt.Survey.DomainModel.Survey.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations.SurveyConfigurations.QuestionsConfigurations;

public class TextAnswerQuestionConfiguration : IEntityTypeConfiguration<TextAnswerQuestion>
{
    public void Configure(EntityTypeBuilder<TextAnswerQuestion> builder)
    {
        builder.HasKey(q => new { q.Id, q.SurveyId });
        builder
            .Property(q => q.Text)
            .HasMaxLength(Question.TextMaxLength)
            .IsRequired();
        builder
            .HasOne(q => q.Survey)
            .WithMany(s => s.TextAnswerQuestions)
            .HasForeignKey(q => q.SurveyId)
            .IsRequired();
    }
}