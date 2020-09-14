using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UICardFactory : FactoryBase
    {

        private readonly Transform m_waveHUD;
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UICardFactory(GameObject uiCardPrefab)
        {
            m_prefab = uiCardPrefab;
            GameObject waveHUD = GameObject.Find("WaveHUD");
            if (waveHUD != null)
                m_waveHUD = waveHUD.transform;
        }

        //============================================================================================================//


        public override GameObject CreateGameObject()
        {
            return Object.Instantiate(m_prefab);
        }

        public T CreateObject<T>(GameCard card, UICard.CardDisplayType displayType)
        {
            GameObject obj = CreateGameObject();

            if (displayType == UICard.CardDisplayType.Hand)
            {
                obj.transform.parent = m_waveHUD;
            }
            else if (displayType == UICard.CardDisplayType.Tooltip)
            {
                obj.transform.parent = UITooltipController.Instance.transform;
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