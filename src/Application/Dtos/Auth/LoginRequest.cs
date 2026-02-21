using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Auth;

public class LoginRequest
{
    /// <summary>
    /// email və ya username
    /// </summary>
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!; 
}
