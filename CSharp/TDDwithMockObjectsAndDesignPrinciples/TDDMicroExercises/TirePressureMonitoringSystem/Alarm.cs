namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    public class Alarm
    {
        private const double LowPressureTreshold = 17;
        private const double HighPressureTreshold = 21;

        readonly Sensor _sensor;
        bool _alarmOn = false;
        private long _alarmCount = 0;

        public Alarm()
        {
            _sensor = new SensorImpl();
        }

        public Alarm(Sensor sensor)
        {
            this._sensor = sensor;
        }


        public void Check()
        {
            double psiPressureValue = _sensor.PopNextPressurePsiValue();

            if (psiPressureValue < LowPressureTreshold || HighPressureTreshold < psiPressureValue)
            {
                _alarmOn = true;
                _alarmCount += 1;
            }
        }

        public bool AlarmOn
        {
            get { return _alarmOn; }
        }

    }
}
