using NUnit.Framework;
using Rhino.Mocks;

namespace TDDMicroExercises.TirePressureMonitoringSystem.Test
{
    [TestFixture]
    class AlarmTest
    {
        private Sensor _sensorImpl;
        private Alarm alarm;
        private const double _underLowerTreshold = 1d;
        private const double _betweenTresholds = 20d;
        private const double _aboveUpperTreshold = 22d;

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
            AssertAlertTriggersFor(_underLowerTreshold);
        }

        private void AssertAlertTriggersFor(double sensorValue)
        {
            SetupSensorAndCheckAlarm(sensorValue);
            Assert.IsTrue(alarm.AlarmOn);
        }

        private void SetupSensorAndCheckAlarm(double sensorValue)
        {
            _sensorImpl.Stub(s => s.PopNextPressurePsiValue()).Return(sensorValue);
            alarm.Check();
        }

        [Test]
        public void TriggersWhenAboveTreshold()
        {
            AssertAlertTriggersFor(_aboveUpperTreshold);
        }

        [Test]
        public void DoesNotTriggerBetweenBoundaries()
        {
            AssertAlertDoesNotTriggerFor(_betweenTresholds);
        }

        private void AssertAlertDoesNotTriggerFor(double sensorValue)
        {
            SetupSensorAndCheckAlarm(sensorValue);
            Assert.IsFalse(alarm.AlarmOn);
        }

        [Test]
        public void RemembersWhenTresholdWasBreached()
        {
            SetupSensorAndCheckAlarm(_underLowerTreshold);
            Assert.IsTrue(alarm.AlarmOn);

            SetupSensorAndCheckAlarm(_betweenTresholds);
            Assert.IsTrue(alarm.AlarmOn);
        }
    }
}
