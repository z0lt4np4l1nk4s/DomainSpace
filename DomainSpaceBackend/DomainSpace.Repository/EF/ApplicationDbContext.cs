namespace DomainSpace.Repository.EF;

/// <summary>
/// Application database context
/// </summary>
public class ApplicationDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    /// <summary>
    /// Content
    /// </summary>
    public DbSet<ContentEntity> Content { get; set; } = default!;

    /// <summary>
    /// Files
    /// </summary>
    public DbSet<FileEntity> Files { get; set; } = default!;

    /// <summary>
    /// Subjects
    /// </summary>
    public DbSet<SubjectEntity> Subjects { get; set; } = default!;

    /// <summary>
    /// Likes
    /// </summary>
    public DbSet<LikeEntity> Likes { get; set; } = default!;

    /// <summary>
    /// Emails
    /// </summary>
    public DbSet<EmailEntity> Emails { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ContentEntity>().HasQueryFilter(x => !x.DeleteTime.HasValue);
        builder.Entity<FileEntity>().HasQueryFilter(x => !x.DeleteTime.HasValue);
        builder.Entity<SubjectEntity>().HasQueryFilter(x => !x.DeleteTime.HasValue);

        builder.Entity<ContentEntity>(entity =>
        {
            entity.HasOne(x => x.User)
                .WithMany(x => x.Content)
                .HasForeignKey(x => x.UserId);

            entity.HasOne(x => x.Subject)
                .WithMany(x => x.Content)
                .HasForeignKey(x => x.SubjectId);

            entity.HasMany(x => x.Likes)
                .WithOne(x => x.Content)
                .HasForeignKey(x => x.ContentId);
        });

        builder.Entity<UserEntity>(entity =>
        {
            entity.HasMany(x => x.Content)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            entity.HasMany(x => x.Subjects)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            entity.HasMany(x => x.Files)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            entity.HasMany(x => x.Likes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            entity.HasMany(x => x.Emails)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        });

        builder.Entity<SubjectEntity>(entity =>
        {
            entity.HasMany(x => x.Content)
                .WithOne(x => x.Subject)
                .HasForeignKey(x => x.SubjectId);

            entity.HasOne(x => x.User)
                .WithMany(x => x.Subjects)
                .HasForeignKey(x => x.UserId);
        });

        builder.Entity<FileEntity>(entity =>
        {
            entity.HasOne(x => x.User)
                .WithMany(x => x.Files)
                .HasForeignKey(x => x.UserId);
        });

        builder.Entity<LikeEntity>(entity =>
        {
            entity.HasOne(x => x.User)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.UserId);

            entity.HasOne(x => x.Content)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.ContentId);
        });

        builder.Entity<EmailEntity>(entity =>
        {
            entity.HasOne(x => x.User)
                .WithMany(x => x.Emails)
                .HasForeignKey(x => x.UserId);
        });

        base.OnModelCreating(builder);
    }
}
