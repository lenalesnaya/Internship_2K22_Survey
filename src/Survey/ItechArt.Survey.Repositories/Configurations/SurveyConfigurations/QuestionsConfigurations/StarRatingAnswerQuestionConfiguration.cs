using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations.SurveyConfigurations.QuestionsConfigurations;

public class StarRatingAnswerQuestionConfiguration : IEntityTypeConfiguration<StarRatingAnswerQuestion>
{
    public void Configure(EntityTypeBuilder<StarRatingAnswerQuestion> builder)
    {
        builder.HasKey(q => new { q.Id, q.SurveyId });
        builder
            .Property(q => q.Text)
            .HasMaxLength(Question.TextMaxLength)
            .IsRequired();
        builder
            .Property(q => q.NumberOfStars)
            .IsRequired();
        builder
            .HasOne(q => q.Survey)
            .WithMany(s => s.StarRatingAnswerQuestions)
            .HasForeignKey(q => q.SurveyId)
            .IsRequired();
    }
}