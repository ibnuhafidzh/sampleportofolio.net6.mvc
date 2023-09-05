using Firebase.Auth;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;
        IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            BasePath = "fillwithyourdata"
            ,
            AuthSecret = "fillwithyourdata"
        };
        IFirebaseClient client;

        public DownloadController(IWebHostEnvironment hostEnvironment)
        {
            environment = hostEnvironment;
        }
        [HttpGet("DownloadFile1")]
        public IActionResult DownloadFile1()
        {
            try
            {
                var token = HttpContext.Session.GetString("_UserToken");
                if (token != null)
                {
                    try
                    {
                        var email = HttpContext.Session.GetString("_UserEmail");
                        client = new FireSharp.FirebaseClient(config);
                        var data = new WebApp.Models.Firebase.Log();
                        data.email = email;
                        data.token = token;
                        data.page = "download";
                        data.info = "download file 1";
                        var dt = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                        data.jam = dt;
                        PushResponse response = client.Push("Log/", data);
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }
            var path = "fillwithyourpdfurl"; // get the path of the file
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] bytes = wc.DownloadData(path);

            return File(bytes, "application/octet-stream", "Resume Ibnu Hafidzh.pdf");
        }
        [HttpGet("DownloadFile2")]
        public IActionResult DownloadFile2()
        {
            try
            {
                var token = HttpContext.Session.GetString("_UserToken");
                if (token != null)
                {
                    try
                    {
                        var email = HttpContext.Session.GetString("_UserEmail");
                        client = new FireSharp.FirebaseClient(config);
                        var data = new WebApp.Models.Firebase.Log();
                        data.email = email;
                        data.token = token;
                        data.page = "download";
                        data.info = "download file 2";
                        var dt = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                        data.jam = dt;
                        PushResponse response = client.Push("Log/", data);
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }
            var path = "fillwithyourpdfurl"; // get the path of the file
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] bytes = wc.DownloadData(path);

            // Return the file. A byte array can also be used instead of a stream
            return File(bytes, "application/octet-stream", "Projek Ibnu Hafidzh.pdf");
        }
        [HttpGet("DownloadFile/{id}")]
        [HttpGet("{id}")]
        public IActionResult DownloadFile(int id)
        {
            var path = "";
            var name = "";
            if (id == 1)
            {
                path = "fillwithyourpdfurl"; // get the path of the file
                name = "Resume Ibnu Hafidzh.pdf";
            }
            else
            {
                path = "fillwithyourpdfurl"; // get the path of the file
                name = "Resume Detail Ibnu Hafidzh.pdf";
            }
            try
            {
                var token = HttpContext.Session.GetString("_UserToken");
                if (token != null)
                {
                    try
                    {
                        var email = HttpContext.Session.GetString("_UserEmail");
                        client = new FireSharp.FirebaseClient(config);
                        var data = new WebApp.Models.Firebase.Log();
                        data.email = email;
                        data.token = token;
                        data.page = "download";
                        data.info = "download file " + name;
                        var dt = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                        data.jam = dt;
                        PushResponse response = client.Push("Log/", data);
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] bytes = wc.DownloadData(path);

            // Return the file. A byte array can also be used instead of a stream
            return File(bytes, "application/octet-stream", name);
        }

    }
}
