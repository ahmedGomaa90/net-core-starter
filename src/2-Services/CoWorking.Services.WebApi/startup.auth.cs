using System;
using System.Text;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using CustomTokenAuthProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CoWorking.Data.Context;

namespace CoWorking.Services.WebApi
{
    public partial class Startup
    {

        private void ConfigureAuth(IApplicationBuilder app)
        {

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("TokenAuthentication:SecretKey").Value));


            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                // Validate the token expiry
                ValidateLifetime = true,
                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });



            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Cookie",
                CookieName = Configuration.GetSection("TokenAuthentication:CookieName").Value,
                TicketDataFormat = new CustomJwtDataFormat(
                    SecurityAlgorithms.HmacSha256,
                    tokenValidationParameters)
            });

            var tokenProviderOptions = new TokenProviderOptions
            {
                Path = Configuration.GetSection("TokenAuthentication:TokenPath").Value,
                Audience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                Issuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Expiration = new TimeSpan(24, 0, 0),
                IdentityResolver = GetIdentity
            };

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(tokenProviderOptions));


        }

        // private readonly Func<UserManager<IdentityUser>> _userManagerFactory;

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var passwordhasher = new PasswordHasher<IdentityUser>();
            using (var userManager =
            new UserManager<IdentityUser>(
            new UserStore<IdentityUser>(new CoWorkingIdentityContext()),
            null,
           passwordhasher,
            new List<UserValidator<IdentityUser>>(),
            new List<PasswordValidator<IdentityUser>>(),
            null,
            new IdentityErrorDescriber(),
            null, null))
            {
                var user = await userManager.FindByNameAsync(username);

                var passResult = passwordhasher.VerifyHashedPassword(user, user.PasswordHash, password);

                if (passResult == PasswordVerificationResult.Success)
                    return new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { });

                return null;

            }

            // // Don't do this in production, obviously!
            // if (username == "Ahmed" && password == "asdasd")
            // {
            //     // var manager = new Microsoft.AspNetCore.Identity.UserManager<IdentityUser>()
            //     // return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));

            // }

            // // Credentials are invalid, or account doesn't exist
            // // return Task.FromResult<ClaimsIdentity>(null);
            // return null;

        }

    }
}