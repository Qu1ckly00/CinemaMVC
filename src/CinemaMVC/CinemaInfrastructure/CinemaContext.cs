using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CinemaDomain.Model;

namespace CinemaInfrastructure;

public partial class CinemaContext : DbContext
{
    public CinemaContext()
    {
    }

    public CinemaContext(DbContextOptions<CinemaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Hall> Halls { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-QF6IVJ6\\SQLEXPRESS; Database=Cinema; Trusted_Connection=True; TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Film>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Directors)
                .HasColumnType("text")
                .HasColumnName("directors");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("rating");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.TrailerUrl)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("trailer_url");

            entity.HasMany(d => d.Genres).WithMany(p => p.Films)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FilmGenres_Genres"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FilmGenres_Films1"),
                    j =>
                    {
                        j.HasKey("FilmId", "GenresId").HasName("PK_FilmGenres_1");
                        j.ToTable("FilmGenres");
                        j.IndexerProperty<int>("FilmId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("film_id");
                        j.IndexerProperty<int>("GenresId").HasColumnName("genres_id");
                    });
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Hall>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Data)
                .HasColumnType("datetime")
                .HasColumnName("data");
            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.HallId).HasColumnName("hall_id");
            entity.Property(e => e.TicketPrice)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("ticket_price");

            entity.HasOne(d => d.Film).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sessions_Films");

            entity.HasOne(d => d.Hall).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.HallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sessions_Halls");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MovieTitle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("movie_title");
            entity.Property(e => e.Seat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("seat");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Session).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Sessions");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Users1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Emaill)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("emaill");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.OrderHistory)
                .HasColumnType("text")
                .HasColumnName("order_history");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
