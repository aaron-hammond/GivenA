using System;
using System.Linq.Expressions;
using Moq;
using Moq.Language.Flow;
using NUnit.Framework;
using StructureMap.AutoMocking.Moq;

namespace Testing.Tools
{
    public class Then : TestAttribute
    {
    }

    [TestFixture]
    public abstract class TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Given();
            When();
        }

        protected virtual void Given()
        {
        }

        protected virtual void When()
        {
        }
    }

    public abstract class TestBase<T> : TestBase where T : class
    {
        public T Target;
    }

    public class GivenA<T> : TestBase<T> where T : class
    {
        private readonly MoqAutoMocker<T> _autoMocker = new MoqAutoMocker<T>();

        protected override void Given()
        {
            Target = _autoMocker.ClassUnderTest;
            base.Given();
        }

        protected Mock<TT> GetMock<TT>() where TT : class
        {
            return Mock.Get(_autoMocker.Get<TT>());
        }

        protected ISetup<TT, TResult> SetupProperty<TT, TResult>(Expression<Func<TT, TResult>> setup) where TT : class
        {
            return Mock.Get(_autoMocker.Get<TT>()).Setup(setup);
        }

        protected void Verify<TT>(Expression<Action<TT>> verify) where TT : class
        {
            Mock.Get(_autoMocker.Get<TT>()).Verify(verify);
        }

        protected void VerifyGet<TT, TResult>(Expression<Func<TT, TResult>> verifyGet) where TT : class
        {
            Mock.Get(_autoMocker.Get<TT>()).VerifyGet(verifyGet);
        }

        protected void VerifySet<TT>(Action<TT> verifySet) where TT : class
        {
            Mock.Get(_autoMocker.Get<TT>()).VerifySet(verifySet);
        }

        protected void Verify<TT>(Expression<Action<TT>> verify, Times times) where TT : class
        {
            Mock.Get(_autoMocker.Get<TT>()).Verify(verify, times);
        }
    }

    public class GivenA<T, TResult> : GivenA<T> where T : class
    {
        public TResult Result;
    }
}