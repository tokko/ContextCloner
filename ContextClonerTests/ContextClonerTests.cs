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
            Context.Datas1.AddRange(Enumerable.Range(0, 10).Select(x => new Data1 {Datas = Context.Datas.ToList()}));
            Context.SaveChanges();

            var ctx = new Context(Effort.DbConnectionFactory.CreateTransient());
            Context.CloneContextTo(ctx);
            Assert.AreEqual(Context.Datas.Count(), ctx.Datas.Count());
            Assert.AreEqual(Context.Datas1.Count(), ctx.Datas1.Count());
        }
    }
}
