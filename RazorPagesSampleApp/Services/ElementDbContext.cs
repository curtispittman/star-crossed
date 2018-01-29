using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorPagesSampleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace RazorPagesSampleApp.Services
{
    public class ElementDbContext : DbContext, IElementRepository
    {
        public DbSet<Element> Elements { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Element>().ToTable("Element").HasKey(element => element.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Element");
        }

        async Task IElementRepository.Add([Bind(nameof(Element.Description))] Element element)
        {
            Elements.Add(element);
            await SaveChangesAsync();
        }

        async Task<IEnumerable<Element>> IElementRepository.GetAll()
        {
            return await Elements.ToListAsync();
        }

        async Task IElementRepository.Remove(Guid id)
        {
            var element = await Elements.FindAsync(id);
            Elements.Remove(element);
            await SaveChangesAsync();
        }

        async Task<Element> IElementRepository.Find(Guid id)
        {
            return await Elements.FindAsync(id);
        }

        async Task IElementRepository.Update(Element element)
        {
            Element toUpdate = await Elements.FindAsync(element.Id);
            if (toUpdate != null)
            {
                toUpdate.Description = element.Description;
                await SaveChangesAsync();
            }
        }

        async Task IElementRepository.Star(Guid id)
        {
            var element = await Elements.FindAsync(id);
            if(element != null)
            {
                element.IsStarred = true;
                element.WasStarred = true;
                await SaveChangesAsync();
            }
        }

        async Task IElementRepository.RemoveAll()
        {
            var elements = Elements.ToList();
            //Elements.RemoveRange(elements);
            //var elements = Elements.ToListAsync();
            foreach (var element in elements)
            {
                 Elements.Remove(element);
            }

            

            await SaveChangesAsync();
            
        }

        async Task IElementRepository.Cross(Guid id)
        {
            var element = await Elements.FindAsync(id);
            if (element != null)
            {
                if (element.IsStarred)
                {
                    element.IsStarred = false;
                }
                else
                {
                    element.IsCrossed = true;
                }
                await SaveChangesAsync();
            }
        }
    }
}
