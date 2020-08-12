using System.Collections.Generic;

namespace Game.Util
{
    internal class CompoundInt
    {
        private List<CompoundIntAdd> _addValues;
        private List<CompoundIntMult> _multValues;

        private CompoundIntAdd _baseValue;
        public int BaseValue
        {
            get { return _baseValue.Value; }
            set
            {
                _baseValue.Value = value;
                RecalculateValue();
            }
        }

        public int Value { get; private set; }

        public CompoundInt(int baseValue)
        {
            _addValues = new List<CompoundIntAdd>();
            _multValues = new List<CompoundIntMult>();

            _baseValue = new CompoundIntAdd(baseValue);
            _addValues.Add(_baseValue);
            RecalculateValue();
        }

        public void AddValue(CompoundIntAdd compoundIntAdd)
        {
            _addValues.Add(compoundIntAdd);
            compoundIntAdd.ValueChanged += RecalculateValue;
            RecalculateValue();
        }

        public void AddValue(CompoundIntMult compoundIntMult)
        {
            _multValues.Add(compoundIntMult);
            compoundIntMult.ValueChanged += RecalculateValue;
            RecalculateValue();
        }

        public void RemoveValue(CompoundIntAdd compoundIntAdd)
        {
            _addValues.Remove(compoundIntAdd);
            compoundIntAdd.ValueChanged -= RecalculateValue;
            RecalculateValue();
        }

        public void RemoveValue(CompoundIntMult compoundIntMult)
        {
            _multValues.Remove(compoundIntMult);
            compoundIntMult.ValueChanged -= RecalculateValue;
            RecalculateValue();
        }

        private void RecalculateValue()
        {
            Value = 0;

            foreach (CompoundIntAdd addValue in _addValues)
            {
                Value += addValue.Value;
            }

            foreach (CompoundIntMult multValue in _multValues)
            {
                Value *= multValue.Value;
            }
        }
    }
}