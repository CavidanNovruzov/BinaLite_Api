using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Constants;
/// <summary>
/// Permission policy adları - Authorization üçün istifadə olunacaq policy adları burada saxlanır
/// </summary>
public static class Policies
{
    public const string ManageCities = "ManageCities";   // şəhər/rayon - yalnız Admin 
    public const string ManageProperties = "ManageProperties"; // elan - giriş etmiş istifadəçi 
}
