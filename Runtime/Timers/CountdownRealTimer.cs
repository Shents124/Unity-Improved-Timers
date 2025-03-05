using System;
using UnityEngine;

namespace ImprovedTimers
{
    /// <summary>
    /// Timer that counts down from a specific value to zero in realtime.
    /// </summary>
    /// 
    public class CountdownRealTimer : Timer
    {
        public Action OnTick = delegate { };

        private const float DELTA = 0.5F;
        private float _currentDelta;
        
        public TimeSpan CurrentTimeSpan => TimeSpan.FromSeconds(CurrentTime);
        
        public CountdownRealTimer(double value) : base(value)
        {
        }

        public CountdownRealTimer(TimeSpan value) : base(value.TotalSeconds)
        {
            CurrentTime = value.TotalSeconds;
        }
        
        public override void Tick()
        {
            if (IsRunning && CurrentTime > 0) 
            {
                CurrentTime -= Time.unscaledDeltaTime;
                _currentDelta -= Time.unscaledDeltaTime;
            }
            
            if (IsRunning && CurrentTime <= 0) {
                Stop();
            }

            if (_currentDelta <= 0)
            {
                _currentDelta = DELTA;
                OnTick?.Invoke();
            }
        }
        
        public override bool IsFinished { get; }

        public void Reset(TimeSpan timeSpan)
        {
            CurrentTime = timeSpan.TotalSeconds;
        }
    }
}

