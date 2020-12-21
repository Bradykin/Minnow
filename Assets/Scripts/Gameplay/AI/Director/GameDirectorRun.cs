using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SerializeField]
public class GameDirectorRun
{
    private const int tagWeightMaximums = 10;
    
    public List<GameDirectorTagWeight> tagWeights = new List<GameDirectorTagWeight>();

    public void RecordCardChoice(in GameCard cardChoice, in GameCard optionOne, in GameCard optionTwo, in GameCard optionThree)
    {
        if (cardChoice != null)
        {
            AddTagValue(cardChoice.m_tagHolder.m_tags);
        }
    }

    public void RecordCardSingleChoice(in GameCard cardOption, bool taken)
    {
        if (taken)
        {
            AddTagValue(cardOption.m_tagHolder.m_tags);
        }
    }

    public void RecordRelicChoice(in GameRelic relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        if (relicChoice != null)
        {
            AddTagValue(relicChoice.m_tagHolder.m_tags);
        }
    }

    public void RecordRelicSingleChoice(in GameRelic relicOption, bool taken)
    {
        if (taken)
        {
            AddTagValue(relicOption.m_tagHolder.m_tags);
        }
    }

    private void AddTagValue(in List<GameTag> gameTags)
    {
        for (int i = 0; i < gameTags.Count; i++)
        {
            GameTag tagType = gameTags[i];
            GameDirectorTagWeight tagWeight = GetTagWeight(tagType.m_tagType);
            if (tagType.m_tagInfluence == GameTagHolder.TagInfluence.Push)
            {
                tagWeight.curWeight += tagType.m_tagWeight;
            }
            else if (tagType.m_tagInfluence == GameTagHolder.TagInfluence.Pull)
            {
                tagWeight.curWeight -= tagType.m_tagWeight;
            }
            tagWeight.curWeight = Mathf.Clamp(tagWeight.curWeight, -tagWeightMaximums, tagWeightMaximums);
        }
    }

    public int GetTagValueFor(GameElementBase checkElement)
    {
        int tagValue = 0;

        List<GameTag> gameTags = checkElement.m_tagHolder.m_tags;
        for (int i = 0; i < gameTags.Count; i++)
        {
            if (!gameTags[i].m_isReceiver)
            {
                continue;
            }

            GameDirectorTagWeight tagWeight = GetTagWeight(gameTags[i].m_tagType);
            tagValue += tagWeight.curWeight;
        }

        tagValue = Mathf.Clamp(tagValue, -tagWeightMaximums, tagWeightMaximums);
        
        return tagValue;
    }

    public GameDirectorTagWeight GetTagWeight(GameTagHolder.TagType tagType)
    {
        if (tagWeights.Any(c => c.gameTagType == tagType))
        {
            return tagWeights.FirstOrDefault(c => c.gameTagType == tagType);
        }

        GameDirectorTagWeight tagWeight = new GameDirectorTagWeight
        {
            gameTagType = tagType
        };
        tagWeights.Add(tagWeight);

        return tagWeight;
    }
}
