using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Mathes.Coordinate
{
    /// <summary>
    /// 几何坐标帮助器
    /// </summary>
    public static class CoordinateHelper
    {
        #region 直角坐标转球面坐标
        /// <summary>
        /// 直角坐标转球面坐标
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public static SphericalCoordinatePoint ToSphericalCoordinatePoint(RectangularCoordinatePoint coord)
        {
            double radius = GetModuloFromCartesianCoord(coord);
            double theta = GetThetaFromCartesianCoord(coord);
            double phi = GetPhiFromCartesianCoord(coord);
            return new SphericalCoordinatePoint(radius, theta, phi);
        }

        private static double GetModuloFromCartesianCoord(RectangularCoordinatePoint coord)
        {
            return Math.Sqrt(coord.X * coord.X + coord.Y * coord.Y + coord.Z * coord.Z);
        }

        private static double GetThetaFromCartesianCoord(RectangularCoordinatePoint coord)
        {
            return Math.Acos(coord.Z / GetModuloFromCartesianCoord(coord));
        }

        private static double GetPhiFromCartesianCoord(RectangularCoordinatePoint coord)
        {
            return Math.Atan(coord.Y / coord.X);
        }
        #endregion

        #region 球面坐标转直角坐标
        /// <summary>
        /// 球面坐标转直角坐标
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public static RectangularCoordinatePoint ToRectangularCoordinatePoint(SphericalCoordinatePoint coord)
        {
            var x = GetXFromSphericalCoord(coord);
            var y = GetYFromSphericalCoord(coord);
            var z = GetZFromSphericalCoord(coord);
            return new RectangularCoordinatePoint(x, y, z);
        }

        private static double GetXFromSphericalCoord(SphericalCoordinatePoint coord)
        {
            return coord.Radius * Math.Sin(coord.Theta) * Math.Cos(coord.Phi);
        }

        private static double GetYFromSphericalCoord(SphericalCoordinatePoint coord)
        {
            return coord.Radius * Math.Sin(coord.Theta) * Math.Sin(coord.Phi);
        }

        private static double GetZFromSphericalCoord(SphericalCoordinatePoint coord)
        {
            return coord.Radius * Math.Cos(coord.Theta);
        }
        #endregion
    }
}
