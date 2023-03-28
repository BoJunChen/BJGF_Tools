using UnityEngine;

namespace BJGF.Tools.Utility
{
    /// <summary>
    /// 向量工具
    /// </summary>
    public static class VectorTool
    {
        /// <summary>
        /// 通过角度获取二维方向向量
        /// </summary>
        /// <param name="angle"> 角度 </param>
        /// <returns> 带方向的单位向量 </returns>
        public static Vector2 Get2DirByAngle(float angle)
        {
            var x = Mathf.Cos(angle);
            var y = Mathf.Sin(angle);

            var dir = new Vector2(x, y);
            dir = dir.normalized;
            return dir;
        }


        /// <summary>
        /// 获取2D向量的垂直向量
        /// </summary>
        /// <param name="vector"> 2D向量 </param>
        /// <param name="isNormalized"> [true 归一化] [false 不归一化] </param>
        /// <returns> 向量的垂直向量 </returns>
        public static Vector2 Get2DVectical(Vector2 vector, bool isNormalized = false)
        {
            var x = -vector.y;
            var y = vector.x;

            var temp = new Vector2(x, y);
            temp = isNormalized ? temp.normalized : temp;

            return temp;
        }

        /// <summary>
        /// 向量按Z轴偏移一定角度
        /// </summary>
        /// <param name="vec"> 原始向量 </param>
        /// <param name="angle"> 偏移的角度 </param>
        /// <returns> 偏移后的向量 </returns>
        public static Vector3 OffsetAngle(this Vector3 vec, float angle)
        {
            var qAng = Quaternion.Euler(new Vector3(0, 0, angle));
            vec = qAng * vec;

            return vec;
        }
    }

}
