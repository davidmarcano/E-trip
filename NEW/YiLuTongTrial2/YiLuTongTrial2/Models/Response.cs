using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YiLuTongTrial2.Models;

namespace YiLuTongTrial2.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class CreateRouteResponse : Response
    {
        public int RouteId { get; set; }
        public string UserId { get; set; }
        public string RouteNumber { get; set; }
        public Route Route { get; set; }
    }

    public class JoinRouteResponse : Response
    {
        public Route Route { get; set; }
    }
}