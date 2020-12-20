using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTagHolder
{
    public enum TagInfluence : int
    {
        Push, //Makes further content more likely to have this tag
        Pull, //Makes further content less likely to have this tag
        None //Has no impact on later content
    }
    
    public enum TagType : int
    {
        //Creature Types
        Humanoid,
        Monster,
        Creation,

        //Keywords
        Victorious,
        Knowledgeable,
        Enrage,
        Momentum,
        Spellcraft,
        Range,
        DamageShield,

        //Archetypes
        MagicPower,
        Reanimate,
        Forest,
        Water,

        //Unit Roles
        Explorer,
        Scaler,
        Midrange,
        Tank,
        UtilityUnit,

        //Unit Stats
        StaminaRegen,
        MaxStamina,

        //Card Cost
        HighCost,
        LowCost,

        //Spell Usage
        DamageSpell,
        BuffSpell,
        UtilitySpell,

        //Other
        EnergyRegen,
        Gold,
        Brittle,
        Shiv,
        Exile,
        Healing
    }

    public List<GameTag> m_tags = new List<GameTag>();


    public void AddPushTag(TagType newTag, int tagWeight = 1, bool isReceiver = true)
    {
        m_tags.Add(new GameTag(newTag, TagInfluence.Push, tagWeight, isReceiver));
    }

    public void AddPullTag(TagType newTag, int tagWeight = 2, bool isReceiver = true)
    {
        m_tags.Add(new GameTag(newTag, TagInfluence.Pull, tagWeight, isReceiver));
    }

    public void AddReceiverOnlyTag(TagType newTag)
    {
        m_tags.Add(new GameTag(newTag, TagInfluence.None, 0, true));
    }

    public static int GetTagValueFor(GameElementBase checkElement)
    {
        return GameNotificationManager.GameDirector.GameDirectorRun.GetTagValueFor(checkElement);
        
        /*int tagValue = 12;

        for (int i = 0; i < checkElement.m_tagHolder.m_tags.Count; i++)
        {
            TagType curTag = checkElement.m_tagHolder.m_tags[i].m_tagType;

            //Check the player's existing deck
            List<GameCard> playerBaseDeck = GameHelper.GetPlayer().m_deckBase.GetCardsForRead();
            for (int c = 0; c < playerBaseDeck.Count; c++)
            {
                //Check unit typeline tags first
                if (curTag == TagType.Creation)
                {
                    if (playerBaseDeck[c].m_tagHolder.HasTag(TagType.Monster) ||
                        playerBaseDeck[c].m_tagHolder.HasTag(TagType.Humanoid))
                    {
                        tagValue--;
                    }
                    else if (playerBaseDeck[c].m_tagHolder.HasTag(TagType.Creation))
                    {
                        tagValue++;
                    }
                }
                else if (curTag == TagType.Monster)
                {
                    if (playerBaseDeck[c].m_tagHolder.HasTag(TagType.Creation) ||
                        playerBaseDeck[c].m_tagHolder.HasTag(TagType.Humanoid))
                    {
                        tagValue--;
                    }
                    else if (playerBaseDeck[c].m_tagHolder.HasTag(TagType.Monster))
                    {
                        tagValue++;
                    }
                }
                else if (curTag == TagType.Humanoid)
                {
                    if (playerBaseDeck[c].m_tagHolder.HasTag(TagType.Creation) ||
                        playerBaseDeck[c].m_tagHolder.HasTag(TagType.Monster))
                    {
                        tagValue--;
                    }
                    else if (playerBaseDeck[c].m_tagHolder.HasTag(TagType.Humanoid))
                    {
                        tagValue++;
                    }
                }
                else //If the tag isn't based on creature types, handle it with the normal system
                {
                    if (playerBaseDeck[c].m_tagHolder.HasTag(curTag))
                    {
                        if (GameTagHolder.IsPullTag(curTag))
                        {
                            tagValue++;
                        }
                        else
                        {
                            tagValue--;
                        }
                    }
                }
            }

            //Then check the players' existing relics
            List<GameRelic> playerRelics = GameHelper.GetPlayer().GetRelics().GetRelicListForRead();
            for (int c = 0; c < playerRelics.Count; c++)
            {
                if (playerRelics[c].m_tagHolder.HasTag(curTag))
                {
                    //Check unit typeline tags first
                    if (curTag == TagType.Creation)
                    {
                        if (playerRelics[c].m_tagHolder.HasTag(TagType.Monster) ||
                            playerRelics[c].m_tagHolder.HasTag(TagType.Humanoid))
                        {
                            tagValue--;
                        }
                        else if (playerRelics[c].m_tagHolder.HasTag(TagType.Creation))
                        {
                            tagValue++;
                        }
                    }
                    else if (curTag == TagType.Monster)
                    {
                        if (playerRelics[c].m_tagHolder.HasTag(TagType.Creation) ||
                            playerRelics[c].m_tagHolder.HasTag(TagType.Humanoid))
                        {
                            tagValue--;
                        }
                        else if (playerRelics[c].m_tagHolder.HasTag(TagType.Monster))
                        {
                            tagValue++;
                        }
                    }
                    else if (curTag == TagType.Humanoid)
                    {
                        if (playerRelics[c].m_tagHolder.HasTag(TagType.Creation) ||
                            playerRelics[c].m_tagHolder.HasTag(TagType.Monster))
                        {
                            tagValue--;
                        }
                        else if (playerRelics[c].m_tagHolder.HasTag(TagType.Humanoid))
                        {
                            tagValue++;
                        }
                    }
                    else //If the tag isn't based on creature types, handle it with the normal system
                    {
                        if (IsPullTag(curTag))
                        {
                            tagValue++;
                        }
                        else
                        {
                            tagValue--;
                        }
                    }
                }
            }
        }

        //Can technically return below 0; but it will be treated as a 0% change to obtain.  Leaving it for interest's sake to see what some things get to.

        return tagValue;*/
    }

    //These tags push the player away from receiving more of this type
    /*public static bool IsPushTag(TagType tagType)
    {
        if (tagType == TagType.Gold ||
            tagType == TagType.Explorer ||
            tagType == TagType.Scaler ||
            tagType == TagType.Midrange ||
            tagType == TagType.Tank ||
            tagType == TagType.Exile ||
            tagType == TagType.UtilitySpell ||
            tagType == TagType.Brittle)
        {
            return true;
        }

        return false;
    }*/

    //These tags pull the player towards being offered more of these
    /*public static bool IsPullTag(TagType tagType)
    {
        return !IsPushTag(tagType);
    }*/
}
