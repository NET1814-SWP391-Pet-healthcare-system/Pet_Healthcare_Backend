using Entities;
using ServiceContracts.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface ITokenService
    {
        AuthenticationResponse CreateToken(User user, string role);
        ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
    }
}
