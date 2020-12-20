using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SerializeField]
public class GameDirectorRun
{
    public List<GameDirectorTagWeight> tagWeights = new List<GameDirectorTagWeight>();

    public void RecordCardChoice(in GameCard cardChoice, in GameCard optionOne, in GameCard optionTwo, in GameCard optionThree)
    {
        if (cardChoice != null)
        {
            for (int i = 0; i < cardChoice.m_tagHolder.m_tags.Count; i++)
            {
                GameTag tagType = cardChoice.m_tagHolder.m_tags[i];
                GameDirectorTagWeight tagWeight = GetTagWeight(tagType);
            }
        }
    }

    public void RecordRelicChoice(in GameRelic relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        if (relicChoice != null)
        {

        }
    }

    public GameDirectorTagWeight GetTagWeight(GameTag tag)
    {
        if (tagWeights.Any(c => c.gameTagType == tag.m_tagType))
        {
            return tagWeights.FirstOrDefault(c => c.gameTagType == tag.m_tagType);
        }

        GameDirectorTagWeight tagWeight = new GameDirectorTagWeight
        {
            gameTagType = tag.m_tagType
        };
        tagWeights.Add(tagWeight);

        return tagWeight;
    }
}
