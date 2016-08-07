using System.Linq;

namespace Persistance.Data
{
    public interface IMessageBoardRepository
    {
        IQueryable<Topic> GetTopics();
        IQueryable<Topic> GetTopicsIncludingReplies(); 
        IQueryable<Reply> GetRepliesByTopic(int topicId);
     

        bool Save();

        bool AddTopic(Topic newTopic);

        bool AddReply(Reply newReply);
    }
}

/*
 * My feeling of repositories is - it should include only operations 
 * that you know are actually needed. Instead of these full blown repos
 * that are hardly used
 * */