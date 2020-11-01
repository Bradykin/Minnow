using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTag
{
    public enum TagType
    {
        Humanoid,
        Monster,
        Creation,
        Spellpower,
        Reanimate,
        StaminaRegen,
        MaxStamina,
        Victorious, 
        Knowledgeable,
        Enrage,
        Momentum,
        Spellcraft,
        Range,
        Forest,
        Mountain,
        Shiv,
        HighCost,
        LowCost,
        Healing,
        Gold,
        DamageShield,
        Explorer,
        Scaler,
        Midrange,
        Tank,
        Exile,
        DamageSpell,
        Brittle,
        BuffSpell,
        UtilitySpell,
        Vanilla
    }

    public List<TagType> m_tagTypes = new List<TagType>();

    public void AddTag(TagType newTag)
    {
        m_tagTypes.Add(newTag);
    }

    public static int GetTagValueFor(GameElementBase checkElement)
    {
        int tagValue = 8;

        for (int i = 0; i < checkElement.m_tags.m_tagTypes.Count; i++)
        {
            TagType curTag = checkElement.m_tags.m_tagTypes[i];

            //Check the player's existing deck
            List<GameCard> playerBaseDeck = GameHelper.GetPlayer().m_deckBase.GetCardsForRead();
            for (int c = 0; c < playerBaseDeck.Count; c++)
            {
                //Check unit typeline tags first
                if (curTag == TagType.Creation)
                {
                    if (playerBaseDeck[c].m_tags.HasTag(TagType.Monster) ||
                        playerBaseDeck[c].m_tags.HasTag(TagType.Humanoid))
                    {
                        tagValue--;
                    }
                    else if (playerBaseDeck[c].m_tags.HasTag(TagType.Creation))
                    {
                        tagValue++;
                    }
                }
                else if (curTag == TagType.Monster)
                {
                    if (playerBaseDeck[c].m_tags.HasTag(TagType.Creation) ||
                        playerBaseDeck[c].m_tags.HasTag(TagType.Humanoid))
                    {
                        tagValue--;
                    }
                    else if (playerBaseDeck[c].m_tags.HasTag(TagType.Monster))
                    {
                        tagValue++;
                    }
                }
                else if (curTag == TagType.Humanoid)
                {
                    if (playerBaseDeck[c].m_tags.HasTag(TagType.Creation) ||
                        playerBaseDeck[c].m_tags.HasTag(TagType.Monster))
                    {
                        tagValue--;
                    }
                    else if (playerBaseDeck[c].m_tags.HasTag(TagType.Humanoid))
                    {
                        tagValue++;
                    }
                }
                else //If the tag isn't based on creature types, handle it with the normal system
                {
                    if (playerBaseDeck[c].m_tags.HasTag(curTag))
                    {
                        if (GameTag.IsPullTag(curTag))
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
                if (playerRelics[c].m_tags.HasTag(curTag))
                {
                    //Check unit typeline tags first
                    if (curTag == TagType.Creation)
                    {
                        if (playerRelics[c].m_tags.HasTag(TagType.Monster) ||
                            playerRelics[c].m_tags.HasTag(TagType.Humanoid))
                        {
                            tagValue--;
                        }
                        else if (playerRelics[c].m_tags.HasTag(TagType.Creation))
                        {
                            tagValue++;
                        }
                    }
                    else if (curTag == TagType.Monster)
                    {
                        if (playerRelics[c].m_tags.HasTag(TagType.Creation) ||
                            playerRelics[c].m_tags.HasTag(TagType.Humanoid))
                        {
                            tagValue--;
                        }
                        else if (playerRelics[c].m_tags.HasTag(TagType.Monster))
                        {
                            tagValue++;
                        }
                    }
                    else if (curTag == TagType.Humanoid)
                    {
                        if (playerRelics[c].m_tags.HasTag(TagType.Creation) ||
                            playerRelics[c].m_tags.HasTag(TagType.Monster))
                        {
                            tagValue--;
                        }
                        else if (playerRelics[c].m_tags.HasTag(TagType.Humanoid))
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

        return tagValue;
    }

    public bool HasTag(TagType testTag)
    {
        return m_tagTypes.Contains(testTag);
    }

    //These tags push the player away from receiving more of this type
    public static bool IsPushTag(TagType tagType)
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
    }

    //These tags pull the player towards being offered more of these
    public static bool IsPullTag(TagType tagType)
    {
        return !IsPushTag(tagType);
    }
}
