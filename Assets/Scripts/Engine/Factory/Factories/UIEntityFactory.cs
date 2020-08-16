using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace Game.Util
{
    public class UIEntityFactory : FactoryBase
    {
        private readonly GameObject m_prefab;

        //============================================================================================================//

        public UIEntityFactory(GameObject uiEntityPrefab)
        {
            m_prefab = uiEntityPrefab;
        }

        //============================================================================================================//


        public override GameObject CreateGameObject()
        {
            return Object.Instantiate(m_prefab);
        }

        public GameObject CreateGameObject(WorldTile tile)
        {
            Vector2 pos = UIHelper.GetScreenPositionForWorldGridElement(tile.m_gameTile.m_gridPosition.x, tile.m_gameTile.m_gridPosition.y);

            GameObject obj = CreateGameObject();
            obj.transform.position = pos;

            GameObject uiParent = GameObject.Find("UI");
            if (uiParent != null)
            {
                obj.transform.parent = uiParent.transform;
            }

            obj.GetComponent<UIEntity>().Init(tile.m_gameTile.m_occupyingEntity);

            return obj;
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

