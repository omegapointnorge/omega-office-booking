using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.AutoMock;

namespace TestServer.Unit
{
    public abstract class TestServiceBase<T> where T : class
    {
        protected T Sut { get; set; }
        protected AutoMocker Mocker { get; }

        protected TestServiceBase()
        {
            Mocker = new AutoMocker();

            // creating instance this way will inject
            // a default mocked instances of all the dependencies
            // which can be retrieved by calling Mocker.GetMock<>
            // e.g. Mocker.GetMock<IBookingService>() will return the injected instance for IBookingService
            Sut = Mocker.CreateInstance<T>();
        }
    }
}
