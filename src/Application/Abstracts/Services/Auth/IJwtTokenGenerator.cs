using Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstracts.Services.Auth;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user,IEnumerable<string> roles);
}
