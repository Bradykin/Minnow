using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBeginWaveButton : WorldElementBase
{
    public SpriteRenderer m_renderer;
    public Text m_beginWaveText;
    public SpriteRenderer m_tintRenderer;
    public GameObject m_holder;

    void Update()
    {
        if (PlayerHasActions())
        {
            m_renderer.color = UIHelper.m_fadedColor;
            m_beginWaveText.color = UIHelper.m_fadedColor;
        }
        else
        {
            m_renderer.color = UIHelper.m_defaultColor;
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

        UIHelper.SetDefaultTintColor(m_tintRenderer);

        WorldController.Instance.EndIntermission();
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
        Globals.m_canScroll = false;
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
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
