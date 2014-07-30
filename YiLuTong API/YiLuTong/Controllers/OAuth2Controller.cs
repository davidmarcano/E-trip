using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP;
using YiLuTong.BLL;
using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;
using System.IO;
using Senparc.Weixin.MP.Entities;

namespace YiLuTong.Controllers
{
    public class OAuth2Controller : Controller
    {
        //下面换成账号对应的信息，也可以放入web.config等地方方便配置和更换
        private string appId = "wxbf475b9a09f4e353";
        private string secret = "6293389fbb293e55ae62107f453c92fd";
        private readonly string Token = "458828";

        public ActionResult Index()
        {
            //此页面引导用户点击授权
            var url = OAuth.GetAuthorizeUrl(appId,
                "http://115.28.130.182/OAuth2/IndexCallback", "JeffreySu", OAuthScope.snsapi_userinfo);
            ViewData["Url"] = url;
            Response.Redirect(url);
            return View();
        }

        public ActionResult IndexCallback(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state != "JeffreySu")
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下，实际上可以存任何想传递的数据，比如用户ID
                return Content("验证失败！请从正规途径进入！");
            }

            //通过，用code换取access_token
            var result = OAuth.GetAccessToken(appId, secret, code);
            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }

            var useSession = SessionHelper.Get(result.openid);
            var routeModel = new YiLuTong.Models.Route();
            var userInfo = new WeixinUserInfoResult();
            var webAccessToken = Untiy.GetAccessTokenFromCache(appId, secret);
            userInfo = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetUserInfo(webAccessToken, result.openid);
            if (useSession != null)
            {
               
                    useSession.UserInfo = userInfo;
                

                routeModel = Route.GetRouteByUserId(userInfo.openid);
                useSession.Route = routeModel;
            }
            else
            {
                useSession = new SessionInfo();
                useSession.UserInfo = userInfo;
                routeModel = Route.GetRouteByUserId(userInfo.openid);
                useSession.Route = routeModel;
            }
            
            if (routeModel != null)
            {
                return View("~/Views/Home/Routing.cshtml", routeModel);
            }
            else
            {
                return View("~/Views/Home/CreateWay.cshtml", userInfo);
            }
        }

        public ActionResult JoinWayCallback(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state != "JeffreySu")
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下，实际上可以存任何想传递的数据，比如用户ID
                return Content("验证失败！请从正规途径进入！");
            }

            //通过，用code换取access_token
            var result = OAuth.GetAccessToken(appId, secret, code);
            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }

            var useSession = SessionHelper.Get(result.openid);
            var routeModel = new YiLuTong.Models.Route();
            var userInfo = new WeixinUserInfoResult();

            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }

            
            var webAccessToken = Untiy.GetAccessTokenFromCache(appId, secret);
            userInfo = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetUserInfo(webAccessToken, result.openid);
            if (useSession != null)
            {

                useSession.UserInfo = userInfo;


                routeModel = Route.GetRouteByUserId(userInfo.openid);
                useSession.Route = routeModel;
            }
            else
            {
                useSession = new SessionInfo();
                useSession.UserInfo = userInfo;
                routeModel = Route.GetRouteByUserId(userInfo.openid);
                useSession.Route = routeModel;
            }

            if (routeModel != null)
            {
                return View("~/Views/Home/Routing.cshtml", routeModel);
            }
            else
            {
                return View("~/Views/Home/JoinWay.cshtml", userInfo);
            }
        }

        public ActionResult JoinWay(string userId, string number)
        {

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(number))
            {
                var webAccessToken = Untiy.GetAccessTokenFromCache(appId, secret);
                var userInfo = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetUserInfo(webAccessToken, userId);
                var reponse = YiLuTong.BLL.Route.JoinRoute(number, "34", "180", userId, userInfo.nickname, userInfo.headimgurl);
            }

            var url = OAuth.GetAuthorizeUrl(appId,
                "http://115.28.130.182/OAuth2/JoinWayCallback", "JeffreySu", OAuthScope.snsapi_userinfo);
            ViewData["Url"] = url;
            Response.Redirect(url);
            return View();
        }

        public void Received()
        {
            string signature = Request["signature"];
            string timestamp = Request["timestamp"];
            string nonce = Request["nonce"];
            string echostr = Request["echostr"];
            if (Request.HttpMethod == "GET")
            {
                #region get method - 仅在微信后台填写URL验证时触发
                if (CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    WriteContent(echostr); //返回随机字符串则表示验证通过
                }
                else
                {
                    WriteContent("failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, Token) + "。" +
                                "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
                }
                Response.End();
                #endregion
            }
            else
            {
                //post method - 当有用户想公众账号发送消息时触发
                if (!CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    WriteContent("参数错误！");
                    return;
                }
                //v4.2.2之后的版本，可以设置每个人上下文消息储存的最大数量，防止内存占用过多，如果该参数小于等于0，则不限制
                var maxRecordCount = 10;
                //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
                var messageHandler = new CustomMessageHandler(Request.InputStream, maxRecordCount);
                try
                {
                    //测试时可开启此记录，帮助跟踪数据
                    messageHandler.RequestDocument.Save(Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Request_" + messageHandler.RequestMessage.FromUserName + ".txt"));
                    //执行微信处理过程
                    messageHandler.Execute();
                    //测试时可开启，帮助跟踪数据
                    messageHandler.ResponseDocument.Save(Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Response_" + messageHandler.ResponseMessage.ToUserName + ".txt"));
                    WriteContent(messageHandler.ResponseDocument.ToString());
                    return;
                }
                catch (Exception ex)
                {
                    using (TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/Error_" + DateTime.Now.Ticks + ".txt")))
                    {
                        tw.WriteLine(ex.Message);
                        tw.WriteLine(ex.InnerException.Message);
                        if (messageHandler.ResponseDocument != null)
                        {
                            tw.WriteLine(messageHandler.ResponseDocument.ToString());
                        }
                        tw.Flush();
                        tw.Close();
                    }
                    WriteContent("");
                }
                finally
                {
                    Response.End();
                }
            }
        }

        private void WriteContent(string str)
        {
            Response.Output.Write(str);
        }
    }
}