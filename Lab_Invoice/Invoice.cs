using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Invoice
{
    public class Invoice : IEntity
    {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public List<InvoiceItem> Items { get; set; }

        public bool Equals(IEntity x, IEntity y)
        {
            return x.Id == y.Id;
        }

        public List<InvoiceItem> GetItems()
        {
            return this.Items;
        }

        public int GetHashCode(IEntity obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
