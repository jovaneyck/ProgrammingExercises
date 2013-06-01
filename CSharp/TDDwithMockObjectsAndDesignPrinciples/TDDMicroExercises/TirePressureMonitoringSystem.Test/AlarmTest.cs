using Moq;
using NUnit.Framework;

namespace TDDMicroExercises.TirePressureMonitoringSystem.Test
{
    [TestFixture]
    class AlarmTest
    {
        [Test]
        public void DependsOnSensor()
        {
            var sensor = new Mock<Sensor>();
            Alarm a = new Alarm(sensor.Object);
        }
    }
}
