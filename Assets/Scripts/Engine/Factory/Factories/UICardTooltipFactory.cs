using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UICardTooltipFactory : FactoryBase
    {
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UICardTooltipFactory(GameObject uiCardPrefab)
        {
            m_prefab = uiCardPrefab;
        }

        //============================================================================================================//


        public override GameObject CreateGameObject()
        {
            return Object.Instantiate(m_prefab);
        }

        public T CreateObject<T>(GameCard card, UICard.CardDisplayType displayType)
        {
            GameObject obj = CreateGameObject();

            if (displayType == UICard.CardDisplayType.Tooltip)
            {
                obj.transform.SetParent(UITooltipController.Instance.transform);
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