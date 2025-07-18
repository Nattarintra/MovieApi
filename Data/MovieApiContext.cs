﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;

namespace MovieApi.Data
{
    public class MovieApiContext : DbContext
    {
        public MovieApiContext (DbContextOptions<MovieApiContext> options)
            : base(options)
        {
        }

        public DbSet<MovieApi.Models.Entities.Movie> Movies { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.MovieDetails)
                .WithOne(md => md.Movie)
                .HasForeignKey<MovieDetails>(md => md.MovieId)
                .IsRequired();

            modelBuilder.Entity<MovieDetails>()
                .HasIndex(md => md.MovieId)
                .IsUnique();

        }
        public DbSet<MovieApi.Models.Entities.Actor> Actors { get; set; } = default!;
        public DbSet<MovieApi.Models.Entities.Review> Reviews { get; set; } = default!;
    }
}
