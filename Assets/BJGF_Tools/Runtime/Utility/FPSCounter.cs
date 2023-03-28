using UnityEngine;

namespace BJGF.Tools.Utility
{
    public class FPSCounter
    {       

        public void Update()
        {
            if (_isStart == false)
            {
                _isStart = true;
                _lastInterval = Time.realtimeSinceStartup;
            }

            ++_frames;
            float timeNow = Time.realtimeSinceStartup;
            if (timeNow > _lastInterval + UPDATEINTERVAL)
            {
                _fps = _frames / (timeNow - _lastInterval);
                _ms = 1000.0f / Mathf.Max(_fps, 0.00001f);
                _frames = 0;
                _lastInterval = timeNow;
            }
        }

        public int GetFPS()
        {
            return Mathf.CeilToInt(_fps);
        }

        public float GetMS()
        {
            return _ms;
        }

        private float _ms;
        private float _fps;
        private int _frames;
        private float _lastInterval;
        private bool _isStart = false;

        /// <summary>
        /// 更新频率
        /// </summary>
        private const float UPDATEINTERVAL = 0.2f;
    }
}
