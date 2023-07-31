using Microsoft.EntityFrameworkCore;

namespace ATM.DataLayer.DbModel
{
    public partial class challengeATMContext : DbContext
    {
        public challengeATMContext()
        {
        }

        public challengeATMContext(DbContextOptions<challengeATMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<Operation> Operations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.Number)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Pin)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PIN");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Code)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdTarjetaNavigation)
                    .WithMany(p => p.Operations)
                    .HasForeignKey(d => d.IdTarjeta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Operation__IdTar__2F10007B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
