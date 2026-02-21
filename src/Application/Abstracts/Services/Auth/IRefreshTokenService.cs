using Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstracts.Services.Auth
{
    public interface IRefreshTokenService
    {
        Task<string> CreateRefreshTokenAsync(User user);
        Task<User?> ValidateAndConsumeAsync(string token);
    }
}
