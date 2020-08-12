namespace Game.Util
{
    internal class CompoundBoolAnd : CompoundBool
    {
        public CompoundBoolAnd() : base()
        {
        }

        protected override void RecalculateValue()
        {
            if (_boolValues.Count == 0)
            {
                Value = false;
                return;
            }

            foreach (CompoundBoolValue boolValue in _boolValues)
            {
                if (boolValue.Value == false)
                {
                    Value = false;
                    return;
                }
            }
            Value = true;
        }
    }
}