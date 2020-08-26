using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UISimpleTooltipFactory : FactoryBase
    {
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UISimpleTooltipFactory(GameObject uiSimpleTooltip)
        {
            m_prefab = uiSimpleTooltip;
        }

        //============================================================================================================//


        public override GameObject CreateGameObject()
        {
            return Object.Instantiate(m_prefab);
        }

        public T CreateObject<T>(string title, string desc)
        {
            GameObject obj = CreateGameObject();
            obj.transform.parent = UITooltipController.Instance.transform;

            obj.GetComponent<UISimpleTooltip>().Init(title, desc);

            return obj.GetComponent<T>();
        }

        public T CreateObject<T>(string title, string desc, Team team)
        {
            GameObject obj = CreateGameObject();
            obj.transform.parent = UITooltipController.Instance.transform;

            obj.GetComponent<UISimpleTooltip>().Init(title, desc, team);

            return obj.GetComponent<T>();
        }

        public T CreateObject<T>(string title, string desc, bool isValid)
        {
            GameObject obj = CreateGameObject();
            obj.transform.parent = UITooltipController.Instance.transform;

            obj.GetComponent<UISimpleTooltip>().Init(title, desc, isValid);

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

