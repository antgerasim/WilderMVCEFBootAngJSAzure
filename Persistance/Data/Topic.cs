using System;
using System.Collections.Generic;

namespace Persistance.Data
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    /*    public string Ficken { get; set; }*/
        public DateTime CreatedAt { get; set; }


        public ICollection<Reply> Replies { get; set; }
    }
}
