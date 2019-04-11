using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;
using NUnit.Framework;
using Lab_Invoice;

namespace Lab_Invoice.Tests
{
    [TestFixture]
    class RepositoryBasicTests
    {
        [Test]
        public void TheRepositoryRemoveShouldBeCalled()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            mockRepository.Setup(x => x.Add(It.IsAny<Invoice>()));
            var sut = new Client(mockRepository.Object);
            var Items = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    Name = "Produkt",
                    Price = 200,
                    Tax = 23
                }
            };
            sut.Buy(Items, 0);
            mockRepository.VerifyAll();
        }
        [Test]
        public void TheRepositoryUpdateShouldReturnFalseWhenNotFind()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            mockRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => null);
            var sut = new Client(mockRepository.Object);
            Assert.That(sut.Update(new Invoice()), Is.False);
        }

        [Test]
        public void TheRepositoryUpdateShouldReturnTrueWhenNotFind()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            mockRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => new Invoice());
            var sut = new Client(mockRepository.Object);
            Assert.That(sut.Update(new Invoice()), Is.True);
        }
        [Test]
        public void TheRepositoryUpdateShouldBeCalled()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            mockRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => new Invoice());
            var sut = new Client(mockRepository.Object);
            sut.Update(new Invoice());
            mockRepository.Verify(x => x.Update(It.IsAny<Invoice>()));
        }
        [Test]
        public void TheRepositoryDeleteInvoiceShouldReturnFalseWhenNotFind()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            mockRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => null);
            var sut = new Client(mockRepository.Object);
            Assert.That(sut.DeleteInvoice(new Invoice()), Is.False);
        }
        [Test]
        public void TheRepositoryDeleteInoviceShouldReturnTrueWhenFind()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            mockRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => new Invoice());
            var sut = new Client(mockRepository.Object);
            Assert.That(sut.DeleteInvoice(new Invoice()), Is.True);
        }
        [Test]
        public void TheRepositoryDeleteShouldBeCalledWhenFind()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            mockRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => new Invoice());
            var sut = new Client(mockRepository.Object);
            sut.DeleteInvoice(new Invoice());
            mockRepository.Verify(x => x.Delete(It.IsAny<Invoice>()), Times.Once);
        }
        
        [Test]
        public void TheRepositoryDeleteInvoiceShouldBeCalledWhenFind()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            mockRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => new Invoice());
            var sut = new Client(mockRepository.Object);
            sut.DeleteInvoice(new Invoice());
            mockRepository.Verify(x => x.Delete(It.IsAny<Invoice>()), Times.Once);
        }
        [Test]
        public void TheRepositoryAddShouldBeCalled()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            var Items = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    Name = "Produkt",
                    Price = 200,
                    Tax = 23
                },
                new InvoiceItem
                {
                    Name = "Produkt2",
                    Price = 300,
                    Tax = 19
                },
                new InvoiceItem
                {
                    Name = "Produkt3",
                    Price = 400,
                    Tax = 23
                },
                new InvoiceItem
                {
                    Name = "Produkt4",
                    Price = 500,
                    Tax = 23
                }
            };
            var sut = new Client(mockRepository.Object);
            sut.Buy(Items, 1);
            mockRepository.Verify(x => x.Add(It.IsAny<Invoice>()), Times.Once);
        }
        [Test]
        public void TheRepositoryAddShouldThrowExceptionIfListIsEmpty()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            var sut = new Client(mockRepository.Object);
            Assert.That(() => sut.Buy(new List<InvoiceItem>(),0),Throws.ArgumentNullException);
        }
        [Test]
        public void TheRepositoryAddShouldBeCalledOncePerManyInvoiceItems()
        {
            var ListOfInvoiceItems = new List<InvoiceItem>
                    {
                        new InvoiceItem
                            {
                                Name = "Produkt",
                                Price = 200,
                                Tax = 23
                            },
                        new InvoiceItem
                            {
                                Name = "Produkt2",
                                Price = 300,
                                Tax = 19
                            },
                        new InvoiceItem
                            {
                                Name = "Produkt3",
                                Price = 400,
                                Tax = 23
                            },
                        new InvoiceItem
                            {
                               Name = "Produkt4",
                               Price = 500,
                               Tax = 23
                            }
                    };
            var mockRepository = new Mock<IRepository<Invoice>>();
            var Client = new Client(mockRepository.Object);
            Client.Buy(ListOfInvoiceItems, 1);
            mockRepository.Verify(x => x.Add(It.IsAny<Invoice>()), Times.Once);
        }
        [Test]
        public void TheRepositoryAddShouldCallWhenItsClientAndInvoice()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            var mockClient = new Mock<IClient<Client>>();
            var Items = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    Name = "Produkt",
                    Price = 200,
                    Tax = 23
                },
                new InvoiceItem
                {
                    Name = "Produkt2",
                    Price = 300,
                    Tax = 19
                },
                new InvoiceItem
                {
                    Name = "Produkt3",
                    Price = 400,
                    Tax = 23
                },
                new InvoiceItem
                {
                    Name = "Produkt4",
                    Price = 500,
                    Tax = 23
                }
            };
            Invoice Invoice = new Invoice() { Items = Items };
            var Client = new Client(mockRepository.Object) { Firstname = "Cezary", Lastname = "Piatek" };
            mockRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => new Invoice());
            mockClient.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => new Client(mockRepository.Object));
            ClientRepository sut = new ClientRepository(mockClient.Object);
            sut.CreateInvoiceForClient(Client, Invoice);
            mockRepository.Verify(x => x.Add(It.IsAny<Invoice>()));
        }
        [Test]
        public void TheRepositoryAddShouldNotBeCalledWhenClientNotFind()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            var mockService = new Mock<IClient<Client>>();
            mockService.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => null);
            var sut = new ClientRepository(mockService.Object);
            sut.CreateInvoiceForClient( new Client(mockRepository.Object), new Invoice());
            mockRepository.Verify(x => x.Add(It.IsAny<Invoice>()), Times.Never);
        }
        
        [Test]
        public void TheRepositoryGetNextIdShouldBeCalled1()
        {
            var mockRepository = new Mock<IClient<Client>>();
            var sut = new ClientRepository(mockRepository.Object);
            sut.CreateClient(It.IsAny<string>(), It.IsAny<string>());
            mockRepository.Verify(x => x.GetNextID(), Times.AtLeastOnce);
        }
        [Test]
        public void TheRepositoryGetNextIdShouldBeCalled2()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            mockRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => new Invoice());
            var Client = new Client(mockRepository.Object);
            var Items = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    Name = "Produkt",
                    Price = 200,
                    Tax = 23
                }
            };
            Client.Buy(Items, 1);
            mockRepository.Verify(x => x.GetNextID(), Times.AtLeastOnce);
        }
        [Test]
        public void TheRepositoryInvoicePriceShouldBeCalled()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            mockRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => new Invoice());
            var sut = new Client(mockRepository.Object);
            sut.InvoicePrice(new Invoice());
            mockRepository.Verify(x => x.InvoicePrice(It.IsAny<Invoice>()), Times.Once);
        }
        [Test]
        public void TheRepositoryInvoicePriceShouldThrowExceptionWhenNotFind()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            mockRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => null);
            var sut = new Client(mockRepository.Object);
            Assert.That(() => sut.InvoicePrice(new Invoice()), Throws.ArgumentNullException);
        }
        [Test]
        public void TheRepositoryDeleteClientShouldNotBeCalledWhenNotFound()
        {
            var mockClient = new Mock<IClient<Client>>();
            var mockRepository = new Mock<IRepository<Invoice>>();
            mockClient.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => null);
            var sut = new ClientRepository(mockClient.Object);
            sut.DeleteClient(new Client(mockRepository.Object));
            mockClient.Verify(x => x.DeleteClient(It.IsAny<Client>()), Times.Never);
        }
        [Test]
        public void TheRepositoryDeleteClientShouldBeCalledWhenFound()
        {
            var mockClient = new Mock<IClient<Client>>();
            var mockRepository = new Mock<IRepository<Invoice>>();
            var Client = new Client(mockRepository.Object) { Firstname = "Cezary", Lastname = "Piatek" };
            var Items = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    Name = "Produkt",
                    Price = 200,
                    Tax = 23
                }
            };
            var iv = new List<Invoice>
            {
                new Invoice
                {
                    Id = 0,
                    Items = Items
                }
            };
            mockRepository.Setup(x => x.ReturnAll()).Returns(() => iv);
            mockClient.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => new Client(mockRepository.Object));
            var sut = new ClientRepository(mockClient.Object);
            sut.DeleteClient(Client);
            mockClient.Verify(x => x.Delete(It.IsAny<Client>()));
        }
        [Test]
        public void TheRepositoryDeleteClientForMultipleInvoicesDeleteShouldBeCalled()
        {
            var mockService = new Mock<IClient<Client>>();
            var mockRepository = new Mock<IRepository<Invoice>>();
            var iv = new List<Invoice>
            {
                new Invoice
                {
                    Items =  new List<InvoiceItem>
                            {
                                new InvoiceItem
                                {
                                    Name = "Produkt",
                                    Price = 200,
                                    Tax = 23
                                },
                                new InvoiceItem
                                {
                                    Name = "Produkt2",
                                    Price = 300,
                                    Tax = 19
                                },
                                new InvoiceItem
                                {
                                    Name = "Produkt3",
                                    Price = 400,
                                    Tax = 23
                                },
                                new InvoiceItem
                                {
                                    Name = "Produkt4",
                                    Price = 500,
                                    Tax = 23
                                }
                            }
                },
                new Invoice
                {
                    Items =  new List<InvoiceItem>
                            {
                                new InvoiceItem
                                {
                                    Name = "Produkt",
                                    Price = 200,
                                    Tax = 23
                                },
                                new InvoiceItem
                                {
                                    Name = "Produkt2",
                                    Price = 300,
                                    Tax = 19
                                },
                                new InvoiceItem
                                {
                                    Name = "Produkt3",
                                    Price = 400,
                                    Tax = 23
                                },
                                new InvoiceItem
                                {
                                    Name = "Produkt4",
                                    Price = 500,
                                    Tax = 23
                                }
                            }
                }
            };
            mockRepository.Setup(x => x.ReturnAll()).Returns(() => iv);
            mockRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => new Invoice());
            mockService.Setup(x => x.FindById(It.IsAny<int>())).Returns(() => new Client(mockRepository.Object));
            var sut = new ClientRepository(mockService.Object);
            sut.DeleteClient(new Client(mockRepository.Object));
            mockRepository.Verify(x => x.Delete(It.IsAny<Invoice>()), Times.Exactly(iv.Count));
        }

    }
}

