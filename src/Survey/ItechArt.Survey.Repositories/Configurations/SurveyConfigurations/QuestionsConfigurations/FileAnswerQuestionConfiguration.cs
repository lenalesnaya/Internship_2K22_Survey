using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations.SurveyConfigurations.QuestionsConfigurations;

public class FileAnswerQuestionConfiguration : IEntityTypeConfiguration<FileAnswerQuestion>
{
    public void Configure(EntityTypeBuilder<FileAnswerQuestion> builder)
    {
        builder.HasKey(q => new { q.Id, q.SurveyId });
        builder
            .Property(q => q.Text)
            .HasMaxLength(Question.TextMaxLength)
            .IsRequired();
        builder
            .HasOne(q => q.Survey)
            .WithMany(s => s.FileAnswerQuestions)
            .HasForeignKey(q => q.SurveyId)
            .IsRequired();
    }
}