namespace HiLand.Utility.Drawing
{
    public enum ThumbnailCutModes
    {
        /// <summary>
        /// 指定高宽缩放（可能变形）  
        /// </summary>
        HW,

        /// <summary>
        /// 指定宽，高按比例 
        /// </summary>
        W,

        /// <summary>
        /// 指定高，宽按比例
        /// </summary>
        H,

        /// <summary>
        /// 自适应（根据缩略图的高宽比与原图的高宽比匹配最合适的情形，不变形）   
        /// </summary>
        Auto,
    }
}