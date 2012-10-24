namespace HiLand.Utility.Finance
{
    public class SimpleRateConverter:RateConverter
    {
        /// <summary>
        /// 按照一般算数方法进行的利率转换
        /// </summary>
        /// <param name="rate">利率值</param>
        /// <param name="circle">利率周期</param>
        public SimpleRateConverter(double rate, PaymentTermTypes circle)
        {
            switch (circle)
            {
                case PaymentTermTypes.Daily:
                    this.DailyRate = rate;
                    this.AnnualRate = rate * 365;
                    break;
                case PaymentTermTypes.Weekly:
                    this.WeeklyRate = rate;
                    this.AnnualRate = rate * 52;
                    break;
                case PaymentTermTypes.DoubleWeekly:
                    this.DoubleWeeklyRate = rate;
                    this.AnnualRate = rate * 26;
                    break;
                case PaymentTermTypes.Monthly:
                    this.MonthlyRate = rate;
                    this.AnnualRate = rate * 12;
                    break;
                case PaymentTermTypes.DoubleMonthly:
                    this.DoubleMonthlyRate = rate;
                    this.AnnualRate = rate * 6;
                    break;
                case PaymentTermTypes.Quarterly:
                    this.QuarterlyRate = rate;
                    this.AnnualRate = rate * 4;
                    break;
                case PaymentTermTypes.SemiYearly:
                    this.SemiYearlyRate = rate;
                    this.AnnualRate = rate * 2;
                    break;
                case PaymentTermTypes.Annual:
                default:
                    this.AnnualRate = rate;
                    break;
            }
        }

        public override double DailyRate
        {
            get
            {
                if (dailyRate == -1)
                {
                    dailyRate = AnnualRate / 365;
                }
                return dailyRate;
            }
            set { dailyRate = value; }
        }


       public override double WeeklyRate
        {
            get
            {
                if (weeklyRate == -1)
                {
                    weeklyRate = AnnualRate / 52;
                }
                return weeklyRate;
            }
            set { weeklyRate = value; }
        }

       public override double DoubleWeeklyRate
        {
            get
            {
                if (doubleWeeklyRate == -1)
                {
                    doubleWeeklyRate = AnnualRate / 26;
                }
                return doubleWeeklyRate;
            }
            set { doubleWeeklyRate = value; }
        }

       public override double MonthlyRate
        {
            get
            {
                if (monthlyRate == -1)
                {
                    monthlyRate = AnnualRate / 12;
                }
                return monthlyRate;
            }
            set { monthlyRate = value; }
        }


       public override double DoubleMonthlyRate
        {
            get
            {
                if (doubleMonthlyRate == -1)
                {
                    doubleMonthlyRate = AnnualRate / 6;
                }
                return doubleMonthlyRate;
            }
            set { doubleMonthlyRate = value; }
        }


       public override double QuarterlyRate
        {
            get
            {
                if (quarterlyRate == -1)
                {
                    quarterlyRate = AnnualRate / 4;
                }
                return quarterlyRate;
            }
            set { quarterlyRate = value; }
        }


       public override double SemiYearlyRate
        {
            get
            {
                if (semiYearlyRate == -1)
                {
                    semiYearlyRate = AnnualRate / 2;
                }
                return semiYearlyRate;
            }
            set { semiYearlyRate = value; }
        }


       public override double AnnualRate
        {
            get { return annualRate; }
            set { annualRate = value; }
        }
    }
}
