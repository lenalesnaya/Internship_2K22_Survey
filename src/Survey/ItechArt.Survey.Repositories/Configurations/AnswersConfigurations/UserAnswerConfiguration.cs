using System.ComponentModel.DataAnnotations.Schema;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations.AnswersConfigurations;

public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
{
    public void Configure(EntityTypeBuilder<UserAnswer> builder)
    {
        builder
            .HasOne(q => q.User)
            .WithMany(u => u.UserAnswers)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(a => a.AnswerVariant)
            .WithMany(a => a.UserAnswers)
            .HasForeignKey(u=>u.AnswerVariantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}