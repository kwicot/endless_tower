using UnityEngine;

namespace Utilities
{
    public class Timer : MonoBehaviour
    {
        private float _time = 0f;
        public static System.Action OnTick;
        private float tick = 1f;
        public void Update()
        {
            _time += Time.deltaTime;

            if (_time < tick) return;
        
            _time -= tick;
            OnTick?.Invoke();
        }
    }
}
