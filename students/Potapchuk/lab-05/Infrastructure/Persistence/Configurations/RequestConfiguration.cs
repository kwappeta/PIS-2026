public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasMaxLength(50);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Status).HasConversion<string>();
    }
}