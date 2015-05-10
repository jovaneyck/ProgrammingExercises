using System;
using Moq;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace AutoFixture
{
    public class AutoMockingShould
    {
        [Fact]
        public void ManualCreatedSUTWithMockedCollaborator()
        {
            //how we usually do it
            var fixture = new Fixture();
            var gateway = new Mock<EmailGateway>();
            var sut = new EmailBuffer(gateway.Object);
            sut.Add(fixture.Create<EmailMessage>());

            sut.Send();

            gateway.Verify(g=>g.Send(It.IsAny<EmailMessage>()));
        }

        [Fact]
        public void CannotByDefaultUseAutoFixtureToResolveAbstractDependencies()
        {
            var fixture = new Fixture();

            Assert.Throws<ObjectCreationException>(() => fixture.Create<EmailBuffer>()); //has dependency to abstract EmailGateway type.
        }

        [Fact]
        public void TurnIntoAnAutoMockingContainer()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var sut = fixture.Create<EmailBuffer>();

        }

        [Fact]
        public void ProvideMockedCollaboratorsUsingFreeze()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            var gateway = fixture.Freeze<Mock<EmailGateway>>(); //get mock, same mock will be returned in all future requests.
            var sut = fixture.Create<EmailBuffer>();
            sut.Add(fixture.Create<EmailMessage>());

            sut.Send();

            gateway.Verify(g=>g.Send(It.IsAny<EmailMessage>()));

        }

        [Fact]
        public void AcceptAlreadyCreatedMockCollaboratorsUsingInject()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            var gateway = new Mock<EmailGateway>();
            fixture.Inject(gateway); //Inject an already created object, will be used in all future requests for that type.

            var sut = fixture.Create<EmailBuffer>();
            sut.Add(fixture.Create<EmailMessage>());

            sut.Send();

            gateway.Verify(g=>g.Send(It.IsAny<EmailMessage>()));

        }

        [Theory]
        [AutoMoqData]
        public void AutoMockWithoutAnySetupByUsingACustomAttribute(
            [Frozen] Mock<EmailGateway> gateway, //Frozen attribute freezes a type
            //Note that the ORDER OF ARGUMENTS MATTERS HERE. if not frozen before a buffer object is created, gateway is not frozen yet!!!
            EmailBuffer buffer, 
            EmailMessage message
            )
        {
            buffer.Add(message);
            buffer.Send();

            gateway.Verify(g=>g.Send(It.IsAny<EmailMessage>()));
        }
    }

    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Fixture().Customize(new AutoMoqCustomization())) //Boom!
        {
        }
    }

    public class EmailBuffer
    {
        private readonly List<EmailMessage> _bufferedMessages = new List<EmailMessage>();
        private readonly EmailGateway _gateway;

        public EmailBuffer(EmailGateway gateway)
        {
            _gateway = gateway;
        }

        public void Add(EmailMessage message)
        {
            _bufferedMessages.Add(message);
        }

        public void Send()
        {
            foreach(var msg in _bufferedMessages)
            {
                _gateway.Send(msg);
            }

            _bufferedMessages.Clear();
        }
    }

    public interface EmailGateway
    {
        void Send(EmailMessage msg);
    }

    public class EmailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}