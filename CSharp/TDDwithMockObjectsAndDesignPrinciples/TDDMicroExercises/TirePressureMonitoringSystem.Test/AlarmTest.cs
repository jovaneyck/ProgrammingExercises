using NUnit.Framework;
using Rhino.Mocks;

namespace TDDMicroExercises.TirePressureMonitoringSystem.Test
{
    [TestFixture]
    class AlarmTest
    {
        private Sensor _sensorImpl;
        private Alarm alarm;

        [SetUp]
        public void SetUp()
        {
            _sensorImpl = MockRepository.GenerateMock<Sensor>();
            alarm = new Alarm(_sensorImpl);
        }

        [Test]
        public void IsOffByDefault()
        {
            Assert.IsFalse(alarm.AlarmOn);
        }

        [Test]
        public void TriggersWhenBelowTreshold()
        {
            _sensorImpl.Stub(s => s.PopNextPressurePsiValue()).Return(1d);

            alarm.Check();
            Assert.IsTrue(alarm.AlarmOn);
        }
    }
}
