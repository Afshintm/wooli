using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Glossary.BusinessServices;
using Glossary.BusinessServices.DataAccess;
using Glossary.BusinessServices.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Glossary.UnitTests
{
    [TestFixture]
    public class GlossaryServiceUnitTests
    {
        private DbContextOptions<GlossaryDbContext> Options { get; set; }
        private GlossaryItem Fake1 { get; set; }
        private GlossaryItem Fake2 { get; set; }
        private GlossaryItem Fake3 { get; set; }

        [SetUp]
        public void Setup()
        {
            Options = new DbContextOptionsBuilder<GlossaryDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;
            InitFakes();
            // Run the test against one instance of the context
        }

        [TearDown]
        public void TearDown()
        {
            Options = null;
        }

        private void InitFakes()
        {
            Fake1 = new GlossaryItem{Id = Guid.NewGuid(),Term = "Fake1", Definition = "A Fake glossary Item Number 1"};
            Fake2 = new GlossaryItem{Id = Guid.NewGuid(),Term = "Fake2", Definition = "A Fake glossary Item Number 2"};
            Fake3 = new GlossaryItem{Id = Guid.NewGuid(),Term = "Fake3", Definition = "A Fake glossary Item Number 3"};
        }

        [Test]
        public void GlossaryServiceShouldAddNewItemAndSearch()
        {
            using (var context = new GlossaryDbContext(Options))
            {
                var service = new GlossaryService(context);
                 var t = Task.Run(async ()=> await service.Create(Fake1)).Result;
            }

            GlossaryItem Inserted;
            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new GlossaryDbContext(Options))
            {
                var service = new GlossaryService(context);
                 Inserted= Task.Run(async () => await service.Get(Fake1.Id)).Result;
                Assert.AreEqual(Fake1.Id, Inserted.Id);
            }
            
        }

        [Test]
        public void GlosssaryServicesShouldUpdateAndDeleteanItem()
        {
            using (var context = new GlossaryDbContext(Options))
            {
                var service = new GlossaryService(context);
                var t = Task.Run(async () => await service.Create(Fake1)).Result;
            }

            GlossaryItem Updated;
            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new GlossaryDbContext(Options))
            {
                var service = new GlossaryService(context);
                var i  = Task.Run(async () => await service.Update(new GlossaryItem{Id = Fake1.Id,Term = "Updated",Definition = "Updated"})).Result;
                Updated = Task.Run(async () => await service.Get(Fake1.Id)).Result;
                Assert.AreEqual(Updated.Id, Fake1.Id);
                Assert.AreEqual(Updated.Term, "Updated");
                Assert.AreEqual(Updated.Definition, "Updated");
            }
        }
        [Test]
        public void GlosssaryServicesShouldReturnTheListOfItems()
        {
            var options = new DbContextOptionsBuilder<GlossaryDbContext>()
                .UseInMemoryDatabase(databaseName: "List_database")
                .Options;

            using (var context = new GlossaryDbContext(options))
            {
                var service1 = new GlossaryService(context);
                var t1 = Task.Run(async () => await service1.Create(Fake1)).Result;
                var t2 = Task.Run(async () => await service1.Create(Fake2)).Result;
                var t3 = Task.Run(async () => await service1.Create(Fake3)).Result;
            }

            var items = new List<GlossaryItem>();
            // Use a separate instance of the context to verify correct data was saved to database
            using (var context1 = new GlossaryDbContext(options))
            {
                var service2 = new GlossaryService(context1);
                items = Task.Run(async () => await service2.Get("")).Result;
                Assert.AreEqual(items.Count, 3);
            }
            
        }
    }
}
