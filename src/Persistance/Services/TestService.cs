using Application.Abstracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance.Services;

public class TestService : ITestService
{
    public Guid Id => Guid.NewGuid();   
}
