using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UIAPBubbleFactory : FactoryBase
    {
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UIAPBubbleFactory(GameObject uiAPBubblePrefab)
        {
            m_prefab = uiAPBubblePrefab;
        }

        //============================================================================================================//


        public override GameObject CreateGameObject()
        {
            return Object.Instantiate(m_prefab);
        }

        public T CreateObject<T>(Transform apContainer, bool isActive, Team team, int index, bool inWorld)
        {
            GameObject obj = CreateGameObject();
            obj.transform.SetParent(apContainer);

            float multiplier = 1f;
            if (inWorld)
            {
                multiplier = -1f;
            }

            if (index < 6)
            {
                obj.transform.localPosition = new Vector3(4.0f + index * 10f, multiplier * -10.0f, 0.0f);
            }
            else
            {
                obj.transform.localPosition = new Vector3(4.0f + (index - 6f) * 10f, 0.0f, 0.0f);
            }

            obj.GetComponent<UIAPBubble>().Init(isActive, team);

            return obj.GetComponent<T>();
        }

        public override T CreateObject<T>()
        {
            if (Recycler.TryGrab(out T newObject))
            {
                return newObject;
            }

            var monoBehaviourComponent = CreateGameObject().GetComponent<T>();

            return monoBehaviourComponent;
        }

        //============================================================================================================//
    }
}