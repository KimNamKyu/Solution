using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [Route("api/Select")]
        [HttpGet]
        public ActionResult<ArrayList> GetSelect([FromForm] Notices notices)
        {
            DataBase db = new DataBase();
            MySqlConnection conn = db.GetConnetion();
            if(conn == null)
            {
                Console.WriteLine("접속 오류");
            }
            else
            {
                Console.WriteLine("접속 성공");
            }
            return Commons.GetSelect();
        }

        [Route("api/Insert")]
        [HttpPost]
        public ActionResult<ArrayList> Insert([FromForm] Notices notices)
        {
            DataBase db = new DataBase();
            MySqlConnection conn = db.GetConnetion();
            if (conn == null)
            {
                Console.WriteLine("접속 오류");
            }
            else
            {
                Console.WriteLine("접속 성공");
            }
            return Commons.Getinsert(notices);
        }

        [Route("api/Update")]
        [HttpPost]
        public ActionResult<ArrayList> Update([FromForm] Notices test)
        {
            DataBase db = new DataBase();
            MySqlConnection conn = db.GetConnetion();
            if (conn == null)
            {
                Console.WriteLine("접속 오류");
            }
            else
            {
                Console.WriteLine("접속 성공");
            }
            return Commons.GetUpdate(test);
        }

        [Route("api/Delete")]
        [HttpPost]
        public ActionResult<ArrayList> Delete([FromForm] Notices test)
        {
            DataBase db = new DataBase();
            MySqlConnection conn = db.GetConnetion();
            if (conn == null)
            {
                Console.WriteLine("접속 오류");
            }
            else
            {
                Console.WriteLine("접속 성공");
            }
            return Commons.GetDelete(test);
        }
    }
}
