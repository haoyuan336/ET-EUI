using System.Collections.Generic;
using UnityEngine;
namespace DefaultNamespace
{
    public static class GameObjectFindHelper
    {
        public static Transform Find(GameObject parent, string name)
        {
            Transform result = null;
            Stack<GameObject> stack = new Stack<GameObject>();
            stack.Push(parent);
            while (stack.Count > 0 && result == null)
            {
                GameObject gameObject = stack.Pop();
                result = gameObject.transform.Find(name);
                if (result != null)
                {
                    return result;
                }
                for (var i = 0; i < gameObject.transform.childCount; i++)
                {
                    var transform = gameObject.transform.GetChild(i);
                    if (transform.childCount > 0)
                    {
                        stack.Push(transform.gameObject);
                    }
                }
            }

            return result;
        }
    }
}