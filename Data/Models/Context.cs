using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
   public class Context:DbContext
    {
        public DbSet<English> Englishes { get; set; }
        public DbSet<Turkish> Turkishes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WordCategory> WordCategories { get; set; }
    }
}
