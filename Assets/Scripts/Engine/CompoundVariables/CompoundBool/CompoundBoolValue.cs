using System;

namespace Game.Util
{
    internal class CompoundBoolValue
    {
        public event Action ValueChanged;

        private bool _value;
        public bool Value
        {
            get { return _value; }
            set
            {
                _value = value;
                ValueChanged?.Invoke();
            }
        }

        public CompoundBoolValue(bool value)
        {
            _value = value;
        }
    }
}
