using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GameTag
{
    public GameTagHolder.TagType m_tagType;
    public GameTagHolder.TagInfluence m_tagInfluence;
    public int m_tagWeight;
    public bool m_isReceiver;

    public GameTag(GameTagHolder.TagType tagType, GameTagHolder.TagInfluence tagInfluence, int tagWeight, bool isReceiver)
    {
        m_tagType = tagType;
        m_tagInfluence = tagInfluence;
        m_tagWeight = tagWeight;
        m_isReceiver = isReceiver;
    }
}
