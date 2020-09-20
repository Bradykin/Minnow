using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBeginWaveButton : WorldElementBase
{
    public Image m_image;
    public Text m_beginWaveText;
    public Image m_tintImage;
    public GameObject m_holder;

    void Update()
    {
        if (PlayerHasActions())
        {
            m_image.color = UIHelper.m_fadedColor;
            m_beginWaveText.color = UIHelper.m_fadedColor;
        }
        else
        {
            m_image.color = UIHelper.m_defaultColor;
            m_beginWaveText.color = UIHelper.m_defaultColor;
        }
    }

    void OnMouseDown()
    {
        BeginWave();
    }

    private void BeginWave()
    {
        if (!Globals.m_canSelect)
        {
            return;
        }

        m_tintImage.color = UIHelper.GetDefaultTintColor();

        WorldController.Instance.EndIntermission();
    }

    void OnMouseOver()
    {
        m_tintImage.color = UIHelper.GetValidTintColor(true);
        Globals.m_canScroll = false;
    }

    void OnMouseExit()
    {
        m_tintImage.color = UIHelper.GetDefaultTintColor();
        Globals.m_canScroll = true;
    }

    private bool PlayerHasActions()
    {
        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return false;
        }

        return player.GetCurActions() > 0;
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Begin Wave", "Start the next wave of enemies!", !PlayerHasActions()));
    }
}
