using CoreModule.Domain.Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistent.Courses;

public class CourseConfig : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses", "dbo");
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Slug).IsUnique();

        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(c => c.Slug)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(300);

        builder.Property(c => c.ImageName)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.VideoName)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(c => c.Price);


        builder.OwnsOne(b => b.SeoData, config =>
        {
            config.Property(b => b.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("MetaDescription");

            config.Property(b => b.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("MetaTitle");

            config.Property(b => b.MetaKeyWords)
                .HasMaxLength(500)
                .HasColumnName("MetaKeyWords");

            config.Property(b => b.IndexPage)
                .HasColumnName("IndexPage");

            config.Property(b => b.Canonical)
                .HasMaxLength(500)
                .HasColumnName("Canonical");

            config.Property(b => b.Schema)
                .HasColumnName("Schema");
        });

        builder.OwnsMany(c => c.Sections, config =>
        {
            config.ToTable("Sections", "course");
            config.HasKey(c => c.Id);

            config.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(200);

            config.Property(s => s.DisplayOrder)
            .IsRequired();

            config.OwnsMany(s => s.Episodes, e =>
            {
                e.ToTable("Episodes", "course");
                e.HasKey(c => c.Id);
                e.HasIndex(c => c.Token);

                e.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

                e.Property(e => e.EnglishTitle)
                .IsRequired()
                .HasMaxLength(100);

                e.Property(c => c.VideoName)
                .IsRequired(false)
                .HasMaxLength(200);

                e.Property(c => c.AttachmentName)
                 .IsRequired(false)
                 .HasMaxLength(100);
            });
        });
    }
}
