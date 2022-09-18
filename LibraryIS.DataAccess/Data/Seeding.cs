using LibraryIS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.DataAccess.Data
{
    public static class Seeding
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publisher>().HasData(new Publisher { Id = 1, Name = "Laguna" });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { Id = 2, Name = "Vulkan" });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { Id = 3, Name = "Odbrana" });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { Id = 4, Name = "Službeni Glasnik" });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { Id = 5, Name = "Plato" });

            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 1, Name = "History" });
            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 2, Name = "Poetry" });
            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 3, Name = "Religion" });
            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 4, Name = "Mitology" });
            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 5, Name = "Classics" });
            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 6, Name = "Classics" });

            modelBuilder.Entity<Author>().HasData(new Author { Id = 1, FirstName = "Ivan", LastName= "Lalić" });
            modelBuilder.Entity<Author>().HasData(new Author { Id = 2, FirstName = "Igor", LastName = "Mirović" });
            modelBuilder.Entity<Author>().HasData(new Author { Id = 3, FirstName = "Branko", LastName = "Miljković" });
            modelBuilder.Entity<Author>().HasData(new Author { Id = 4, FirstName = "Franc", LastName = "Kafka" });
            modelBuilder.Entity<Author>().HasData(new Author { Id = 5, FirstName = "Desanka", LastName = "Maksimović" });

            modelBuilder.Entity<Member>().HasData(new Member { Id = 1, FirstName = "Member", LastName = "One", EmailAddress = "memberone@test.com", PhoneNumber = "0111", RegistrationDate = DateTime.Today, Address = "Address1" });
            modelBuilder.Entity<Member>().HasData(new Member { Id = 2, FirstName = "Member", LastName = "Two", EmailAddress = "membertwo@test.com", PhoneNumber = "0112", RegistrationDate = DateTime.Today, Address = "Address2" });
            modelBuilder.Entity<Member>().HasData(new Member { Id = 3, FirstName = "Member", LastName = "Three", EmailAddress = "memberthree@test.com", PhoneNumber = "0113", RegistrationDate = DateTime.Today, Address = "Address3" });
            modelBuilder.Entity<Member>().HasData(new Member { Id = 4, FirstName = "Member", LastName = "Four", EmailAddress = "memberfour@test.com", PhoneNumber = "0114", RegistrationDate = DateTime.Today, Address = "Address4" });
            modelBuilder.Entity<Member>().HasData(new Member { Id = 5, FirstName = "Member", LastName = "Five", EmailAddress = "memberfive@test.com", PhoneNumber = "0115", RegistrationDate = DateTime.Today, Address = "Address5" });
            modelBuilder.Entity<Member>().HasData(new Member { Id = 6, FirstName = "Member", LastName = "Five", EmailAddress = "memberfive@test.com", PhoneNumber = "0115", RegistrationDate = DateTime.Today, Address = "Address5" });
        }
    }
}
