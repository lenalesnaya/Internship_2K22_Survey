using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations.QuestionsConfigurations;

public class FileAnswerQuestionConfiguration : IEntityTypeConfiguration<FileAnswerQuestion>
{
    public void Configure(EntityTypeBuilder<FileAnswerQuestion> builder)
    {
        builder
            .Property(q => q.Title)
            .HasMaxLength(Question.TextMaxLength)
            .IsRequired();
        builder
            .HasOne(q => q.Survey)
            .WithMany(s => s.FileAnswerQuestions)
            .HasForeignKey(q => q.SurveyId)
            .IsRequired();
    }
}