using System;
using System.Collections.Generic;
using System.Text;

namespace Hiland.BasicLibrary.Mathes.Coordinate
{
    /// <summary>
    /// 球坐标点
    /// </summary>
    public class SphericalCoordinatePoint
    {
        /// <summary>
        ///  是从原点到 P 点的距离
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// 是从原点到 P 点的连线与正 z-轴的夹角
        /// </summary>
        public double Theta { get; set; }

        /// <summary>
        /// 从原点到 P 点的连线在 xy-平面的投影线，与正 x-轴的夹角
        /// </summary>
        public double Phi { get; set; }

        /// <summary>
        /// 坐标原点
        /// </summary>
        public static SphericalCoordinatePoint Zero
        {
            get
            {
                return new SphericalCoordinatePoint(0, 0, 0);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="theta"></param>
        /// <param name="phi"></param>
        public SphericalCoordinatePoint(double radius, double theta, double phi)
        {
            this.Radius = radius;
            this.Theta = theta;
            this.Phi = phi;
        }

        /// <summary>
        /// 复制坐标值到目标点
        /// </summary>
        /// <param name="target"></param>
        public void CopyTo(SphericalCoordinatePoint target)
        {
            target.Radius = this.Radius;
            target.Theta = this.Theta;
            target.Phi = this.Phi;
        }

        /// <summary>
        /// 从源点复制坐标值
        /// </summary>
        /// <param name="source"></param>
        public void CopyFrom(SphericalCoordinatePoint source)
        {
            this.Radius = source.Radius;
            this.Theta = source.Theta;
            this.Phi = source.Phi;
        }

        /// <summary>
        /// 重置坐标值
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="theta"></param>
        /// <param name="phi"></param>
        public void Reset(double radius, double theta, double phi)
        {
            this.Radius = radius;
            this.Theta = theta;
            this.Phi = phi;
        }
    }
}
