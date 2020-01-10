using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LiteDB;
using ToDo.BackEnd.Models;

namespace ToDo.BackEnd.Controllers
{
    public class CommentController : ApiController
    {

        [HttpGet]
        [Route("api/comment/")]
        public IHttpActionResult GetCommentsforTask(Guid taskID)
        {
            using (var db = new LiteDatabase(@"C:\ToDo\ToDo.db"))
            {
                var taskCollection = db.GetCollection<Task>("Tasks");
                var userCollection = db.GetCollection<User>("Users");
                var commentCollection = db.GetCollection<Comment>("Comments");

                if (taskCollection.FindById(taskID) == null)
                {
                    return NotFound();
                }
                return Ok(commentCollection.Find(c => c.UserId == taskID).ToList());

            }
        }
        [HttpPost]
        public Comment CreateNewComment(Comment newComment)
        {
            using (var db = new LiteDatabase(@"C:\ToDo\ToDo.db"))
            {
                var taskCollection = db.GetCollection<Task>("Tasks");
                var userCollection = db.GetCollection<User>("Users");
                var commentCollection = db.GetCollection<Comment>("Comments");

                newComment.Id = Guid.NewGuid();

                commentCollection.Insert(newComment);

                return newComment;
            }
        }
    }
}

