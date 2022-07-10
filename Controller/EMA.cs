using System.Collections.Generic;

namespace MyCryptoApp.Controller
{
    internal class EMA
    {
        #region Property
        private readonly List<double> Input = new();
        public readonly List<double> Value = new();
        private readonly int Period;
        private int CurrentBar;
        private readonly double constant1;
        private readonly double constant2;
        #endregion

        public bool IsFinish
        {
            get
            {
                return Input.Count == Period;
            }
        }
        public EMA(int period)
        {
            Period = period;
            CurrentBar = 0;
            constant1 = 2.0 / (1 + Period);
            constant2 = 1 - (2.0 / (1 + Period));
        }
        public void Add(double value)
        {
            Input.Insert(index:0, value);
            Value.Insert(index:0, item: value);

            Value[0] = CurrentBar == 0 ? Input[0] : Input[0] * constant1 + constant2 * Value[1];

            CurrentBar++;

            if (Input.Count > Period)
            {
                Input.RemoveAt(index: Input.Count - 1);
                Value.RemoveAt(index: Value.Count - 1);
            }
        }
    }
}
