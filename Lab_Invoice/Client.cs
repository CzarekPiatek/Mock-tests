using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Invoice
{
    public class Client : IEntity
    {
        public int Id { get; set; }
        public string Firstname;
        public string Lastname;
        public IRepository<Invoice> myInvoices;
        public Client(IRepository<Invoice> repo)
        {
            myInvoices = repo;
        }

        public Invoice Buy(List<InvoiceItem> list, int IdClient)
        {
            if (list.Count == 0)
            {
                throw new ArgumentNullException();
            }
            else
            {
                Invoice iv = new Invoice()
                {
                    Id = myInvoices.GetNextID(),
                    IdCustomer = IdClient,
                    Items = list
                };
                myInvoices.Add(iv);
                return iv;
            }
        }

        public Boolean DeleteInvoice(Invoice iv)
        {
            if (myInvoices.FindById(iv.Id) == null)
                return false;
            else
            {
                myInvoices.Delete(iv);
                return true;
            }
        }

        public Boolean Update(Invoice iv)
        {
            if (myInvoices.FindById(iv.Id) == null)
                return false;
            else
            {
                myInvoices.Update(iv);
                return true;
            }
        }

        public double InvoicePrice(Invoice iv)
        {
            if (myInvoices.FindById(iv.Id) != null)
                return myInvoices.InvoicePrice(iv);
            else
                throw new ArgumentNullException();
        }

        public bool Equals(IEntity x, IEntity y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(IEntity obj)
        {
            return obj.Id.GetHashCode();
        }

       
    }
}
