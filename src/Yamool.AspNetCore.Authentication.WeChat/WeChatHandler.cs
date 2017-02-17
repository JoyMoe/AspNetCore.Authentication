﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Yamool.AspNetCore.Authentication.WeChat
{
    public class WeChatHandler : OAuthHandler<WeChatOptions>
    {
        public WeChatHandler(HttpClient client) : base(client) { }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var queryString = new Dictionary<string, string>()
            {
                {"access_token",tokens.AccessToken },
                {"openid",tokens.Response.Value<string>("openid") }
            };
            var endpoint = QueryHelpers.AddQueryString(Options.UserInformationEndpoint, queryString);
            var response = await Backchannel.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError($"An error occurred while retrieving the user information: the remote server returned a " +
                  $"{response.StatusCode} response with the following payload: {await response.Content.ReadAsStringAsync()}.");

                throw new HttpRequestException("An error occurred when retrieving user information.");
            }

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

            // checking a remote server error response.
            if (!string.IsNullOrEmpty(payload.Value<string>("errcode")))
            {
                Logger.LogError($"An error occurred while retrieving the user information: the remote server returned a " +
                  $"{response.StatusCode} response with the following payload: {await response.Content.ReadAsStringAsync()}.");

                throw new HttpRequestException("An error occurred when retrieving user information.");
            }

            var unionid = WeChatHelper.GetUnionid(payload);
            if (!string.IsNullOrEmpty(unionid))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, unionid, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var nickname = WeChatHelper.GetNickname(payload);
            if (!string.IsNullOrEmpty(nickname))
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, nickname, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var sex = WeChatHelper.GetSex(payload);
            if (!string.IsNullOrEmpty(sex))
            {
                identity.AddClaim(new Claim(ClaimTypes.Gender, sex, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var country = WeChatHelper.GetCountry(payload);
            if (!string.IsNullOrEmpty(country))
            {
                identity.AddClaim(new Claim(ClaimTypes.Country, country, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var openid = WeChatHelper.GetOpenId(payload);
            if (!string.IsNullOrEmpty(openid))
            {
                identity.AddClaim(new Claim("urn:weixin:openid", openid, ClaimValueTypes.String, Options.ClaimsIssuer));
            }


            var province = WeChatHelper.GetProvince(payload);
            if (!string.IsNullOrEmpty(province))
            {
                identity.AddClaim(new Claim("urn:weixin:province", province, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var city = WeChatHelper.GetCity(payload);
            if (!string.IsNullOrEmpty(city))
            {
                identity.AddClaim(new Claim("urn:weixin:city", city, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var headimgurl = WeChatHelper.GetHeadimgUrl(payload);
            if (!string.IsNullOrEmpty(headimgurl))
            {
                identity.AddClaim(new Claim("urn:weixin:headimgurl", headimgurl, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, properties, Options.AuthenticationScheme);

            var context = new OAuthCreatingTicketContext(ticket, Context, Options, Backchannel, tokens);

            await Options.Events.CreatingTicket(context);
            return context.Ticket;
        }

        protected override async Task<OAuthTokenResponse> ExchangeCodeAsync(string code, string redirectUri)
        {
            var queryString = new Dictionary<string, string>()
            {
                {"appid",Options.AppId },
                {"secret",Options.AppSecret },
                {"code",code },
                {"grant_type","authorization_code" }
            };
            var endpoint = QueryHelpers.AddQueryString(Options.TokenEndpoint, queryString);
            var response = await Backchannel.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                var error = "OAuth token endpoint failure: " + await response.Content.ReadAsStringAsync();
                return OAuthTokenResponse.Failed(new Exception(error));
            }
            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

            // checking a remote server error response.
            if (!string.IsNullOrEmpty(payload.Value<string>("errcode")))
            {
                var error = "OAuth token endpoint failure: " + payload.ToString();
                return OAuthTokenResponse.Failed(new Exception(error));
            }
            return OAuthTokenResponse.Success(payload);
        }

        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {
            var scope = FormatScope();
            var state = Options.StateDataFormat.Protect(properties);

            var queryString = new Dictionary<string, string>()
            {
                {"appid",Options.AppId },
                { "scope", scope },
                { "response_type", "code" },
                { "redirect_uri", redirectUri },
                { "state", state }
            };
            return QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, queryString);
        }

        protected override string FormatScope()
        {
            return string.Join(",", Options.Scope);
        }
    }
}
