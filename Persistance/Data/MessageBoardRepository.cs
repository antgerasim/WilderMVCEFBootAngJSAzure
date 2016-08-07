using System;
using System.Linq;

namespace Persistance.Data
{
    public class MessageBoardRepository : IMessageBoardRepository
    {
        private readonly MessageBoardContext _ctx;

        public MessageBoardRepository(MessageBoardContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Topic> GetTopics()
        {
            return _ctx.Topics;
        }

        public IQueryable<Topic> GetTopicsIncludingReplies()
        {
            //eager loading the 'replies' collections
            return _ctx.Topics.Include("Replies");
        }

        public IQueryable<Reply> GetRepliesByTopic(int topicId)
        {
            return _ctx.Replies.Where(r => r.TopicId == topicId);
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                //Todo logging
                return false;
            }
        }

        public bool AddTopic(Topic newTopic)
        {
            try
            {
                _ctx.Topics.Add(newTopic);
                return true;
            }
            catch (Exception e)
            {
                //todo logging
                return false;
            }
        }

        public bool AddReply(Reply newReply)
        {
            try
            {
                _ctx.Replies.Add(newReply);
                return true;
            }
            catch (Exception e)
            {
                //todo logging
                return false;
            }
        }
    }
}