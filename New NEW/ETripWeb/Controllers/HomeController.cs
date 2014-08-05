using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP;
using YiLuTong.BLL;
using YiLuTong.Models;
using System.Threading.Tasks;

namespace YiLuTong.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private string appId = "wxa6899f45ea1aaa1d";
        private string secret = "f17531456e565f763511431cb1f3a170";

        public ActionResult StartWay()
        {
            return View();
        }

        [HttpGet]
        public JsonResult AddWay(string fromLng, string fromLat, string toLng, string toLat, string userid)
        {
            var session = SessionHelper.Get(userid);
            var user = session.UserInfo;
            var routeModel = YiLuTong.BLL.Route.CreateRoute(fromLng, fromLat, toLng, toLat, user.openid, user.nickname, user.headimgurl);
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
        public void deleteWay(string routenumber, string userid)
        {
            YiLuTong.BLL.Route.ExitRoute(routenumber, userid);
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


        [HttpPost]
        public JsonResult JoinWay(FormCollection form)
        {
            var number = form["number"];
            var userid = form["hidUserId"];
            var lat = form["lat"];
            var lng = form["lng"];

            var session = SessionHelper.Get(userid);


            if (number != null)
            {

                var reponse = YiLuTong.BLL.Route.JoinRoute(number, lat, lng, userid, session.UserInfo.nickname, session.UserInfo.headimgurl);
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
            var model = YiLuTong.BLL.Route.GetRouteByNumber(number);
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
            var members = new List<YiLuTong.Models.RouteMember>();
            if (SessionHelper.Sessions == null)
            {
                SessionHelper.Sessions = new Dictionary<string, SessionInfo>();
            }

            foreach (var item in SessionHelper.Sessions.Keys)
            {
                var session = SessionHelper.Sessions[item];
                if (session.Route != null && session.Route.RouteNumber == number && !string.IsNullOrEmpty(session.Latitude))
                {
                    members.Add(new YiLuTong.Models.RouteMember { UserId = session.UserInfo.openid, UserPicture = (session.UserInfo.headimgurl ?? string.Empty).Replace("/0", "/46"), UserName = session.UserInfo.nickname, CurrentLatitude = session.Latitude, CurrentLongitude = session.Longitude });
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

        public JsonResult IamArrived(string userId)
        {
            YiLuTong.BLL.Route.IMArrived(userId);
            return new JsonResult
            {
                Data = true,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult IamHidden(string userId)
        {
            YiLuTong.BLL.Route.IMHidden(userId);
            return new JsonResult
            {
                Data = true,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }
        //to handle the exiting routine
        public ActionResult Endtrip(string routenumber, string userid)
        {
            ViewData["routenumber"] = routenumber;
            ViewData["userid"] = userid;
            return View();
        }
    }
}
