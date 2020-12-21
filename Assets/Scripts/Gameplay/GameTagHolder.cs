using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


    public void AddPushTag(TagType tagType, int tagWeight = 1, bool isReceiver = true)
    {
        m_tags.RemoveAll(t => t.m_tagType == tagType && t.m_tagInfluence == TagInfluence.None);

        m_tags.Add(new GameTag(tagType, TagInfluence.Push, tagWeight, isReceiver));
    }

    public void AddPullTag(TagType tagType, int tagWeight = 2, bool isReceiver = true)
    {
        m_tags.RemoveAll(t => t.m_tagType == tagType && t.m_tagInfluence == TagInfluence.None);

        m_tags.Add(new GameTag(tagType, TagInfluence.Pull, tagWeight, isReceiver));
    }

    public void AddReceiverOnlyTag(TagType tagType)
    {
        m_tags.Add(new GameTag(tagType, TagInfluence.None, 0, true));
    }

    public static int GetTagValueFor(GameElementBase checkElement)
    {
        return GameNotificationManager.GameDirector.GetTagValueFor(checkElement);
    }
}
