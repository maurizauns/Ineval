using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.Dto
{
    public class ApiCycling
    {
        public static async Task<Root> GetByCycling(string p1, string p2, string p3)
        {
            var result = await RequestClient.GetItem(Routes.GpMap.Cycling.Replace("<P1>",p1).Replace("<P2>", p2).Replace("<P3>", p3));
            Root list = JsonConvert.DeserializeObject<Root>(result);
            return list;
        }

        public class Leg
        {
            public string summary { get; set; }
            public double weight { get; set; }
            public double duration { get; set; }
            public List<object> steps { get; set; }
            public double distance { get; set; }
        }

        public class Route
        {
            public string geometry { get; set; }
            public List<Leg> legs { get; set; }
            public string weight_name { get; set; }
            public double weight { get; set; }
            public double duration { get; set; }
            public double distance { get; set; }
        }

        public class Waypoint
        {
            public double distance { get; set; }
            public string name { get; set; }
            public List<double> location { get; set; }
        }

        public class Root
        {
            public List<Route> routes { get; set; }
            public List<Waypoint> waypoints { get; set; }
            public string code { get; set; }
            public string uuid { get; set; }
        }
    }
}
