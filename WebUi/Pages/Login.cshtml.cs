using Application.Command.User;
using Application.Common;
using Application.DataTransferObject;
using Application.Service;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace WebUi.Pages;

public class LoginModel (IMediator mediator, IJanService janService) : PageModel
{
    private readonly IMediator _mediator = mediator;
    private readonly IJanService _janService = janService;
    public async Task<IActionResult> OnGet(string token)
    {

        if (User.Identity!.IsAuthenticated)
        {
            await HttpContext.SignOutAsync();
        }

        if (string.IsNullOrEmpty(token))
        {
            return Redirect("/AccessDenied");
        }
        BaseResult<JanUserDto> checkAccess = await _janService.
            ChackUserAccessForLoginAsync(new LoginDto()
            {
                token = token,
                path = "/token"
            });
        if (checkAccess.IsSuccess is false)
        {
            return Redirect("/AccessDenied");
        }

        JanUserDto janUser = checkAccess.Data!;
        BaseResult registerSignInUser =
            await _mediator.Send
                    (new SignInCommand(
                    janUser.username,
                    janUser.name,
                    janUser.mobile,
                    janUser.image));
        if (registerSignInUser.IsSuccess is false)
        {
            return Page();
        }
        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, janUser.username!),
                new Claim(ClaimTypes.Name, janUser.name!),
                new Claim(ClaimTypes.UserData, "Correspondence"),
            };

        var claimsIdentity = new ClaimsIdentity(claims,
            CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.
            AuthenticationScheme,
            claimsPrincipal);


        return Redirect("/Dashboard");
    }
}

