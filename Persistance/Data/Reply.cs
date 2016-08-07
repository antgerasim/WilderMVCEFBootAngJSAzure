using System;

namespace Persistance.Data
{
    public class Reply
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }

        //nav prop to root
        public int TopicId { get; set; }
    }
}