using UnityEngine;
using Object = UnityEngine.Object;

namespace BJGF.Tools.Extension
{
    public static class GameObject_Extension
    {
        public static void ResetParent(this GameObject child, GameObject parent)
        {
            child.transform.SetParent(parent.transform);
            child.ResetTransform();
        }

        public static void ResetParent(this GameObject child, Transform parent)
        {
            child.transform.SetParent(parent);
            child.ResetTransform();
        }


        public static void ResetParent(this Transform child, GameObject parent)
        {
            child.SetParent(parent.transform);
            child.ResetTransform();
        }


        public static void ResetParent(this Transform child, Transform parent)
        {
            child.SetParent(parent);
            child.ResetTransform();
        }


        public static void ResetTransform(this GameObject obj, bool isWorld = false)
        {
            ResetTransform(obj.transform);
        }

        public static void ResetTransform(this Transform tran, bool isWorld = false)
        {
            if (!isWorld)
            {
                tran.localPosition = Vector3.zero;
                tran.localScale = Vector3.one;
                tran.localRotation = Quaternion.identity;
            }
            else
            {
                tran.position = Vector3.zero;
                tran.localScale = Vector3.one;
                tran.rotation = Quaternion.identity;
            }
        }

        public static float GetParticleDuration(this GameObject gameObject, bool includeChildren = true, bool includeInactive = false, bool allowLoop = false)
        {
            if (includeChildren)
            {
                var particles = gameObject.GetComponentsInChildren<ParticleSystem>(includeInactive);
                var duration = -1f;
                for (var i = 0; i < particles.Length; i++)
                {
                    var ps = particles[i];
                    var time = ps.GetDuration(allowLoop);
                    if (time > duration)
                    {
                        duration = time;
                    }
                }

                return duration;
            }
            else
            {
                var ps = gameObject.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    return ps.GetDuration(allowLoop);
                }
                else
                {
                    return -1f;
                }
            }

        }
        /// <summary>
        /// 实例化对象并设置父节点
        /// </summary>
        /// <param name="obj"> 待实例化的对象 </param>
        /// <param name="parent"> 父节点 </param>
        /// <returns> 实例化的对象 </returns>
        public static GameObject Instantiate(Object obj, GameObject parent = null)
        {
            if (obj == null)
            {
                Debug.LogError("Instantiate obj is nil");
                return null;
            }
            GameObject newObj = GameObject.Instantiate(obj) as GameObject;
            if (parent != null)
            {
                newObj.transform.SetParent(parent.transform);
            }

            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localScale = Vector3.one;
            newObj.transform.localRotation = Quaternion.identity;
            return newObj;
        }

    }
}
