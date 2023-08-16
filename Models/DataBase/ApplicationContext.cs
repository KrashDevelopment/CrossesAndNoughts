﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossesAndNoughts.Models.DataBase;

class ApplicationContext : DbContext
{
    public DbSet<UserRecord> Records { get; set; }

    public ApplicationContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=crossesandnoughtsdb;Trusted_Connection=True;");
    }
}
