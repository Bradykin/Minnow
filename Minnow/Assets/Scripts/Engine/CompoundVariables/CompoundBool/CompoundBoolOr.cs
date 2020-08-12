namespace Game.Util
{
    internal class CompoundBoolOr : CompoundBool
    {
        public CompoundBoolOr() : base()
        {
        }

        protected override void RecalculateValue()
        {
            if (_boolValues.Count == 0)
            {
                Value = true;
                return;
            }

            foreach (CompoundBoolValue boolValue in _boolValues)
            {
                if (boolValue.Value == true)
                {
                    Value = true;
                    return;
                }
            }
            Value = false;
        }
    }
}