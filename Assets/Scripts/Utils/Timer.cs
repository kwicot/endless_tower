using UnityEngine;

namespace Utilities
{
    public class Timer : MonoBehaviour
    {
        private float _time = 0f;
        public static System.Action OnTick;
        public void Update()
        {
            _time += Time.deltaTime;

            if (_time < 1.0f) return;
        
            _time -= 1.0f;
            OnTick?.Invoke();
        }
    }
}
