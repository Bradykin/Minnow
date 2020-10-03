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
            obj.transform.position = tile.GetScreenPositionForUnit();

            GameObject uiParent = GameObject.Find("UI");
            if (uiParent != null)
            {
                obj.transform.parent = uiParent.transform;
            }

            WorldUnit uiUnit = obj.GetComponent<WorldUnit>();

            if (tile.GetGameTile().m_isFog)
            {
                uiUnit.SetVisible(false);
            }

            uiUnit.Init(tile.GetGameTile().m_occupyingUnit);

            if (uiUnit.GetUnit().GetTeam() == Team.Player)
            {
                FactoryManager.Instance.GetFactory<UIBorderUnitFactory>().CreateObject<UIBorderUnit>(uiUnit);
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

