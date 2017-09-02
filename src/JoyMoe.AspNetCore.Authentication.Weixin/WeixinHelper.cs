using Newtonsoft.Json.Linq;

namespace JoyMoe.AspNetCore.Authentication.Weixin
{
    public static class WeixinHelper
    {
        public static string GetOpenId(JObject user) => user.Value<string>("openid");

        public static string GetNickname(JObject user) => user.Value<string>("nickname");

        public static string GetSex(JObject user) => user.Value<string>("sex");

        public static string GetProvince(JObject user) => user.Value<string>("province");

        public static string GetCity(JObject user) => user.Value<string>("city");

        public static string GetCountry(JObject user) => user.Value<string>("country");

        public static string GetHeadimgUrl(JObject user) => user.Value<string>("headimgurl");

        public static string GetUnionid(JObject user) => user.Value<string>("unionid");
    }
}
