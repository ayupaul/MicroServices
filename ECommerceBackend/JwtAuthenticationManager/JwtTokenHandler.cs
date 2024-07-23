using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;


namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string Secret_Key = "ayush_pal_my_scret_key_12345.....";
        public const int Jwt_Token_Validity_Mins = 20;
        UserDataSource source=new UserDataSource();
        private List<UserAccount> _accounts;
        //private readonly List<UserAccount> _accounts;
        public JwtTokenHandler()
        {
            //_accounts.Add(new UserAccount { UserName = "admin@gmail.com", Password = "admin123@", Role = "admin" });
            //_accounts.Add(new UserAccount { UserName = "ayush@gmail.com", Password = "ayush123@", Role = "user" });
            _accounts=source.GetAllUserDetails();
        }
        public AuthenticationResponse? GenerateToken(AuthenticationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            {
                return null;
            }
            var userAccount = _accounts.FirstOrDefault(it => it.UserName == request.UserName && it.Password == request.Password);
            if (userAccount == null)
            {
                return null;
            }
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(Jwt_Token_Validity_Mins);
            var tokenKey = Encoding.ASCII.GetBytes(Secret_Key);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,request.UserName),
                new Claim("Role",userAccount.Role)
            });
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return new AuthenticationResponse
            {
                UserName = userAccount.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                Token = token,
            };
        }
    }
}
