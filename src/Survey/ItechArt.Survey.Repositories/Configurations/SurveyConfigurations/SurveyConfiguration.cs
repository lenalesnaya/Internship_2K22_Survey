using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations.SurveyConfigurations;

public class SurveyConfiguration : IEntityTypeConfiguration<DomainModel.SurveyModel.Survey>
{
    public void Configure(EntityTypeBuilder<DomainModel.SurveyModel.Survey> builder)
    {
        builder
            .Property(s => s.Title)
            .HasMaxLength(DomainModel.SurveyModel.Survey.TitleMaxLength)
            .IsRequired();
        builder
            .HasOne(s => s.Сreator)
            .WithMany(u => u.Surveys)
            .HasForeignKey(u => u.СreatorId)
            .IsRequired();
    }
}