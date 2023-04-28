using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserQuestionsDataAccess;

namespace WebAPI.Controllers
{
    public class BcwwChatbotUserQuestionsController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (BCWW_ChatbotEntities entities = new BCWW_ChatbotEntities())
            {
                try
                {
                    var entity = entities.tbl_userQuestions.ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                catch (Exception ex)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }

            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (BCWW_ChatbotEntities entities = new BCWW_ChatbotEntities())
            {
                var entity = entities.tbl_userQuestions.FirstOrDefault(x => x.id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record with Id = " + id.ToString() + " not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] tbl_userQuestions questions)
        {
            try
            {
                using (BCWW_ChatbotEntities entities = new BCWW_ChatbotEntities())
                {
                    entities.tbl_userQuestions.Add(questions);
                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.Created, questions);

                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
