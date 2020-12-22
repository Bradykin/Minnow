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

        public GameObject CreateGameObject(Transform parent, Vector3 position)
        {
            return Object.Instantiate(m_prefab, position, Quaternion.identity, parent);
        }

        public T CreateObject<T>(GameRelic relic, Transform parent)
        {
            GamePlayer player = GameHelper.GetPlayer();

            GameObject obj;

            if (player == null)
            {
                 obj = CreateGameObject(parent, new Vector3(0,0,0));
            }
            else
            {
                obj = CreateGameObject(parent, new Vector3(200.0f + player.GetRelics().GetSize() * 60.0f, -25.0f, 0.0f));
            }

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

