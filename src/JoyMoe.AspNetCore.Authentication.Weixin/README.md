微信登录
===

安装
===

```posh
Install-Package JoyMoe.AspNetCore.Authentication.Weixin
```

使用
===

### Startup.cs

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddAuthentication()
            .AddWeixin(weixinOptions =>
            {
                weixinOptions.AppId = Configuration["Authentication:Weixin:AppId"];
                weixinOptions.AppSecret = Configuration["Authentication:Weixin:AppSecret"];
            })
}
```

| Claim Name                | Descript         |
| ------------------------- | ---------------- |
| ClaimTypes.NameIdentifier | 用户统一标识           |
| ClaimTypes.Name           | 用户昵称             |
| ClaimTypes.Gender         | 性别，1为男性，2为女性     |
| ClaimTypes.Country        | 国家               |
| urn:weixin:openid         | 用户的标识，对当前开发者帐号唯一 |
| urn:weixin:province       | 省份               |
| urn:weixin:city           | 城市               |
| urn:weixin:headimgurl     | 用户头像             |
|                           |                  |
