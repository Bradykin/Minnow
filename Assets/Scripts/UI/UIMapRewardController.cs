using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapRewardController : MonoBehaviour
{
    public Image m_otherRewardImage;
    public UICard m_rewardCard;
    public UIRelic m_rewardRelic;

    public Text m_unlockHintText;

    void Start()
    {
        m_rewardCard.gameObject.SetActive(false);
        m_rewardRelic.gameObject.SetActive(false);
        m_otherRewardImage.gameObject.SetActive(false);
    }

    void Update()
    {
        GameMetaprogressionDataElement reward = GameMetaprogressionUnlocksDataManager.GetReward(UILevelSelectController.Instance.m_curMap.m_id);

        if (reward == null)
        {
            m_unlockHintText.gameObject.SetActive(false);
            return;
        }

        bool hasReward = false;
        if (hasReward)
        {
            m_rewardCard.gameObject.SetActive(false);
            m_rewardRelic.gameObject.SetActive(false);
            m_otherRewardImage.gameObject.SetActive(false);
            m_unlockHintText.gameObject.SetActive(false);
            return;
        }

        m_unlockHintText.gameObject.SetActive(true);

        if (reward.GetCard() != null)
        {
            m_rewardCard.gameObject.SetActive(true);
            m_rewardRelic.gameObject.SetActive(false);
            m_otherRewardImage.gameObject.SetActive(false);

            m_rewardCard.Init(reward.GetCard(), UICard.CardDisplayType.Deck);
        }
        else if (reward.GetRelic() != null)
        {
            m_rewardCard.gameObject.SetActive(false);
            m_rewardRelic.gameObject.SetActive(true);
            m_otherRewardImage.gameObject.SetActive(false);

            m_rewardRelic.Init(reward.GetRelic(), UIRelic.RelicSelectionType.View);
        }
        else
        {
            m_rewardCard.gameObject.SetActive(false);
            m_rewardRelic.gameObject.SetActive(false);
            m_otherRewardImage.gameObject.SetActive(true);

            m_otherRewardImage.sprite = reward.GetIcon();
        }
    }
}
