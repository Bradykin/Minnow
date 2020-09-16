using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Util
{
    
    //Based on: https://www.dofactory.com/net/factory-method-design-pattern
    public class FactoryManager : Singleton<FactoryManager>
    {
        //============================================================================================================//

        [SerializeField]
        private GameObject m_worldTilePrefab = null;
        [SerializeField]
        private GameObject m_uiEntityPrefab = null;
        [SerializeField]
        private GameObject m_uiCardPrefab = null;
        [SerializeField]
        private GameObject m_uiCardTooltipPrefab = null;
        [SerializeField]
        private GameObject m_uiWorldElementNotificationPrefab = null;
        [SerializeField]
        private GameObject m_uiAPBubblePrefab = null;
        [SerializeField]
        private GameObject m_uiRelicPrefab = null;

        //Tooltips
        [SerializeField]
        private GameObject m_uiSimpleTooltipPrefab = null;

        [SerializeField]
        private UIHUDController m_uiHUDController = null;

        //============================================================================================================//;

        private Dictionary<Type, FactoryBase> _factoryBases;
        
        //============================================================================================================//

        public T GetFactory<T>() where T : FactoryBase
        {
            var type = typeof(T);
            
            if (_factoryBases == null)
            {
                _factoryBases = new Dictionary<Type, FactoryBase>();
            }

            if (!_factoryBases.ContainsKey(type))
            {
                _factoryBases.Add(type, CreateFactory<T>());
            }
            
            
            return _factoryBases[type] as T;
        }

        private T CreateFactory<T>() where T : FactoryBase
        {
            var type = typeof(T);
            switch (true)
            {
                case bool _ when type == typeof(WorldTileFactory):
                    return new WorldTileFactory(m_worldTilePrefab) as T;
                case bool _ when type == typeof(UIEntityFactory):
                    return new UIEntityFactory(m_uiEntityPrefab) as T;
                case bool _ when type == typeof(UICardFactory):
                    return new UICardFactory(m_uiCardPrefab, m_uiHUDController.m_waveHUD) as T;
                case bool _ when type == typeof(UICardTooltipFactory):
                    return new UICardTooltipFactory(m_uiCardTooltipPrefab) as T;
                case bool _ when type == typeof(UIWorldElementNotificationFactory):
                    return new UIWorldElementNotificationFactory(m_uiWorldElementNotificationPrefab) as T;
                case bool _ when type == typeof(UISimpleTooltipFactory):
                    return new UISimpleTooltipFactory(m_uiSimpleTooltipPrefab) as T;
                case bool _ when type == typeof(UIAPBubbleFactory):
                    return new UIAPBubbleFactory(m_uiAPBubblePrefab) as T;
                case bool _ when type == typeof(UIRelicFactory):
                    return new UIRelicFactory(m_uiRelicPrefab) as T;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type.Name, null);
            }
        }
        
        //============================================================================================================//

    }
}


