using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.UnitOfWorks;

namespace WordsAPI.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
           _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            try
            {
                var a = await _context.SaveChangesAsync();
                Debug.WriteLine(_context.ChangeTracker.DebugView.LongView);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
          
        }
    }
}
