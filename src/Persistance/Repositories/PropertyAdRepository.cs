using Application.Abstracts.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories;

public class PropertyAdRepository:GenericRepository<PropertyAd,int>, IPropertyAdRepository
{
    public PropertyAdRepository(BinaLiteDbContext context):base(context)
    {
    }
}
