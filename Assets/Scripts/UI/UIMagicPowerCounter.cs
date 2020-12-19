using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIMagicPowerCounter : UIElementBase
{
    public TMP_Text m_countText;
    public GameObject m_holder;

    void Start()
    {
        m_stopScrolling = true;
    }

    void Update()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        if (player.GetMagicPower() == 0)
        {
            m_holder.SetActive(false);
            return;
        }
        else
        {
            m_holder.SetActive(true);
        }

        m_countText.text = $"{player.GetMagicPower()}";
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("<b>Magic Power</b>", "This is your current <b>Magic Power</b>! This affects the power of various spells you cast."));
    }
}
