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

        public T CreateObject<T>(string message, Color color, GameObject positionObj)
        {
            GameObject obj = CreateGameObject();
            obj.transform.position = positionObj.transform.position;

            obj.GetComponent<UIWorldElementNotification>().Init(message, color);

            return obj.GetComponent<T>();
        }

        public T CreateObject<T>(string message, Color color, Vector3 position)
        {
            GameObject obj = CreateGameObject();
            obj.transform.position = Camera.main.ScreenToWorldPoint(position);

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

