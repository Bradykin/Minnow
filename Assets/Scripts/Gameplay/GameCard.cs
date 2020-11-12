using Game.Util;
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
        None //This is used for spells like 'Draw 3'
    }

    protected int m_cost;
    protected int m_costTempModifier = 0;
    public string m_typeline;
    public Target m_targetType;
    public bool m_shouldExile;
    public int m_storedTagWeight;

    protected int m_playerUnlockLevel;

    public virtual string GetTypeline()
    {
        return m_typeline;
    }

    public int GetCost()
    {
        int toReturn = m_cost + m_costTempModifier;

        if (GameHelper.HasRelic<ContentGoldenKnotRelic>() && this is GameCardSpellBase)
        {
            toReturn += 3;
        }

        if (this is GameCardSpellBase)
        {
            if (GameHelper.HasRelic<ContentTomeOfDuluhainRelic>())
            {
                toReturn -= 1;
            }

            if (GameHelper.HasRelic<ContentShardOfSorrowRelic>())
            {
                if (GameHelper.CardInPlayerDeck(this))
                {
                    int numShivsInHand = 0;
                    for (int i = 0; i < GameHelper.GetPlayer().m_hand.Count; i++)
                    {
                        if (GameHelper.GetPlayer().m_hand[i] is ContentShivCard)
                        {
                            numShivsInHand++;
                        }
                    }

                    toReturn -= numShivsInHand;
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

    public virtual bool PlayerHasUnlockedCard()
    {
        return Constants.CheatsOn || (PlayerDataManager.GetCurLevel() >= GetPlayerUnlockLevel());
    }

    public int GetPlayerUnlockLevel()
    {
        return m_playerUnlockLevel;
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

    public virtual void InitializeWithLevel(int level) { }

    public virtual int GetCardLevel()
    {
        if (m_rarity == GameRarity.Starter)
        {
            if (PlayerDataManager.PlayerAccountData.m_starterCardUnlockLevels.ContainsKey(GetBaseName()))
            {
                return PlayerDataManager.PlayerAccountData.m_starterCardUnlockLevels[GetBaseName()];
            }
            else
            {
                return 0;
            }
        }
        //TODO: alex - Hook this up to player save data.

        return 0;
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
