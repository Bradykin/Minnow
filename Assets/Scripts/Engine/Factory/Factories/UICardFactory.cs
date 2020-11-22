using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UICardFactory : FactoryBase
    {

        private Transform m_playerHandParent;
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UICardFactory(GameObject uiCardPrefab, GameObject playerHandParent)
        {
            m_prefab = uiCardPrefab;
            m_playerHandParent = playerHandParent.transform;
        }

        //============================================================================================================//


        public override GameObject CreateGameObject()
        {
            return Object.Instantiate(m_prefab);
        }

        public override GameObject CreateGameObject(Transform parent)
        {
            return Object.Instantiate(m_prefab, parent);
        }

        public T CreateObject<T>(GameCard card, UICard.CardDisplayType displayType)
        {
            Transform parent = null;
            if (displayType == UICard.CardDisplayType.Hand)
            {
                parent = m_playerHandParent;
            }
            else if (displayType == UICard.CardDisplayType.Tooltip)
            {
                parent = UITooltipController.Instance.transform;
            }

            GameObject obj;
            if (parent == null)
            {
                obj = CreateGameObject();
            }
            else
            {
                obj = CreateGameObject(parent);
            }

            obj.GetComponent<UICard>().Init(card, displayType);

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