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

        public T CreateObject<T>(GameActionIntermission action)
        {
            GameObject obj = CreateGameObject();

            GameObject uiParent = GameObject.Find("ActionHolder");
            if (uiParent != null)
            {
                obj.transform.parent = uiParent.transform;
            }

            obj.GetComponent<GameIntermissionActionController>().Init(action);

            return obj.GetComponent<T>();
        }

        public T CreateObject<T>(GameTechIntermission tech)
        {
            GameObject obj = CreateGameObject();

            GameObject uiParent = GameObject.Find("TechHolder");
            if (uiParent != null)
            {
                obj.transform.parent = uiParent.transform;
            }

            obj.GetComponent<GameIntermissionActionController>().Init(tech);

            return obj.GetComponent<T>();
        }

        public T CreateObject<T>(GameBuildingIntermission building)
        {
            GameObject obj = CreateGameObject();

            GameObject uiParent = GameObject.Find("BuildingHolder");
            if (uiParent != null)
            {
                obj.transform.parent = uiParent.transform;
            }

            obj.GetComponent<GameIntermissionActionController>().Init(building);

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
