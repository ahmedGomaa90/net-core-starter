using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CoWorking.Data.Repositories;
using CoWorking.Data.Repositories.Repositories;
using CoWorking.Domain.Entities;
using CoWorking.Business.Managers;
using CoWorking.Domain.BusinessConracts.Managers;


namespace CoWorking.Services.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IDummyManager _dummyManager;

        public ValuesController(IDummyManager dummyManager){
            _dummyManager = dummyManager;
        }
        // GET api/values
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            // return new string[] { "value1", "value2" };

            // var quow = new QueryableUnitOfWork();
            // var repo = new DummyRepository(quow);
            // var manager = new DummyManager(repo);

            // var alls = _dummyManager.Create(new Dummy
            // {
            //     Name = "Ahmed"
            // });

            // quow.Commit();

            return _dummyManager.GetAll().Select(x => x.Name).ToList();


        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
