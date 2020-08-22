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

        public T CreateObject<T>(Transform apContainer, bool isActive, int index)
        {
            GameObject obj = CreateGameObject();
            obj.transform.parent = apContainer;
            obj.transform.localPosition = new Vector3(index * 0.2f, 0.0f, 0.0f);

            obj.GetComponent<UIAPBubble>().Init(isActive);

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