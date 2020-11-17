using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UIUnitFactory : FactoryBase
    {
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UIUnitFactory(GameObject uiUnitPrefab)
        {
            m_prefab = uiUnitPrefab;
        }

        //============================================================================================================//


        public override GameObject CreateGameObject()
        {
            return Object.Instantiate(m_prefab);
        }

        public T CreateObject<T>(WorldTile tile)
        {
            GameObject obj = CreateGameObject();
            WorldUnit uiUnit = obj.GetComponent<WorldUnit>();
            uiUnit.Init(tile.GetGameTile().m_occupyingUnit);
            /*obj.transform.position = tile.GetScreenPositionForUnit();

            GameObject uiParent = GameObject.Find("UI");
            if (uiParent != null)
            {
                obj.transform.parent = uiParent.transform;
            }*/

            obj.transform.position = tile.GetScreenPositionForUnit(uiUnit);

            GameObject uiParent = GameObject.Find("UI");
            if (uiParent != null)
            {
                obj.transform.parent = uiParent.transform;
            }

            uiUnit.SetMoveTarget(tile.GetScreenPositionForUnit(uiUnit));

            if (tile.GetGameTile().m_isFog)
            {
                uiUnit.SetVisible(false);
            }

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

