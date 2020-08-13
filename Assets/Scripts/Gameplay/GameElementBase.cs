using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElementBase
{
    public string m_name { get; protected set; }
    public string m_desc { get; protected set; }
    public Sprite m_icon { get; protected set; }

    public virtual void InitTooltip(UITooltipController tooltipController)
    {
        tooltipController.m_titleText.text = m_name;
        tooltipController.m_descText.text = m_desc;

        tooltipController.m_titleBackground.color = Color.gray;
        tooltipController.m_descBackground.color = Color.gray;
    }
}
