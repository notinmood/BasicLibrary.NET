using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Data;

namespace HiLand.General.Entity
{
    public class ResidentalEntity : BaseModel<ResidentalEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "ResidentialGuid" }; }
        }

        #region 实体信息
        private int residentialID;
        public int ResidentialID
        {
            get { return residentialID; }
            set { residentialID = value; }
        }

        private Guid residentialGuid = Guid.Empty;
         [DBFieldAttribute(IsBusinessPrimaryKey = true)]
        public Guid ResidentialGuid
        {
            get { return residentialGuid; }
            set { residentialGuid = value; }
        }

        private Guid residentalUserGuid = Guid.Empty;
        public Guid ResidentalUserGuid
        {
            get { return residentalUserGuid; }
            set { residentalUserGuid = value; }
        }

        private ResidentalTypes residentialStatus;
        public ResidentalTypes ResidentialStatus
        {
            get { return residentialStatus; }
            set { residentialStatus = value; }
        }

        private int residentalNo;
        public int ResidentalNo
        {
            get { return residentalNo; }
            set { residentalNo = value; }
        }

        private int isPrimary;
        public int IsPrimary
        {
            get { return isPrimary; }
            set { isPrimary = value; }
        }

        private string state = String.Empty;
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        private string city = String.Empty;
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        private string suburb = String.Empty;
        public string Suburb
        {
            get { return suburb; }
            set { suburb = value; }
        }

        private string street = String.Empty;
        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        private string apartmentNo = String.Empty;
        public string ApartmentNo
        {
            get { return apartmentNo; }
            set { apartmentNo = value; }
        }

        private string postCode = String.Empty;
        public string PostCode
        {
            get { return postCode; }
            set { postCode = value; }
        }

        private string contactPerson = String.Empty;
        public string ContactPerson
        {
            get { return contactPerson; }
            set { contactPerson = value; }
        }

        private string telephone = String.Empty;
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        private string fax = String.Empty;
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        private string mobile = String.Empty;
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }

        private int residentalYears;
        public int ResidentalYears
        {
            get { return residentalYears; }
            set { residentalYears = value; }
        }

        private int residentalMonths;
        public int ResidentalMonths
        {
            get { return residentalMonths; }
            set { residentalMonths = value; }
        }

        private DateTime residentalBeginTime= DateTimeHelper.Min;
        public DateTime ResidentalBeginTime
        {
            get { return residentalBeginTime; }
            set { residentalBeginTime = value; }
        }

        private DateTime residentaEndTime = DateTimeHelper.Min;
        public DateTime ResidentaEndTime
        {
            get { return residentaEndTime; }
            set { residentaEndTime = value; }
        }

        private DateTime createDate = DateTimeHelper.Min;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        #endregion
    }
}
