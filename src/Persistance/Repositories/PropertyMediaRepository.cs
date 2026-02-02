using Application.Abstracts.Repositories;
using Domain.Entities;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance.Repositories;

public class PropertyMediaRepository : GenericRepository<PropertyMedia,int>,IPropertyMediaRepository
{
    public PropertyMediaRepository(BinaLiteDbContext context):base(context)
    {
    }
}
