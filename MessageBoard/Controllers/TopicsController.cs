using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Persistance.Data;

namespace MessageBoard.Controllers
{
    public class TopicsController : ApiController
    {
        private readonly IMessageBoardRepository _repo;

        public TopicsController(IMessageBoardRepository repo)
        {
            _repo = repo;
        }

        //localhost:1540/api/v1/topics/?includeReplies=true
        public IEnumerable<Topic> Get(bool includeReplies = false)
        {
            IQueryable<Topic> results;

            if (includeReplies)
            {
                results = _repo.GetTopicsIncludingReplies();
            }
            else
            {
                results = _repo.GetTopics();
            }

            return results
                .OrderByDescending(t => t.CreatedAt)
                .Take(50)
                .ToList();
        }

        //API mapping HTTP verbs types automatically (GET, POST etc)
        //from (http)FromBody not nessecary, but good practice
        public HttpResponseMessage Post([FromBody]Topic newTopic)
        {
            if (newTopic.CreatedAt ==default(DateTime))
            {
                newTopic.CreatedAt = DateTime.UtcNow;
            }

            if (_repo.AddTopic(newTopic) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, newTopic);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
