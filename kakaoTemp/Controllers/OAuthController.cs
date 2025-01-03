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

        // 로그인 플랫폼에 대한 상수 정의
        public enum Platform
        {
            Kakao = 1,
            Google,
            Naver
        }

        public OAuthController(OAuthService oauthService)
        {
            _oauthService = oauthService;
        }

        [HttpGet("/kakao")]
        public async Task<IActionResult> HandleOAuthRedirect([FromQuery] string code)
        {
            string access_token = await _oauthService.RequestAccessToken(code);
            long user_id = await _oauthService.RequestUserId(access_token);
            user_id = long.Parse(Platform.Kakao.ToString() + user_id.ToString());

            if (isJoined(user_id))
            {
                Login();
            }
            else
            {
                Join();
            }

            var loginCompleteResponse = await System.IO.File.ReadAllTextAsync("./loginCompletePage.html");
            return Content(loginCompleteResponse, "text/html");
        }

        public bool isJoined(long user_id)
        {
            Console.WriteLine(user_id);
            return true;
        }

        public void Login()
        {
            Console.WriteLine("로그인");
        }

        public void Join()
        {
            Console.WriteLine("회원 가입");
        }
    }
}