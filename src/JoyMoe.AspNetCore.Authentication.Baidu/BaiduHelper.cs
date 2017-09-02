using Newtonsoft.Json.Linq;

namespace JoyMoe.AspNetCore.Authentication.Baidu
{
    public class BaiduHelper
    {
        public static string GetId(JObject user) => user.Value<string>("uid");

        public static string GetName(JObject user) => user.Value<string>("uname");

        public static string GetPortrait(JObject user) => user.Value<string>("portrait");       
    }
}
