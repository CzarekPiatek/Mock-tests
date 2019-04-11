using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Invoice
{
    class ClientRepository : IClient<Client>
    {
        private List<Client> dataSource = new List<Client>();
        private IClient<Client> Client;
        public IEnumerable<Client> List => dataSource;

        public ClientRepository(IClient<Client> repo)
        {
            Client = repo;

        }
        public Client CreateClient(string FName, string LName)
        {
            var client = new Client(null)
            {
                Id = Client.GetNextID(),
                Firstname = FName,
                Lastname = LName
            };

            Client.Add(client);
            return client;
        }

        public void CreateInvoiceForClient(Client c, Invoice iv)
        {
            if (Client.FindById(c.Id) != null)
               c.Buy(iv.Items, c.Id);
        }

        public void DeleteClient(Client c)
        {
            if (Client.FindById(c.Id) != null)
            {
                if (c.myInvoices.ReturnAll() != null)
                {
                    foreach (Invoice iv in c.myInvoices.ReturnAll())
                        c.DeleteInvoice(iv);
                }
                Client.Delete(c);
            }
        }

        public void Add(Client entity)
        {
            if (FindById(entity.Id) == null)
                dataSource.Add(entity);
        }

        public void Delete(Client entity)
        {
            dataSource.Remove(entity);
        }

        public Client FindById(int Id)
        {
            return dataSource.Find(x => x.Id == Id);
        }

        public void Update(Client entity)
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
    }
}
