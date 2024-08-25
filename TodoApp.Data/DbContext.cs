using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data.Entites;
namespace TodoApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Status> TaskStatuses { get; set; }
        public DbSet<Priority> TaskPriorities { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.HasOne(t => t.User)
                    .WithMany()
                    .HasForeignKey(t => t.UserId)
                    .IsRequired();

                entity.HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.StatusId)
                .IsRequired();

                entity.HasOne(t => t.Priority)
                .WithMany()
                .HasForeignKey(t => t.PriorityId)
                .IsRequired();

                entity.ToTable("Tasks");

            });
            builder.Entity<Priority>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.ToTable("TaskPriorities");

            });
            builder.Entity<Status>(entity =>
            {
                entity.ToTable("TaskStatuses");
                entity.HasKey(entity => entity.Id);
            });
        }
    }
}
