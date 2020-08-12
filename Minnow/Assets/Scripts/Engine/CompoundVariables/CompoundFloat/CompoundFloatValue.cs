using System;

namespace Game.Util
{
    internal class CompoundFloatValue
    {
        public event Action ValueChanged;

        private float _value;
        public float Value
        {
            get { return _value; }
            set
            {
                _value = value;
                ValueChanged?.Invoke();
            }
        }

        public CompoundFloatValue(float value)
        {
            _value = value;
        }
    }
}
