using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP;
using YiLuTongTrial2.BLL;
using YiLuTongTrial2.Models;
using System.Threading.Tasks;
using Senparc.Weixin.MP.Entities;

namespace YiLuTongTrial2.Controllers
{
    public class HomeController : Controller
    {
        //I've deleted mappingSource from DBContext. DBContext was regenerated?
        private DBDataContext db = new DBDataContext();
        //ADDING THIS
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Home/
        private string appId = "wxbf475b9a09f4e353";
        private string secret = "6293389fbb293e55ae62107f453c92fd";

        public ActionResult StartWay()
        {

            return View();
        }


        [HttpGet]
        public JsonResult AddWay(string fromLng, string fromLat, string toLng, string toLat, string userid)
        {
            var session = SessionHelper.Get(userid);
            //this is not valid because it does not handle null exceptions. Perhaps it cannot be tested through a browser?
            if (session != null)
            {
                var user = session.UserInfo;
                var routeModel = YiLuTongTrial2.BLL.Route.CreateRoute(fromLng, fromLat, toLng, toLat, user.openid, user.nickname, user.headimgurl);
                session.Route = routeModel.Route;
                Task.Factory.StartNew(() =>
                {
                    var token = SeaStar.WechatAPI.TokenHandler.GetAccess_token(appId, secret);
                    SeaStar.WechatAPI.MessageHandle.SendText(token.access_token, new SeaStar.WechatAPI.Model.SendTextInfo()
                    {
                        text = new SeaStar.WechatAPI.Model.Contents()
                        {
                            content = "恭喜您创建旅途成功，您的路线号码为：" + routeModel.RouteNumber + "，您只需将路线号码分享给您的好友后，便可以跟他们一起旅游了，赶快分享吧~"
                        },
                        touser = routeModel.UserId
                    });
                });

                return new JsonResult()
                {
                    Data = routeModel,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public ActionResult Search(string keyword, string userid)
        {
            ViewData["keyword"] = keyword;
            ViewData["userid"] = userid;
            return View();
        }

        public ActionResult Navigate(string title1, string userid, string lat, string lng)
        {
            ViewData["title1"] = title1;
            ViewData["userid"] = userid;
            ViewData["lat"] = lat;
            ViewData["lng"] = lng;
            return View();
        }


        //I WROTE:
        [HttpGet]
        public ActionResult JoinWay()
        {
            //the model is null right now, we have to fill it.

            var sessionInfo = new SessionInfo()
            {
                
                Latitude = "3",
                Longitude = "4",
                UserInfo = new WeixinUserInfoResult()
                {
                    nickname = "david",
                    openid = "davidmarcano",
                    headimgurl = "kate/david/thebomb"
                }
            };

            var allSessions = SessionHelper.Sessions;
            System.Diagnostics.Debug.Write("allSessions: " + allSessions);
            allSessions.Add("davidmarcano", sessionInfo);
            
            var userInfo2 = sessionInfo.UserInfo;

            /*
            var userInfo = new WeixinUserInfoResult()
            {
                headimgurl = "C:\\Users\\dmarcano\\Documents\\Backgrounds\\cool_desktop_wallpaper.jpg",
                language = "English",
                nickname = "Dave",
                openid = "ID stuff",
                subscribe = 1
            };
            */
            //var userInfo = db.RouteMembers;
            return View(userInfo2);
        }

        //Added searchString in order to search within the database?
        [HttpPost]
        //public JsonResult JoinWay(FormCollection form)
        public JsonResult JoinWay(FormCollection form)
        {
            var number = form["number"];
            var userid = form["hidUserId"];
            var lat = form["lat"];
            var lng = form["lng"];
            System.Diagnostics.Debug.Write("number: " + number);
            System.Diagnostics.Debug.Write("userid: " + userid);
            System.Diagnostics.Debug.Write("lat: " + lat);
            System.Diagnostics.Debug.Write("lng: " + lng);
            //inside session exists the SessionInfo of the person that submitted the form. Wouldn't this work only if the person exists in the database?
            var session = SessionHelper.Get(userid);

            //what is this number? Is this the input?
            if (number != null)
            {
                //response holds wether or not the input already exists in the database
                var reponse = YiLuTongTrial2.BLL.Route.JoinRoute(number, lat, lng, userid, session.UserInfo.nickname, session.UserInfo.headimgurl);
                if (reponse.IsSuccess)
                {
                    session.Route = reponse.Route;
                }

                return new JsonResult()
                {
                    Data = reponse
                };
            }

            return new JsonResult();
                
        }

        [HttpGet]
        public ActionResult Routing(string routnumber, string userid)
        {
            var number = routnumber;
            var userId = userid;
            //var number = Route.CreateRoute("108.88751", "34.230669", "109.069117", "34.276839", "oijfFtxWCUOtx5cXbF1pTIJMeVdo", "Jammey", "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/46").RouteNumber;//form["routnumber"];
            var model = YiLuTongTrial2.BLL.Route.GetRouteByNumber(number);
            model.UserId = userId;
            //var model = new YiLuTong.Models.Route();
            //model.UserId = "111";
            //model.RouteNumber = "111";
            //model.ToLatitude = "34.230669";
            //model.ToLongitude = "108.88751";
            return View(model);
        }

        public JsonResult GetMembers(string number)
        {
            //var members = YiLuTong.BLL.Route.GetRouteMembersByNumber(number);
            var members = new List<YiLuTongTrial2.Models.RouteMember>();
            if (SessionHelper.Sessions == null)
            {
                SessionHelper.Sessions = new Dictionary<string, SessionInfo>();
            }

            foreach (var item in SessionHelper.Sessions.Keys)
            {
                var session = SessionHelper.Sessions[item];
                if (session.Route != null && session.Route.RouteNumber == number && !string.IsNullOrEmpty(session.Latitude))
                {
                    members.Add(new YiLuTongTrial2.Models.RouteMember { UserId = session.UserInfo.openid, UserPicture = (session.UserInfo.headimgurl ?? string.Empty).Replace("/0", "/46"), UserName = session.UserInfo.nickname, CurrentLatitude = session.Latitude, CurrentLongitude = session.Longitude });
                }
            }
            //var random = new Random();
            //members.Add(new YiLuTong.Models.RouteMember { UserId = "111", UserName = "aaaa", CurrentLatitude = "34.230669", CurrentLongitude = (108 + random.Next(0, 20) / 100.00).ToString() });
            //members.Add(new YiLuTong.Models.RouteMember { UserId = "222", UserName = "bbbb", CurrentLatitude = "34.230669", CurrentLongitude = (108 + random.Next(0, 20) / 100.00).ToString() });
            //members.Add(new YiLuTong.Models.RouteMember { UserId = "333", UserName = "cccc", CurrentLatitude = "34.230669", CurrentLongitude = (108 + random.Next(0, 20) / 100.00).ToString() });
            //members.Add(new YiLuTong.Models.RouteMember { UserId = "444", UserName = "dddd", CurrentLatitude = "34.230669", CurrentLongitude = (108 + random.Next(0, 20) / 100.00).ToString() });
            //members.Add(new YiLuTong.Models.RouteMember { UserId = "555", UserName = "eee", CurrentLatitude = "34.230669", CurrentLongitude = (108 + random.Next(0, 20) / 100.00).ToString() });
            //members.Add(new YiLuTong.Models.RouteMember { UserId = "666", UserName = "fff", CurrentLatitude = "34.230669", CurrentLongitude = (108 + random.Next(0, 20) / 100.00).ToString() });
            return new JsonResult
            {
                Data = members,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SaveLocation(string lat, string lgd, string userId, string routNumber)
        {
            //YiLuTong.BLL.Route.RefrehLocation(userId, routNumber, lgd, lat);
            return new JsonResult
            {
                Data = true,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        //public ActionResult CreateWay()
        //{
        //    return View();
        //}

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

    }
}
