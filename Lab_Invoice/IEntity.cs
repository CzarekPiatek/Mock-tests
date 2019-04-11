using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Invoice
{
    public interface IEntity : IEqualityComparer<IEntity>
    {
        int Id { get; set; }
    }
}
