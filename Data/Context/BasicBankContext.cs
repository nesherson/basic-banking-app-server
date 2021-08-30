using Microsoft.EntityFrameworkCore;
using Npgsql;

using basic_banking_app_server.Models.UserModel;
using basic_banking_app_server.Models.CardModel;
using basic_banking_app_server.Models.TransactionModel;
using basic_banking_app_server.Enums;

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
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TransactionEnums.Method>("transaction_method");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TransactionEnums.Status>("transaction_status");
        }


        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
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
            modelBuilder.HasPostgresEnum("method", "transaction_method", new[] { "withdraw", "deposit", "payment" })
                .HasPostgresEnum("status", "transaction_status", new[] { "pending", "failed", "captured", "refunded" })
                .HasAnnotation("Relational:Collation", "English_United States.1252");

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

                entity.Property(e => e.Network)
                    .IsRequired()
                    .HasColumnName("network");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cards_user_id_fkey");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transactions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");
                   
                entity.Property(e => e.Method).HasColumnName("method")
                    .HasColumnType("transaction_method");

                entity.Property(e => e.Status).HasColumnName("status")
                    .HasColumnType("transaction_status");

                entity.Property(e => e.CapturedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("captured_at");

                entity.Property(e => e.CardId).HasColumnName("card_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ReceiverCardNum)
                    .HasMaxLength(16)
                    .HasColumnName("receiver_card_num");

                entity.Property(e => e.RefundedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("refunded_at");

                entity.Property(e => e.SenderCardNum)
                    .HasMaxLength(16)
                    .HasColumnName("sender_card_num");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.TransactionCards)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("transactions_card_id_fkey");

                entity.HasOne(d => d.ReceiverCardNumNavigation)
                    .WithMany(p => p.TransactionReceiverCardNumNavigations)
                    .HasPrincipalKey(p => p.CardNumber)
                    .HasForeignKey(d => d.ReceiverCardNum)
                    .HasConstraintName("transactions_receiver_card_num_fkey");

                entity.HasOne(d => d.SenderCardNumNavigation)
                    .WithMany(p => p.TransactionSenderCardNumNavigations)
                    .HasPrincipalKey(p => p.CardNumber)
                    .HasForeignKey(d => d.SenderCardNum)
                    .HasConstraintName("transactions_sender_card_num_fkey");
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
