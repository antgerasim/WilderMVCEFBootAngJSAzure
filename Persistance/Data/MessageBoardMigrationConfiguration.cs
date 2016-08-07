using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Persistance.Data
{
    public class MessageBoardMigrationConfiguration : DbMigrationsConfiguration<MessageBoardContext>
    {
        public MessageBoardMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;//this is a dangerous setting - only for test servers
            AutomaticMigrationsEnabled = true;
        }

        //seed is called everytime the webapplication restarts, more importantly - everytime a new 
        //app domain is created. The code inside the seed must be protected multiple times. You 
        //need to check the state of the DB before execting the code in the seed method.
        protected override void Seed(MessageBoardContext context)
        {
            base.Seed(context);
#if DEBUG
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
#endif
        }
    }
}