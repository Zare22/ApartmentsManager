using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Public_MVC.Context
{
    public partial class ApartmentsContext : DbContext
    {
        public ApartmentsContext()
            : base("name=ApartmentsContext")
        {
        }

        public virtual DbSet<Apartment> Apartment { get; set; }
        public virtual DbSet<ApartmentPicture> ApartmentPicture { get; set; }
        public virtual DbSet<ApartmentReservation> ApartmentReservation { get; set; }
        public virtual DbSet<ApartmentReview> ApartmentReview { get; set; }
        public virtual DbSet<ApartmentStatus> ApartmentStatus { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<TaggedApartment> TaggedApartment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Apartment>()
                .HasMany(e => e.ApartmentReservation)
                .WithRequired(e => e.Apartment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Apartment>()
                .HasMany(e => e.ApartmentReview)
                .WithRequired(e => e.Apartment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Apartment>()
                .HasMany(e => e.TaggedApartment)
                .WithRequired(e => e.Apartment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApartmentPicture>()
                .Property(e => e.Base64Content)
                .IsUnicode(false);

            modelBuilder.Entity<ApartmentReservation>()
                .Property(e => e.UserPhone)
                .IsFixedLength();

            modelBuilder.Entity<ApartmentStatus>()
                .HasMany(e => e.Apartment)
                .WithRequired(e => e.ApartmentStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.ApartmentReservation)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.ApartmentReview)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.TaggedApartment)
                .WithRequired(e => e.Tag)
                .WillCascadeOnDelete(false);
        }
    }
}
