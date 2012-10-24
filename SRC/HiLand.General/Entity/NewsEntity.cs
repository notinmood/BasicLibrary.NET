using System;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;

namespace HiLand.General.Entity
{
    public class NewsEntity : BaseModel<NewsEntity>
    {
        public override string[] BusinessKeyNames
        {
            get { return new string[] { "NewsGuid" }; }
        }

        #region 实体信息
        private int newsID;
        public int NewsID
        {
            get { return newsID; }
            set { newsID = value; }
        }

        private Guid newsGuid = Guid.Empty;
         [DBFieldAttribute(IsBusinessPrimaryKey = true)]
        public Guid NewsGuid
        {
            get { return newsGuid; }
            set { newsGuid = value; }
        }

        private string newsCategoryCode = String.Empty;
        public string NewsCategoryCode
        {
            get { return newsCategoryCode; }
            set { newsCategoryCode = value; }
        }

        private string newsCategoryName = String.Empty;
        public string NewsCategoryName
        {
            get { return newsCategoryName; }
            internal set { newsCategoryName = value; }
        }

        private string newsTitle = String.Empty;
        public string NewsTitle
        {
            get { return newsTitle; }
            set { newsTitle = value; }
        }

        private string newsBody = String.Empty;
        public string NewsBody
        {
            get { return newsBody; }
            set { newsBody = value; }
        }

        private string newsSEOUrl = String.Empty;
        public string NewsSEOUrl
        {
            get { return newsSEOUrl; }
            set { newsSEOUrl = value; }
        }

        private DateTime newsDate = DateTimeHelper.Min;
        public DateTime NewsDate
        {
            get { return newsDate; }
            set { newsDate = value; }
        }

        private string newsAuthor = String.Empty;
        public string NewsAuthor
        {
            get { return newsAuthor; }
            set { newsAuthor = value; }
        }

        private int newsReadCount;
        public int NewsReadCount
        {
            get { return newsReadCount; }
            set { newsReadCount = value; }
        }

        private decimal newsRating;
        public decimal NewsRating
        {
            get { return newsRating; }
            set { newsRating = value; }
        }

        private Logics canUsable = Logics.True;
        public Logics CanUsable
        {
            get { return canUsable; }
            set { canUsable = value; }
        }

        private int newsIsTop;
        public int NewsIsTop
        {
            get { return newsIsTop; }
            set { newsIsTop = value; }
        }

        private int newsIsRecommend;
        public int NewsIsRecommend
        {
            get { return newsIsRecommend; }
            set { newsIsRecommend = value; }
        }

        private int newsPlusCount;
        public int NewsPlusCount
        {
            get { return newsPlusCount; }
            set { newsPlusCount = value; }
        }

        private int newsMinusCount;
        public int NewsMinusCount
        {
            get { return newsMinusCount; }
            set { newsMinusCount = value; }
        }

        private string newsOriginalFrom = String.Empty;
        public string NewsOriginalFrom
        {
            get { return newsOriginalFrom; }
            set { newsOriginalFrom = value; }
        }

        private string newsOriginalUrl = String.Empty;
        public string NewsOriginalUrl
        {
            get { return newsOriginalUrl; }
            set { newsOriginalUrl = value; }
        }

        private string newsOriginalAuthor = String.Empty;
        public string NewsOriginalAuthor
        {
            get { return newsOriginalAuthor; }
            set { newsOriginalAuthor = value; }
        }

        private DateTime newsOriginalDate = DateTimeHelper.Min;
        public DateTime NewsOriginalDate
        {
            get { return newsOriginalDate; }
            set { newsOriginalDate = value; }
        }
        #endregion
    }
}
