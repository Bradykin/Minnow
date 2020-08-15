using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElementBase
{
    public string m_name { get; protected set; }
    public string m_desc { get; protected set; }
    public Sprite m_icon { get; protected set; }
    protected Color m_color { get; set; }

    public virtual UITooltipController InitTooltip()
    {
        UITooltipController tooltipController = UITooltipController.m_instance;
        tooltipController.gameObject.SetActive(true);

        tooltipController.m_titleText.text = m_name;
        tooltipController.m_descText.text = m_desc;

        tooltipController.m_titleBackground.color = Color.gray;
        tooltipController.m_descBackground.color = Color.gray;

        return tooltipController;
    }

    public virtual void ClearTooltip()
    {
        UITooltipController tooltipController = UITooltipController.m_instance;
        tooltipController.gameObject.SetActive(false);
    }

    public virtual Color GetColor()
    {
        return m_color;
    }
}
