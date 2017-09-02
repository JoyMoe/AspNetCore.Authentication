using Microsoft.AspNetCore.Http;
using JoyMoe.AspNetCore.Authentication.Baidu;

namespace Microsoft.AspNetCore.Builder
{
    public class BaiduOptions : OAuthOptions
    {
        public BaiduOptions()
        {
            AuthenticationScheme = BaiduDefaults.AuthenticationScheme;
            DisplayName = AuthenticationScheme;
            ClaimsIssuer = BaiduDefaults.Issuer;
            CallbackPath = new PathString(BaiduDefaults.CallbackPath);

            AuthorizationEndpoint = BaiduDefaults.AuthorizationEndpoint;
            TokenEndpoint = BaiduDefaults.TokenEndpoint;           
            UserInformationEndpoint = BaiduDefaults.UserInformationEndpoint;          
        }  
        
        public string Appkey
        {
            get
            {
                return ClientId;
            }
            set
            {
                ClientId = value;
            }
        }

        public string SecretKey
        {
            get
            {
                return ClientSecret;
            }
            set
            {
                ClientSecret = value;
            }
        }
    }
}
