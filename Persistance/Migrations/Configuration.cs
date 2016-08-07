using System.Collections.Generic;
using Persistance.Data;

namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Persistance.Data.MessageBoardContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Persistance.Data.MessageBoardContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (!context.Topics.Any())
            {
                var topic = new Topic()
                {
                    Title = "I love MVC",
                    CreatedAt = DateTime.Now,
                    Body = "I love ASP.NET MVC and want everyone to know about it",
                    Replies = new List<Reply>()
                    {
                        new Reply()
                        {
                            Body = "I love it too!",
                            CreatedAt = DateTime.Now
                        },
                        new Reply()
                        {
                            Body = "Me too",
                            CreatedAt = DateTime.Now
                        },
                        new Reply()
                        {
                            Body = "Aw sucks",
                            CreatedAt = DateTime.Now
                        }
                    }
                };

                context.Topics.Add(topic);

                var anotherTopic = new Topic()
                {
                    Title = "I like Ruby too!",
                    CreatedAt = DateTime.Now,
                    Body = "Ruby on Rails is popular"
                };

                context.Topics.Add(anotherTopic);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    var msg = e.Message;
                }
            }
        }
    }
}
