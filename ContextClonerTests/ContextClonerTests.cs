using System.Linq;
using ClientCode;
using ContextCloner;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContextClonerTests
{
    [TestClass]
    public class ContextClonerTests
    {
        public Context Context { get; set; }

        public void Setup()
        {
            
           
        }

        [TestMethod]
        public void TestCloneContextTo()
        {
            Context = new Context(Effort.DbConnectionFactory.CreateTransient());
            Context.Datas.AddRange(Enumerable.Range(0, 10).Select(x => new Data { Info = x.ToString() }));
            Context.SaveChanges();

            var ctx = new Context(Effort.DbConnectionFactory.CreateTransient());
            Context.CloneContextTo(ctx);
            Assert.AreEqual(Context.Datas.Count(), ctx.Datas.Count());
        }
    }
}
