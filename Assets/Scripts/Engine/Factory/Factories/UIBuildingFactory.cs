using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UIBuildingFactory : FactoryBase
    {
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UIBuildingFactory(GameObject uibuildingPrefab)
        {
            m_prefab = uibuildingPrefab;
        }

        //============================================================================================================//


        public override GameObject CreateGameObject()
        {
            return Object.Instantiate(m_prefab);
        }

        public T CreateObject<T>(GameBuildingBase building, WorldTile tile)
        {
            GameObject obj = CreateGameObject();
            obj.transform.position = tile.GetScreenPositionForBuilding();

            obj.GetComponent<UIBuilding>().Init(building, tile);

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