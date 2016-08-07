using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Migrations;


namespace Persistance.Data
{
   public class MessageBoardContext : DbContext
    {
       public MessageBoardContext()
           : base("DefaultConnection")
       {
           Configuration.LazyLoadingEnabled = false;
           Configuration.ProxyCreationEnabled = false;

           Database.SetInitializer(
               new MigrateDatabaseToLatestVersion<MessageBoardContext, Configuration>("DefaultConnection"));
       }

       public DbSet<Topic> Topics { get; set; }
       public DbSet<Reply> Replies { get; set; }
    }
}

/*
 * What the EF Code First functionality does here, is it looks the DbSets 
 * and infers by the structure of the classes how to build that DB.
 * When creating the instance of DbContext, it is looking for a topic 
 * and reply table in the DB. If nothing found, it'll create them.
 * 
 * You can specify additional configs by using attributes or Fluent APi 
 * to fine tune your mapping.
 */