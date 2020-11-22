using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UIStaminaBubbleFactory : FactoryBase
    {
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UIStaminaBubbleFactory(GameObject staminaBubblePrefab)
        {
            m_prefab = staminaBubblePrefab;
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

        public T CreateObject<T>(Transform staminaContainer, bool isActive, Team team, int index, bool inWorld)
        {
            GameObject obj = CreateGameObject(staminaContainer);

            if (inWorld)
            {
                obj.transform.localPosition = new Vector3(-62.5f + (12.5f * index), 4.0f, 0.0f);
            }
            else
            {
                if (index < 6)
                {
                    obj.transform.localPosition = new Vector3(4.0f + index * 15f, -10.0f, 0.0f);
                }
                else
                {
                    obj.transform.localPosition = new Vector3(4.0f + (index - 6f) * 15f, 4.0f, 0.0f);
                }
            }

            obj.GetComponent<UIStaminaBubble>().Init(isActive, team);

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