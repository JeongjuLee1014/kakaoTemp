using kakaoTemp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace kakaoTemp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly OAuthService _oauthService;

        public OAuthController(OAuthService oauthService)
        {
            _oauthService = oauthService;
        }

        [HttpGet]
        public async Task<IActionResult> HandleOAuthRedirect([FromQuery] string code)
        {
            string access_token = await _oauthService.RequestAccessToken(code);
            long user_id = await _oauthService.RequestUserId(access_token);
            // 회원 가입 또는 로그인 로직
            var loginCompleteResponse = await System.IO.File.ReadAllTextAsync("./loginCompletePage.html");
            return Content(loginCompleteResponse, "text/html");
        }
    }
}