using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Senparc.Weixin.MP.Entities;
using System.Web.Caching;

namespace YiluTongTrial.BLL
{
    public class Untiy
    {
        public static string GetAccessTokenFromCache(string appID, string appsecret)
        {
            //if (HttpContext.Current.Cache != null && HttpContext.Current.Cache.Get(appID) != null)
            //{
            //    return (HttpContext.Current.Cache.Get(appID) as AccessTokenResult).access_token;
            //}
            //else
            //{
            var token = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetToken(appID, appsecret);
            HttpContext.Current.Cache.Add(appID, token, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(110),
System.Web.Caching.CacheItemPriority.NotRemovable, null);
            return token.access_token;
            //}
        }
    }
}