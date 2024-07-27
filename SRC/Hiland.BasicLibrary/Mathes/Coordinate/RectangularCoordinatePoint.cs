using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Mathes.Coordinate
{
    /// <summary>
    /// 直接坐标点
    /// </summary>
    public class RectangularCoordinatePoint
    {
        /// <summary>
        /// X坐标值
        /// </summary>
        public double X { get; set; }
        
        /// <summary>
        /// Y坐标值
        /// </summary>
        public double Y { get; set; }
        
        /// <summary>
        /// Z坐标值
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// 坐标原点
        /// </summary>
        public static RectangularCoordinatePoint Zero
        {
            get
            {
                return new RectangularCoordinatePoint(0, 0, 0);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public RectangularCoordinatePoint(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// 复制坐标值到目标点
        /// </summary>
        /// <param name="target"></param>
        public void CopyTo(RectangularCoordinatePoint target)
        {
            target.X = this.X;
            target.Y = this.Y;
            target.Z = this.Z;
        }

        /// <summary>
        /// 从源点复制坐标值
        /// </summary>
        /// <param name="source"></param>
        public void CopyFrom(RectangularCoordinatePoint source)
        {
            this.X = source.X;
            this.Y = source.Y;
            this.Z = source.Z;
        }

        /// <summary>
        /// 重置坐标值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Reset(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
