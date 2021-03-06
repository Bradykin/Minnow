﻿using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class GameCard : GameElementBase, ILoad<JsonGameCardData>, ISave<JsonGameCardData>
{
    public enum Target
    { 
        Tile,
        Ally,
        Enemy,
        Unit,
        Building,
        None
    }

    protected int m_cost;
    protected bool m_xSpell = false;
    protected int m_costTempModifier = 0;
    public string m_typeline;
    public Target m_targetType;
    public bool m_shouldExile;
    public int m_storedTagWeight;

    public virtual string GetTypeline()
    {
        return m_typeline;
    }

    public int GetCost()
    {
        if (!GameHelper.IsInGame())
        {
            return m_cost;
        }

        if (m_xSpell)
        {
            return GameHelper.GetPlayer().GetCurEnergy();
        }

        int toReturn = m_cost + m_costTempModifier;


        if (this is GameCardSpellBase)
        {
            GamePlayer player = GameHelper.GetPlayer();

            if (GameHelper.HasRelic<ContentGoldenKnotRelic>())
            {
                toReturn += 3;
            }

            if (GameHelper.HasRelic<ContentTomeOfDuluhainRelic>())
            {
                toReturn -= 1;
            }

            if (GameHelper.HasRelic<ContentShardOfSorrowRelic>())
            {
                if (GameHelper.CardInPlayerDeck(this))
                {
                    int numShivsInHand = 0;
                    for (int i = 0; i < player.m_hand.Count; i++)
                    {
                        if (player.m_hand[i] is ContentShivCard)
                        {
                            numShivsInHand++;
                        }
                    }

                    toReturn -= numShivsInHand;
                }
            }

            for (int i = 0; i < player.m_controlledUnits.Count; i++)
            {
                if (player.m_controlledUnits[i] is ContentMysticWitch)
                {
                    toReturn -= 1;
                }
            }
        }
        else if (this is GameUnitCard)
        {
            if (GameHelper.HasRelic<ContentPinnacleOfFearRelic>())
            {
                toReturn -= 1;
            }
        }

        if (toReturn < 0)
        {
            toReturn = 0;
        }

        return toReturn;
    }

    public void SetTempCost(int i)
    {
        m_costTempModifier = i;
    }

    public virtual string GetDesc()
    {
        return m_desc;
    }

    public void SetDesc(string desc)
    {
        m_desc = desc;
    }

    public virtual void PlayCard() 
    {
        PayCost();
    }

    public virtual void PlayCard(GameTile targetTile) 
    {
        PayCost();
    }

    public virtual void PlayCard(GameUnit targetUnit) 
    {
        PayCost();
    }

    public virtual void PlayCard(GameBuildingBase targetBuilding) 
    {
        PayCost();
    }

    public virtual void OnDraw()
    {
        if (GameHelper.HasRelic<ContentMysticRuneRelic>())
        {
            m_cost = Random.Range(0, 4);
        }
    }

    protected virtual void PayCost()
    {
        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        player.SpendEnergy(GetCost());
    }

    public virtual void ResetCard()
    {

    }

    public virtual bool IsValidToPlay() 
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return false;
        }

        if (player.m_curEnergy >= GetCost())
        {
            return true;
        }

        return false; 
    }

    public virtual bool IsValidToPlay(GameTile targetTile) 
    {
        if (!IsValidToPlay())
        {
            return false;
        }

        if (m_targetType == Target.Tile)
        {
            return true;
        }

        return false; 
    }

    public virtual bool IsValidToPlay(GameUnit targetUnit) 
    {
        if (!IsValidToPlay())
        {
            return false;
        }

        if (targetUnit is ContentDemonMagicianEnemy)
        {
            return false;
        }

        if (targetUnit.GetFadeKeyword() != null && targetUnit.GetTeam() == Team.Enemy)
        {
            return false;
        }

        if (m_targetType == Target.Ally && targetUnit.GetTeam() == Team.Player)
        {
            return true;
        }

        if (m_targetType == Target.Enemy && targetUnit.GetTeam() == Team.Enemy)
        {
            return true;
        }

        if (m_targetType == Target.Unit)
        {
            return true;
        }

        return false; 
    }

    public virtual bool IsValidToPlay(GameBuildingBase targetBuilding)
    {
        if (!IsValidToPlay())
        {
            return false;
        }

        if (m_targetType == Target.Building)
        {
            return true;
        }

        return false;
    }

    public bool IsXSpell()
    {
        return m_xSpell;
    }

    //============================================================================================================//

    public virtual JsonGameCardData SaveToJson()
    {
        JsonGameCardData jsonData = new JsonGameCardData
        {
            baseName = GetName()
        };

        return jsonData;
    }

    public virtual void LoadFromJson(JsonGameCardData jsonData)
    {

    }
}
