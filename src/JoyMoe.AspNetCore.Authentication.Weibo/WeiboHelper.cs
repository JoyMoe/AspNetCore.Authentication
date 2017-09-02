using Newtonsoft.Json.Linq;

namespace JoyMoe.AspNetCore.Authentication.Weibo
{
    public class WeiboHelper
    {
        public static string GetId(JObject user) => user.Value<string>("id");

        public static string GetScreenName(JObject user) => user.Value<string>("screen_name");

        public static string GetName(JObject user) => user.Value<string>("name");

        public static string GetProfileImageUrl(JObject user) => user.Value<string>("profile_image_url");      

        public static string GetGender(JObject user) => user.Value<string>("gender");

        public static string GetAvatarLarge(JObject user) => user.Value<string>("avatar_large");

        public static string GetAvatarHD(JObject user) => user.Value<string>("avatar_hd");

        public static string GetCoverImagePhone(JObject user) => user.Value<string>("cover_image_phone");

        public static string GetLocation(JObject user) => user.Value<string>("location");
    }
}
