using Microsoft.EntityFrameworkCore;

using basic_banking_app_server.Models.UserModel;
using basic_banking_app_server.Models.CardModel;
using basic_banking_app_server.Models.TransactionModel;

namespace basic_banking_app_server.Data.Context
{
    public partial class BasicBankContext : DbContext
    {
        public BasicBankContext()
        {
        }

        public BasicBankContext(DbContextOptions<BasicBankContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<TransactionDeposit> TransactionDeposits { get; set; }
        public virtual DbSet<TransactionPayment> TransactionPayments { get; set; }
        public virtual DbSet<TransactionWithdrawal> TransactionWithdrawals { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("cards");

                entity.HasIndex(e => e.CardNumber, "cards_card_number_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasColumnName("card_number");

                entity.Property(e => e.Network).HasColumnName("network");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("cards_user_id_fkey");
            });

            modelBuilder.Entity<TransactionDeposit>(entity =>
            {
                entity.ToTable("transaction_deposits");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CapturedAt).HasColumnName("captured_at");

                entity.Property(e => e.CardId).HasColumnName("card_id");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.TransactionDeposits)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transaction_deposits_card_id_fkey");
            });

            modelBuilder.Entity<TransactionPayment>(entity =>
            {
                entity.ToTable("transaction_payments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CapturedAt).HasColumnName("captured_at");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ReceiverCardNumber)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("receiver_card_number");

                entity.Property(e => e.RefundedAt).HasColumnName("refunded_at");

                entity.Property(e => e.SenderCardNumber)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("sender_card_number");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status");

                entity.HasOne(d => d.ReceiverCardNumberNavigation)
                    .WithMany(p => p.TransactionPaymentReceiverCardNumberNavigations)
                    .HasPrincipalKey(p => p.CardNumber)
                    .HasForeignKey(d => d.ReceiverCardNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transaction_payments_receiver_card_number_fkey");

                entity.HasOne(d => d.SenderCardNumberNavigation)
                    .WithMany(p => p.TransactionPaymentSenderCardNumberNavigations)
                    .HasPrincipalKey(p => p.CardNumber)
                    .HasForeignKey(d => d.SenderCardNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transaction_payments_sender_card_number_fkey");
            });

            modelBuilder.Entity<TransactionWithdrawal>(entity =>
            {
                entity.ToTable("transaction_withdrawals");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CapturedAt).HasColumnName("captured_at");

                entity.Property(e => e.CardId).HasColumnName("card_id");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.TransactionWithdrawals)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transaction_withdrawals_card_id_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.Country).HasColumnName("country");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
