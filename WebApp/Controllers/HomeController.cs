using Firebase.Auth;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Mail;
using System.Reflection.PortableExecutable;
using WebApp.Models;
using static WebApp.Models.Firebase;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        FirebaseAuthProvider auth;
        IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            BasePath = "fillwithyourdata"
            ,
            AuthSecret = "fillwithyourdata"
        };
        IFirebaseClient client;
        public HomeController()
        {
            auth = new FirebaseAuthProvider(
                        new Firebase.Auth.FirebaseConfig("fillwithyourdata"));
        }

        public async Task<IActionResult> IndexAsync()
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
                        data.page = "index";
                        data.info = "halaman index";
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
            var list = new About();

            try
            {
                list.Sosmed = new List<Socialmedia>();
                list.Desc = new Desc();
                list.Role = new List<Role>();
                list.Skill = new List<Skill>();
                list.Project = new List<Project>();
                client = new FireSharp.FirebaseClient(config);
                FirebaseResponse responsedesc = await client.GetAsync("Desc/");
                dynamic datadesc = JsonConvert.DeserializeObject<dynamic>(responsedesc.Body);
                if (datadesc != null)
                {
                    foreach (var item in datadesc)
                    {
                        try
                        {
                            var convitem = JsonConvert.DeserializeObject<Desc>(((JProperty)item).Value.ToString());
                            list.Desc = convitem;
                        }
                        catch
                        {

                        }
                    }

                }
                FirebaseResponse responserole = await client.GetAsync("Role/");
                dynamic datarole = JsonConvert.DeserializeObject<dynamic>(responserole.Body);
                if (datarole != null)
                {
                    foreach (var item in datarole)
                    {
                        try
                        {
                            var convitem = JsonConvert.DeserializeObject<Role>(((JProperty)item).Value.ToString());
                            convitem.name = convitem.name + ",";
                            list.Role.Add(convitem);
                        }
                        catch
                        {

                        }

                    }
                }
                FirebaseResponse responseskill = await client.GetAsync("skill/");
                dynamic dataskill = JsonConvert.DeserializeObject<dynamic>(responseskill.Body);
                if (dataskill != null)
                {
                    foreach (var item in dataskill)
                    {
                        try
                        {
                            var convitem = JsonConvert.DeserializeObject<Skill>(((JProperty)item).Value.ToString());
                            list.Skill.Add(convitem);
                        }
                        catch
                        {

                        }

                    }
                }
                FirebaseResponse responseexp = await client.GetAsync("experience/");
                dynamic dataexp = JsonConvert.DeserializeObject<dynamic>(responseexp.Body);
                if (dataexp != null)
                {
                    foreach (var item in dataexp)
                    {
                        try
                        {
                            var convitem = JsonConvert.DeserializeObject<Project>(((JProperty)item).Value.ToString());
                            if (convitem != null)
                            {
                                if (convitem.FrontEnd != null)
                                {
                                    if (convitem.FrontEnd.Count > 0)
                                    {
                                        var listitem = new List<string>();

                                        foreach (string itemdata in convitem.FrontEnd)
                                        {
                                            string strdata = "";
                                            if (!string.IsNullOrEmpty(itemdata))
                                            {
                                                strdata = itemdata + ", ";
                                            }
                                            if (!string.IsNullOrEmpty(strdata))
                                            {
                                                listitem.Add(strdata);
                                            }
                                        }
                                        convitem.FrontEnd.Clear();
                                        convitem.FrontEnd.AddRange(listitem);
                                    }
                                }
                                if (convitem.BackEnd != null)
                                {
                                    if (convitem.BackEnd.Count > 0)
                                    {
                                        var listitem = new List<string>();

                                        foreach (string itemdata in convitem.BackEnd)
                                        {
                                            string strdata = "";
                                            if (!string.IsNullOrEmpty(itemdata))
                                            {
                                                strdata = itemdata + ", ";
                                            }
                                            if (!string.IsNullOrEmpty(strdata))
                                            {
                                                listitem.Add(strdata);
                                            }
                                        }
                                        convitem.BackEnd.Clear();
                                        convitem.BackEnd.AddRange(listitem);
                                    }
                                }
                                if (convitem.Database != null)
                                {
                                    if (convitem.Database.Count > 0)
                                    {
                                        var listitem = new List<string>();

                                        foreach (string itemdata in convitem.Database)
                                        {
                                            string strdata = "";
                                            if (!string.IsNullOrEmpty(itemdata))
                                            {
                                                strdata = itemdata + ", ";
                                            }
                                            if (!string.IsNullOrEmpty(strdata))
                                            {
                                                listitem.Add(strdata);
                                            }
                                        }
                                        convitem.Database.Clear();
                                        convitem.Database.AddRange(listitem);
                                    }
                                }
                                list.Project.Add(convitem);
                            }
                        }
                        catch
                        {

                        }

                    }
                }
                FirebaseResponse responsesosmed = await client.GetAsync("Socialmedia/");
                dynamic datasosmed = JsonConvert.DeserializeObject<dynamic>(responsesosmed.Body);
                if (datasosmed != null)
                {
                    foreach (JProperty item in datasosmed)
                    {

                        try
                        {
                            JToken value = item.Value;
                            if (value.Type == JTokenType.Object)
                            {
                                var itemsosmed = new Socialmedia();
                                itemsosmed.name = value.Value<String>("name");
                                itemsosmed.url = value.Value<String>("url");
                                itemsosmed.detail = value.Value<String>("detail");
                                list.Sosmed.Add(itemsosmed);
                            }

                        }
                        catch
                        {

                        }

                    }
                }

            }
            catch
            {

            }
            return View(list);
        }
        [Route("Download")]
        public IActionResult Download()
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
                    data.info = "halaman download";
                    var dt = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    data.jam = dt;
                    PushResponse response = client.Push("Log/", data);

                }
                catch
                {

                }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Registration(LoginModel loginModel)
        {

            try
            {
                //create the user
                await auth.CreateUserWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                //log in the new user
                var fbAuthLink = await auth
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                string token = fbAuthLink.FirebaseToken;
                //saving the token in a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);
                    HttpContext.Session.SetString("_UserEmail", loginModel.Email);
                    return RedirectToAction("Download");
                }
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View(loginModel);
            }
            return View();

        }
        [Route("SignIn")]
        public IActionResult SignIn()
        {
            var token = HttpContext.Session.GetString("_UserToken");
            if (token != null)
            {
                return RedirectToAction("Download");
            }
            else
            {
                return View();

            }
        }
        [Route("SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel loginModel)
        {

            try
            {
                //log in an existing user
                var fbAuthLink = await auth
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                string token = fbAuthLink.FirebaseToken;
                //save the token to a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);
                    HttpContext.Session.SetString("_UserEmail", loginModel.Email);
                    return RedirectToAction("Download");
                }

            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View(loginModel);
            }
            return View();
        }

        [Route("LogOut")]
        public IActionResult LogOut()
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
                        data.page = "logout";
                        data.info = "halaman logout";
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
            HttpContext.Session.Remove("_UserToken");
            HttpContext.Session.Remove("_UserEmail");
            return RedirectToAction("SignIn");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}