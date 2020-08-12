using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Util
{
    internal class CompoundFloat
    {
        private List<CompoundFloatAdd> _addValues;
        private List<CompoundFloatMult> _multValues;

        private CompoundFloatAdd _baseValue;
        public float BaseValue
        {
            get { return _baseValue.Value; }
            set
            {
                _baseValue.Value = value;
                RecalculateValue();
            }
        }

        public float Value { get; private set; }

        public CompoundFloat(float baseValue = 0)
        {
            _addValues = new List<CompoundFloatAdd>();
            _multValues = new List<CompoundFloatMult>();

            _baseValue = new CompoundFloatAdd(baseValue);
            _addValues.Add(_baseValue);
            RecalculateValue();
        }

        public void AddValue(CompoundFloatAdd compoundFloatAdd)
        {
            _addValues.Add(compoundFloatAdd);
            compoundFloatAdd.ValueChanged += RecalculateValue;
            RecalculateValue();
        }

        public void AddValue(CompoundFloatMult compoundFloatMult)
        {
            _multValues.Add(compoundFloatMult);
            compoundFloatMult.ValueChanged += RecalculateValue;
            RecalculateValue();
        }

        public void RemoveValue(CompoundFloatAdd compoundFloatAdd)
        {
            _addValues.Remove(compoundFloatAdd);
            compoundFloatAdd.ValueChanged -= RecalculateValue;
            RecalculateValue();
        }

        public void RemoveValue(CompoundFloatMult compoundFloatMult)
        {
            _multValues.Remove(compoundFloatMult);
            compoundFloatMult.ValueChanged -= RecalculateValue;
            RecalculateValue();
        }

        private void RecalculateValue()
        {
            Value = 0;

            foreach (CompoundFloatAdd addValue in _addValues)
            {
                Value += addValue.Value;
            }

            foreach (CompoundFloatMult multValue in _multValues)
            {
                Value *= multValue.Value;
            }
        }
    }
}