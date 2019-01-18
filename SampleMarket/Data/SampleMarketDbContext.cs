﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleMarket.Models;

namespace SampleMarket.Data
{
    public class SampleMarketDbContext : DbContext
    {
        public SampleMarketDbContext (DbContextOptions<SampleMarketDbContext> options)
            : base(options)
        {
        }

        public DbSet<SampleMarket.Models.Product> Products { get; set; }

        public DbSet<SampleMarket.Models.CartItem> CartItem { get; set; }
    }
}
