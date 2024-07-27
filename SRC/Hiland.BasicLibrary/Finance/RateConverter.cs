namespace Hiland.BasicLibrary.Finance
{
    /// <summary>
    /// 利率转换器
    /// </summary>
    public abstract class RateConverter
    {
        protected double dailyRate = -1;
        public virtual double DailyRate { get; set; }

        protected double weeklyRate = -1;
        public virtual double WeeklyRate { get; set; }

        protected double doubleWeeklyRate = -1;
        public virtual double DoubleWeeklyRate { get; set; }

        protected double monthlyRate = -1;
        public virtual double MonthlyRate { get; set; }

        protected double doubleMonthlyRate = -1;
        public virtual double DoubleMonthlyRate { get; set; }

        protected double quarterlyRate = -1;
        public virtual double QuarterlyRate { get; set; }

        protected double semiYearlyRate = -1;
        public virtual double SemiYearlyRate { get; set; }

        protected double annualRate = -1;
        public virtual double AnnualRate { get; set; }

        /// <summary>
        /// 根据周期类型获取利率
        /// </summary>
        /// <param name="loanTermType"></param>
        /// <returns></returns>
        public double GetRate(PaymentTermTypes loanTermType)
        {
            double rate = this.MonthlyRate;
            switch (loanTermType)
            {
                case PaymentTermTypes.Daily:
                    rate = this.DailyRate;
                    break;
                case PaymentTermTypes.Weekly:
                    rate = this.WeeklyRate;
                    break;
                case PaymentTermTypes.DoubleWeekly:
                    rate = this.DoubleWeeklyRate;
                    break;
                case PaymentTermTypes.Monthly:
                    rate = this.MonthlyRate;
                    break;
                case PaymentTermTypes.DoubleMonthly:
                    rate = this.DoubleMonthlyRate;
                    break;
                case PaymentTermTypes.Quarterly:
                    rate = this.QuarterlyRate;
                    break;
                case PaymentTermTypes.SemiYearly:
                    rate = this.SemiYearlyRate;
                    break;
                case PaymentTermTypes.Annual:
                    rate = this.AnnualRate;
                    break;
                default:
                    break;
            }

            return rate;
        }
    }
}
