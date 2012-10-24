using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.General
{
    public enum EnterpriseTypes
    {
        Individual = 10,
        SoleTrader = 20,
        Partnership = 30,
        Company = 40,
    }

    public enum LoanTypes
    {
        Secured = 0,
        UnSecured = 1,
    }

    /// <summary>
    /// 客户贷款的状态
    /// </summary>
    public enum LoanStatuses
    {
        Deleted = -10,
        UserUnCompleted = 0,
        New = 10,
        //Checking = 20,
        Rejected = 30,
        Approved = 40,
        Blacklisted = 90,
        Collection = 100,
        Completed= 110,
    }

    public enum ScheduleStatuses
    {
        Normal = 0,
        /// <summary>
        /// 本应付记录已经被重新分期到其他新的账期中
        /// </summary>
        ReInstalled = 10,
    }

    public enum ResidentalTypes
    {
        Renting = 10,
        OwnHomeWithoutMortgage = 20,
        OwnHomeWithMortgage = 30,
        Parent = 40,
        Relative = 50,
        Boarding = 60,
    }
}
