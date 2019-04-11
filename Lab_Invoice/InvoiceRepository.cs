using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Invoice
{
    class InvoiceRepository : IRepository<Invoice>
    {
        public IRepository<Invoice> myInvoices;
        private List<Invoice> dataSource = new List<Invoice>();
        public IEnumerable<Invoice> List => dataSource;
       
        public void Add(Invoice entity)
        {
            if (FindById(entity.Id) == null)
                dataSource.Add(entity);
        }

        public void Delete(Invoice entity)
        {
            dataSource.Remove(entity);
        }

        public Invoice FindById(int Id)
        {
            return dataSource.Find(x => x.Id == Id);
        }

        public void Update(Invoice entity)
        {
            if (FindById(entity.Id) != null)
                Delete(entity);
            else
                throw new ArgumentException();
            Add(entity);
        }
        public int GetNextID()
        {
            return dataSource[dataSource.Count - 1].Id + 1;
        }

        public double InvoicePrice(Invoice entity)
        {
            double price = 0;
            if (FindById(entity.Id) != null)
            {
                foreach (InvoiceItem items in entity.GetItems())
                {
                    price += items.Price;
                }
            }
            return price;
        }

        public List<Invoice> ReturnAll()
        {
            return dataSource;
        }
    }
}
