using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UIWorldElementNotificationFactory : FactoryBase
    {
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UIWorldElementNotificationFactory(GameObject uiWorldElementNotification)
        {
            m_prefab = uiWorldElementNotification;
        }

        //============================================================================================================//


        public override GameObject CreateGameObject()
        {
            return Object.Instantiate(m_prefab);
        }

        public T CreateObject<T>(string message, Color color, WorldElementBase worldElement)
        {
            GameObject obj = CreateGameObject();
            obj.transform.position = worldElement.transform.position;

            obj.GetComponent<UIWorldElementNotification>().Init(message, color);

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

