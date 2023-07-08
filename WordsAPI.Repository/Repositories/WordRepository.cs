﻿using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.Models;
using WordsAPI.Core.Repositories;
using WordsAPI.Core.Services;

namespace WordsAPI.Repository.Repositories
{
    public class WordRepository<T> : GenericRepository<T>, IWordRepository<T> where T : AWord
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;


        public WordRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetWordsWithRelations(ODataQueryOptions<T> queryOptions)
        {
            IQueryable<T> query;

            if(queryOptions != null)
            {
                query = queryOptions.ApplyTo(_dbSet.Include(z => z.Translations).Include(z => z.Categories).AsSplitQuery().Where(z => z.Status > 0)) as IQueryable<T>;
            }
            else
            {
                query = _dbSet.Include(z => z.Translations).Include(z => z.Categories).AsSplitQuery().Where(z => z.Status > 0);
            }
          

            return  query.ToList();
        }
        public async Task<T> GetWordWithRelations(int id, ODataQueryOptions<T> queryOptions)
        {
            return await _dbSet.Where(z => z.Id == id).Include(z => z.Translations).Include(z => z.Categories).AsSplitQuery().Where(z => z.Status > 0).SingleOrDefaultAsync();
        }
    }
}
