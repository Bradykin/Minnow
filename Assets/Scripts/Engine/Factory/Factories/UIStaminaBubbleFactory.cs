﻿using System.Collections.Generic;
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

        public T CreateObject<T>(Transform staminaContainer, bool isActive, Team team, int index, bool inWorld)
        {
            GameObject obj = CreateGameObject();
            obj.transform.SetParent(staminaContainer);

            if (index < 6)
            {
                if (inWorld)
                {
                    obj.transform.localPosition = new Vector3(0.35f * index, 0, 0.0f);
                }
                else
                {
                    obj.transform.localPosition = new Vector3(4.0f + index * 10f, -10.0f, 0.0f);
                }
            }
            else
            {
                if (inWorld)
                {
                    obj.transform.localPosition = new Vector3(0.35f * (index - 6f), -0.35f, 0.0f);
                }
                else
                {
                    obj.transform.localPosition = new Vector3(4.0f + (index - 6f) * 10f, 0.0f, 0.0f);
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