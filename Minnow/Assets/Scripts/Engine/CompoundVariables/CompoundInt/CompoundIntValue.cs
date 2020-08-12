using System;

namespace Game.Util
{
    internal class CompoundIntValue
    {
        public event Action ValueChanged;

        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                ValueChanged?.Invoke();
            }
        }

        public CompoundIntValue(int value)
        {
            _value = value;
        }
    }
}
