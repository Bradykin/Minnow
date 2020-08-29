using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UIEventFactory : FactoryBase
    {
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UIEventFactory(GameObject uiEventPrefab)
        {
            m_prefab = uiEventPrefab;
        }

        //============================================================================================================//


        public override GameObject CreateGameObject()
        {
            return Object.Instantiate(m_prefab);
        }

        public T CreateObject<T>(WorldTile tile)
        {
            GameObject obj = CreateGameObject();
            obj.transform.position = tile.GetScreenPositionForEvent();

            GameObject uiParent = GameObject.Find("UI");
            if (uiParent != null)
            {
                obj.transform.parent = uiParent.transform;
            }

            obj.GetComponent<UIEvent>().Init(tile.GetGameTile().m_event);

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

