QQ帐号登录
===

安装
===

```posh
Install-Package JoyMoe.AspNetCore.Authentication.QQ
```

使用
===

### Startup.cs

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddAuthentication()
            .AddQQ(qqOptions =>
            {
                qqOptions.AppId = Configuration["Authentication:QQ:AppId"];
                qqOptions.AppKey = Configuration["Authentication:QQ:AppKey"];
            })
}
```

| Claim Name                | Descript               |
| ------------------------- | ---------------------- |
| ClaimTypes.NameIdentifier | 用户身份的OpenID            |
| ClaimTypes.Name           | 用户在QQ空间的昵称             |
| ClaimTypes.Gender         | 性别                     |
| urn:qq:figureurl          | 大小为30×30像素的QQ空间头像URL   |
| urn:qq:figureurl_1        | 大小为50×50像素的QQ空间头像URL   |
| urn:qq:figureurl_2        | 大小为100×100像素的QQ空间头像URL |
| urn:qq:figureurl_qq_1     | 大小为40×40像素的QQ头像URL     |
| urn:qq:figureurl_qq_2     | 大小为100×100像素的QQ头像URL   |
|                           |                        |
