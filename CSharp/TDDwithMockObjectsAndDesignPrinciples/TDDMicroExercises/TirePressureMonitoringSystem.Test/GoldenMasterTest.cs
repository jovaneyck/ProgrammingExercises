using System;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace TDDMicroExercises.TirePressureMonitoringSystem.Test
{
    [TestFixture]
    public class GoldenMasterTest
    {
        [Test]
        public void NUnitSanityCheck()
        {
            Assert.IsTrue(true);
        }

        [Test]
        [UseReporter(typeof(NUnitReporter))]
        public void AlarmUsedInInitialScenario()
        {
            //Note that this is a VERY high-level regression test: 
            // the dummy sensor always returns the same value, so the alarm will always be on after the first pressure check.
            StringBuilder s = new StringBuilder();
            for(int i = 0; i < 100; i++)
            {
                Alarm a = new Alarm();
                for(int j = 0; j < 10; j++)
                {
                    s.AppendLine(String.Format("Run {0} {1}: {2}", i,j, a.AlarmOn));
                    a.Check();
                }
            }

            Approvals.Verify(s);
        }

        [Test]
        [UseReporter(typeof(NUnitReporter))]
        public void AlarmUsedInRealisticScenario()
        {
            StringBuilder s = new StringBuilder();

            const double initialValue = 17d;
            Sensor sensor = new SensorStub(initialValue);
            Alarm a = new Alarm(sensor);

            for (double psiValue = initialValue; psiValue < 25; psiValue++)
            {
                s.AppendLine(String.Format("Psi {0}: {1}", psiValue, a.AlarmOn));
                a.Check();
            }

            Approvals.Verify(s);
        }
    }

    public class SensorStub : Sensor
    {
        private double _nextValue;

        public SensorStub(double initialValue)
        {
            _nextValue = initialValue;
        }

        public double PopNextPressurePsiValue()
        {
            return _nextValue++;
        }
    }
}
