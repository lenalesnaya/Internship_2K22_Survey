using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations.SurveyConfigurations.QuestionsConfigurations;

public class StarRatingAnswerQuestionConfiguration : IEntityTypeConfiguration<StarRatingAnswerQuestion>
{
    public void Configure(EntityTypeBuilder<StarRatingAnswerQuestion> builder)
    {
        builder
            .Property(q => q.Title)
            .HasMaxLength(Question.TextMaxLength)
            .IsRequired();
        builder
            .Property(q => q.AmountOfStars)
            .IsRequired();
        builder
            .HasOne(q => q.Survey)
            .WithMany(s => s.StarRatingAnswerQuestions)
            .HasForeignKey(q => q.SurveyId)
            .IsRequired();
    }
}