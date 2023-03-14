using UnityEngine;

namespace EndlessDestructionOfCubes.Scripts.Infrastructure.Timers
{
    public class Countdown
    {
        private float _time;
        private float _startTime;


        public bool IsOver
            => GetCurrentTime() > _time;

        public float RemainingTime
            => _time - GetCurrentTime();

        public float GetTime
            => _time;


        public void Run(float time)
        {
            _time = time;
            _startTime = Time.time;
        }

        private float GetCurrentTime()
        {
            return Time.time - _startTime;
        }
    }
}
