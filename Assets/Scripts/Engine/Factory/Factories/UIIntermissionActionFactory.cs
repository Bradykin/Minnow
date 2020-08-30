using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UIIntermissionActionFactory : FactoryBase
    {
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UIIntermissionActionFactory(GameObject uiIntermissionActionPrefab)
        {
            m_prefab = uiIntermissionActionPrefab;
        }

        //============================================================================================================//


        public override GameObject CreateGameObject()
        {
            return Object.Instantiate(m_prefab);
        }

        public T CreateObject<T>(GameActionIntermission action, Transform parent)
        {
            GameObject obj = CreateGameObject();
            obj.transform.parent = parent;

            obj.GetComponent<UIActionController>().Init(action);

            return obj.GetComponent<T>();
        }

        public T CreateObject<T>(GameTechIntermission tech, Transform parent)
        {
            GameObject obj = CreateGameObject();
            obj.transform.parent = parent;

            obj.GetComponent<UIActionController>().Init(tech);

            return obj.GetComponent<T>();
        }

        public T CreateObject<T>(GameBuildingIntermission building, Transform parent)
        {
            GameObject obj = CreateGameObject();
            obj.transform.parent = parent;

            obj.GetComponent<UIActionController>().Init(building);

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
