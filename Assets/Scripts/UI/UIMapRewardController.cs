using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapRewardController : MonoBehaviour
{
    public UICard m_rewardCard;
    public UIRelic m_rewardRelic;

    public Text m_unlockHintText;

    void Start()
    {
        m_rewardCard.gameObject.SetActive(false);
        m_rewardRelic.gameObject.SetActive(false);
        m_unlockHintText.gameObject.SetActive(false);
    }

    void Update()
    {
        GameMetaprogressionDataElement reward = GameMetaprogressionUnlocksDataManager.GetReward(UILevelSelectController.Instance.m_curMap.m_id);

        if (reward == null)
        {
            m_rewardCard.gameObject.SetActive(false);
            m_rewardRelic.gameObject.SetActive(false);
            m_unlockHintText.gameObject.SetActive(false);
            return;
        }

        bool hasReward = false;
        if (hasReward)
        {
            m_rewardCard.gameObject.SetActive(false);
            m_rewardRelic.gameObject.SetActive(false);
            m_unlockHintText.gameObject.SetActive(false);
            return;
        }

        if (reward.GetCard() != null)
        {
            m_rewardCard.gameObject.SetActive(true);
            m_rewardRelic.gameObject.SetActive(false);
            m_unlockHintText.gameObject.SetActive(true);

            m_unlockHintText.text = "Beat Chaos 2 to unlock a new starter Card option:";

            m_rewardCard.Init(reward.GetCard(), UICard.CardDisplayType.Deck);
        }
        else if (reward.GetRelic() != null)
        {
            m_rewardCard.gameObject.SetActive(false);
            m_rewardRelic.gameObject.SetActive(true);
            m_unlockHintText.gameObject.SetActive(true);

            m_unlockHintText.text = "Beat Chaos 2 to unlock a new starter Relic option:";

            m_rewardRelic.Init(reward.GetRelic(), UIRelic.RelicSelectionType.View);
        }
    }
}
