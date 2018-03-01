# Facebook Web Hook support for ASP<i></i>.NET Core projects.

Facebook Graph API Webhooks feature allows apps to receive real-time notifications of changes to selected pieces of data.

# Configuring ASP<i></i>.NET Core project

1. Add reference of Microsoft.AspNetCore.WebHooks.Receivers.Facebook.
2. Modify your `Startup.cs` file - `ConfigureServices` method.
```CSharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc()
    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
    .AddFacebookWebHooks();
}
```
3. Add WebHook configuration in appsettings. This token can be anything length should be minimum 1 and maximum 100. This token should be in the `Subscribe to this Topic` dialog.
```Javascript
"WebHooks": {
  "facebook": {
    "SecretKey": {
      "default": "[Verification Token]"
    }
  }
}
```
4. Add Controller class with `[FacebookWebHook]` attribute.

```CSharp
public class FacebookController : ControllerBase
{
    [FacebookWebHook]
    public IActionResult FacebookHandler(string receiverName, JObject data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok();
    }
}
```
# Configuring Facebook.

## Create WebHook

1. Go to the Dashboard of your Facebook Application : https://developers.facebook.com/apps/
2. Click the Webhooks menu
3. New Subscription > Page
4. Callback Url : https://server/api/webhooks/incoming/facebook/
5. Verify Token : The same one as in appsettings.json
6. Once Subscription created, select names you're interested in.

![WebHook Configuration](/images/webhook_config.png)

## Enable Page subscribe to your app.

1. Open the Graph API Explorer : https://developers.facebook.com/tools/explorer/
2. On the top right Combo Box, select your Application
3. Just below, in the Token ComboBox, select your Page.
4. Select the verb POST for the request
5. Enter the path : {your-page-id}/subscribed_apps
6. Submit : you should get a success.

![Response from Graph API Explorer](/images/graph_api_result.png)

## Done.

Now onwards if you post / share something to page - your application handler will be invoked.

# Demo

![Response from Graph API Explorer](/images/facebook_webhook.gif)

Enjoy :)