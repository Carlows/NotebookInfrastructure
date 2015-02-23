using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MyNotebook.Domain.Logging;

namespace MyNotebook.Domain.Entities
{
    public class MyNotebookDbContext : IdentityDbContext
    {
        public MyNotebookDbContext()
            : base("DefaultConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Note> Notes { get; set; }
    }

    /// <summary>
    /// Database initializer.
    /// </summary>
    public class MyNotebookDbInitializer : DropCreateDatabaseAlways<MyNotebookDbContext>
    {
        /// <summary>
        /// Seeds the database with data.
        /// </summary>
        /// <param name="context">the context to seed</param>
        protected override void Seed(MyNotebookDbContext context)
        {
            try
            {
                var user = new IdentityUser
                {
                    UserName = "Admin"
                };
                new UserManager<IdentityUser>(new UserStore<IdentityUser>(context)).Create(user, "password");
                context.Notes.Add(new Note
                {
                    Heading = "My First Note",
                    Body = "This is a note.",
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    UserId = user.Id
                });
                context.SaveChanges();
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
            }
        }
    }
}
