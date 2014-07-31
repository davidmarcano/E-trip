using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;

namespace YiLuTongTrial2.BLL
{
    public static class SessionHelper
    {
        public static Dictionary<string, SessionInfo> Sessions { get; set; }
        private static object obj = new object();

        //would this function ever end? it's not adding the key?
        public static void Add(string key, SessionInfo SessionInfo)
        {
            lock (obj)
            {
                if (Sessions == null)
                {
                    Sessions = new Dictionary<string, SessionInfo>();
                }

                if (Sessions.ContainsKey(key))
                {
                    Sessions.Remove(key);

                }
                if (SessionInfo.UserInfo != null)
                {
                    if (SessionInfo.UserInfo.headimgurl == null)
                    {
                        SessionInfo.UserInfo.headimgurl = string.Empty;
                    }
                    else
                    {
                        SessionInfo.UserInfo.headimgurl = SessionInfo.UserInfo.headimgurl.Replace("/0", "/46");
                    }
                }
                Sessions.Add(key, SessionInfo);
            }
        }

        public static SessionInfo Get(string key)
        {
            lock (obj)
            {
                if (key == null)
                {
                    return null;
                }

                if (Sessions == null)
                {
                    Sessions = new Dictionary<string, SessionInfo>();
                }

                if (Sessions.ContainsKey(key))
                {
                    return Sessions[key];
                }
                else
                {
                    return null;
                }
            }
        }

        public static void UpdateLocation(string key, string Latitude, string Longitude)
        {
            lock (obj)
            {
                if (Sessions == null)
                {
                    Sessions = new Dictionary<string, SessionInfo>();
                }

                if (Sessions.ContainsKey(key))
                {
                    Sessions[key].Latitude = Latitude;
                    Sessions[key].Longitude = Longitude;
                }
                else
                {
                    Sessions.Add(key, new SessionInfo()
                    {
                        Longitude = Longitude,
                        Latitude = Latitude,
                        AccessTokenResult = new OAuthAccessTokenResult()
                        {
                            openid = key
                        },
                        UserInfo = new WeixinUserInfoResult()
                        {
                            openid = key
                        }
                    });
                }
            }
        }

        public static List<SessionInfo> GetSessionsByRoomNumber(string roomNumber)
        {
            lock (obj)
            {
                if (Sessions == null)
                {
                    Sessions = new Dictionary<string, SessionInfo>();
                }
                var list = new List<SessionInfo>();
                foreach (var item in Sessions)
                {
                    if (item.Value.Route != null)
                    {
                        if (item.Value.Route.RouteNumber == roomNumber)
                        {
                            list.Add(item.Value);
                        }
                    }
                }

                return list;
            }
        }
    }

    //this is a context which contains information about a user's current session.
    public class SessionInfo
    {
        public SessionInfo()
        {
            AccessTokenResult = new OAuthAccessTokenResult();
            UserInfo = new WeixinUserInfoResult();
            Route = new Models.Route();
        }

        public OAuthAccessTokenResult AccessTokenResult { get; set; }
        public WeixinUserInfoResult UserInfo { get; set; }
        public YiLuTongTrial2.Models.Route Route { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}