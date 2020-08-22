using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElementBase
{
    public string m_name { get; protected set; }
    public string m_desc { get; protected set; }
    public Sprite m_icon { get; protected set; }
    protected Color m_color { get; set; }

    public virtual void ClearTooltip()
    {
        UITooltipController tooltipController = UITooltipController.Instance;
        tooltipController.gameObject.SetActive(false);
    }

    public virtual Color GetColor()
    {
        return m_color;
    }
}
