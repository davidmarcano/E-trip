using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YiLuTongTrial2.Models;

namespace YiLuTongTrial2.BLL
{
    public static class Route
    {
        public static void RefrehLocation(string userId, string routeNumber, string Longitude, string Latitude)
        {
            DBDataContext db = new DBDataContext();
            var routeMember = db.RouteMembers.FirstOrDefault(T => T.UserId == userId && T.RouteNumber == routeNumber);
            if (routeNumber != null)
            {
                routeMember.CurrentLongitude = Longitude;
                routeMember.CurrentLatitude = Latitude;
                db.SubmitChanges();
            }
        }

        public static YiLuTongTrial2.Models.Route GetRouteByUserId(string userId)
        {
            DBDataContext db = new DBDataContext();
            var routeMembers = db.RouteMembers.FirstOrDefault(T => T.UserId == userId);
            if (routeMembers != null)
            {
                var route = db.Routes.FirstOrDefault(T => T.RouteNumber == routeMembers.RouteNumber);
                return route;
            }

            return null;
        }

        public static YiLuTongTrial2.Models.Route GetRouteByNumber(string routeNumber)
        {
            DBDataContext db = new DBDataContext();
            var route = db.Routes.FirstOrDefault(T => T.RouteNumber == routeNumber);
            return route;
        }

        public static List<YiLuTongTrial2.Models.RouteMember> GetRouteMembersByNumber(string routeNumber)
        {
            DBDataContext db = new DBDataContext();
            var route = db.RouteMembers.Where(T => T.RouteNumber == routeNumber);
            return route.ToList();
        }

        /// <summary>
        /// 创建路线
        /// </summary>
        /// <param name="FromLatitude">当前经度</param>
        /// <param name="FromLongitude">当前纬度</param>
        /// <param name="ToLatitude">目的地经度</param>
        /// <param name="ToLongitude">目的地纬度</param>
        /// <param name="UserId">微信用户ID</param>
        /// <param name="UserName">微信用户名称</param>
        /// <param name="UserPicture">用户头像</param>
        /// <returns></returns>
        public static CreateRouteResponse CreateRoute(string FromLongitude, string FromLatitude, string ToLongitude, string ToLatitude, string UserId, string UserName, string UserPicture)
        {
            var routeNumber = new Random().Next(0, 9999).ToString("0000");
            DBDataContext db = new DBDataContext();
            if (db.Routes.Count(T => T.RouteNumber == routeNumber) > 0)
            {
                routeNumber = new Random().Next(0, 9999).ToString("0000");
            }

            var route = new YiLuTongTrial2.Models.Route()
            {
                CreateTime = DateTime.Now,
                FromLatitude = FromLatitude,
                FromLongitude = FromLongitude,
                IsOpen = true,
                RouteNumber = routeNumber.ToString(),
                ToLatitude = ToLatitude,
                ToLongitude = ToLongitude,
                UserId = UserId,
                UserName = UserName,
                UserPicture = UserPicture
            };

            var routeMember = new YiLuTongTrial2.Models.RouteMember()
            {
                CurrentLatitude = FromLatitude,
                CurrentLongitude = FromLongitude,
                IsHost = true,
                OnRouting = true,
                RouteNumber = routeNumber,
                UserId = UserId,
                UserName = UserName,
                UserPicture = UserPicture,
            };


            db.Routes.InsertOnSubmit(route);
            db.RouteMembers.InsertOnSubmit(routeMember);
            db.SubmitChanges();

            return new CreateRouteResponse()
            {
                IsSuccess = true,
                Message = string.Empty,
                RouteId = route.RouteId,
                UserId = UserId,
                RouteNumber = routeNumber.ToString()
            };
        }

        /// <summary>
        /// 加入路线
        /// </summary>
        /// <param name="RouteNumber">房间号码</param>
        /// <param name="FormLatitude">当前经度</param>
        /// <param name="FromLongitude">当前纬度</param>
        /// <param name="UserId">微信用户ID</param>
        /// <param name="UserName">微信用户名称</param>
        /// <param name="UserPicture">用户头像</param>
        /// <returns></returns>
        public static JoinRouteResponse JoinRoute(string RouteNumber, string FormLatitude, string FromLongitude, string UserId, string UserName, string UserPicture)
        {
            //This creates a context in which to search the database with
            DBDataContext db = new DBDataContext();

            //response contains a Message and a IsSuccess boolean
            var response = new JoinRouteResponse();

            //this checks the database for the give RouteNumber
            var route = db.Routes.FirstOrDefault(T => T.RouteNumber == RouteNumber);

            //if there is no such route
            if (route == null)
            {
                response.IsSuccess = false;
                response.Message = "路线不存在";
                return response;
            }

            //If the route is not open (i.e the deadline has passed, or we have closed it)
            if (!route.IsOpen)
            {
                response.IsSuccess = false;
                response.Message = "路线已经关闭";
                return response;
            }

            //this checks if you are already in the database/route
            var routeMembers = db.RouteMembers.Where(T => T.UserId == UserId);

            //if you are already in the database
            if (routeMembers != null && routeMembers.Count() > 0)
            {
                //if the route you are trying to enter is NOT the one you already have
                if (routeMembers.Count(T => T.RouteNumber != RouteNumber) > 0)
                {
                    response.IsSuccess = false;
                    response.Message = "您已经在其他的路线中，不能重复加入，请点击‘我的路线’菜单后，点击退出即可";
                    return response;
                }
                //if the route you are trying to enter is the one you ALREADY are in
                else
                {
                    response.IsSuccess = false;
                    response.Message = "您已经加入该路线中，请勿重复加入";
                    return response;
                }
            }

            //if you are NOT already in the database
            else
            {
                var routeMember = new YiLuTongTrial2.Models.RouteMember()
                {
                    CurrentLatitude = FormLatitude,
                    CurrentLongitude = FromLongitude,
                    IsHost = false,
                    OnRouting = true,
                    RouteNumber = RouteNumber,
                    UserId = UserId,
                    UserName = UserName,
                    UserPicture = UserPicture,
                };

                db.RouteMembers.InsertOnSubmit(routeMember);
                db.SubmitChanges();

                response.IsSuccess = true;
                response.Route = route;
                response.Message = "";
                return response;
            }
        }

        /// <summary>
        /// 退出路线
        /// </summary>
        /// <param name="RouteNumber">房间号码</param>
        /// <param name="UserId">微信用户ID</param>
        public static void ExitRoute(string RouteNumber, string UserId)
        {
            DBDataContext db = new DBDataContext();
            var members = db.RouteMembers.FirstOrDefault(T => T.RouteNumber == RouteNumber && T.UserId == UserId);
            if (members != null)
            {
                db.RouteMembers.DeleteOnSubmit(members);
                db.SubmitChanges();

                if (db.RouteMembers.Count(T => T.RouteNumber == RouteNumber) == 0)
                {
                    var route = db.Routes.FirstOrDefault(T => T.RouteNumber == RouteNumber);
                    db.Routes.DeleteOnSubmit(route);
                    db.SubmitChanges();
                }
            }
        }
    }
}