using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Common;

public abstract class BaseEntity<T>
{
    public T Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
