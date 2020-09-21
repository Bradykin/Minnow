using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UIRelicFactory : FactoryBase
    {
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UIRelicFactory(GameObject uiRelicFactory)
        {
            m_prefab = uiRelicFactory;
        }

        //============================================================================================================//


        public override GameObject CreateGameObject()
        {
            return Object.Instantiate(m_prefab);
        }

        public T CreateObject<T>(GameRelic relic, Transform parent)
        {
            GameObject obj = CreateGameObject();

            obj.transform.SetParent(parent);

            obj.GetComponent<UIRelic>().Init(relic, UIRelic.RelicSelectionType.View);

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

