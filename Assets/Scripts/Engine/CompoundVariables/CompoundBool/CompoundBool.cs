using System.Collections.Generic;

namespace Game.Util
{
    internal abstract class CompoundBool
    {
        protected List<CompoundBoolValue> _boolValues;

        public bool Value { get; protected set; }

        public CompoundBool()
        {
            _boolValues = new List<CompoundBoolValue>();
            RecalculateValue();
        }

        public void AddValue(CompoundBoolValue compoundBool)
        {
            _boolValues.Add(compoundBool);
            compoundBool.ValueChanged += RecalculateValue;
            RecalculateValue();
        }

        public void RemoveValue(CompoundBoolValue compoundBool)
        {
            _boolValues.Remove(compoundBool);
            compoundBool.ValueChanged -= RecalculateValue;
            RecalculateValue();
        }

        protected abstract void RecalculateValue();
    }
}