using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapRewardController : MonoBehaviour
{
    public Image m_rewardImage;

    void Update()
    {
        Sprite rewardImageSprite = GameMetaprogressionUnlocksDataManager.GetIconForMapAndChaos(UILevelSelectController.Instance.m_curMap.m_id, Globals.m_curChaos);

        if (rewardImageSprite == null)
        {
            m_rewardImage.gameObject.SetActive(false);
        }
        else
        {
            m_rewardImage.gameObject.SetActive(true);
            m_rewardImage.sprite = rewardImageSprite;
        }
    }
}
