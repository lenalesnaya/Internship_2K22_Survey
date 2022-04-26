using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations.SurveyConfigurations.QuestionsConfigurations;

public class TextAnswerQuestionConfiguration : IEntityTypeConfiguration<TextAnswerQuestion>
{
    public void Configure(EntityTypeBuilder<TextAnswerQuestion> builder)
    {
        builder
            .Property(q => q.Title)
            .HasMaxLength(Question.TextMaxLength)
            .IsRequired();
        builder
            .HasOne(q => q.Survey)
            .WithMany(s => s.TextAnswerQuestions)
            .HasForeignKey(q => q.SurveyId)
            .IsRequired();
    }
}