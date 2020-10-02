using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UIBorderUnitFactory : FactoryBase
    {

        private Transform m_HUDTransform;
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UIBorderUnitFactory(GameObject uiBorderUnitPrefab, GameObject hudParent)
        {
            m_prefab = uiBorderUnitPrefab;
            m_HUDTransform = hudParent.transform;
        }

        //============================================================================================================//


        public override GameObject CreateGameObject()
        {
            return Object.Instantiate(m_prefab);
        }

        public T CreateObject<T>(UIEntity ownerEntity)
        {
            GameObject obj = CreateGameObject();

            obj.transform.SetParent(m_HUDTransform);
            obj.GetComponent<UIBorderUnit>().Init(ownerEntity);

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