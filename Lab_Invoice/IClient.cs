using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Invoice
{
    public interface IClient<T> where T : IEntity
    {
        IEnumerable<T> List { get; }
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FindById(int Id);
        int GetNextID();
        void CreateInvoiceForClient(Client c, Invoice iv);
        void DeleteClient(Client c);
        Client CreateClient(string FName, string LName);
    }
}
