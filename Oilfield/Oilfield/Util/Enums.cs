using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public enum State : int
    {
        LOW = 0,
        MEDIUM = 1,
        HIGH = 2
    }

    public enum Reward
    {
        VERYBAD = -2,
        BAD = -1,
        NORMAL = 0,
        GOOD = 1,
        VERYGOOD = 2
    }

    public struct WorldState
    {
        private int money;

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        private int extCount;

        public int ExtCount
        {
            get { return extCount; }
            set { extCount = value; }
        }

        private int income;

        public int Income
        {
            get { return income; }
            set { income = value; }
        }

        public WorldState(int money, int extCount, int income)
        {
            this.money = money;
            this.income = income;
            this.extCount = extCount;
        }

        public override bool Equals(object obj)
        {
            return this.ToString() == obj.ToString();
        }

        public override string ToString()
        {
            return money.ToString() + income.ToString() + extCount.ToString();
        }

        public override int GetHashCode()
        {
            return money * 100 + income * 10 + extCount;
        }

    }

    
}
