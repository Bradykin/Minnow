using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIBeginWaveButton : UIElementBase
    , IPointerClickHandler
{
    public Image m_image;
    public Text m_beginWaveText;
    public GameObject m_holder;

    void Start()
    {
        m_stopScrolling = true;
    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        if (GameHelper.GetPlayer().GetCurActions() > 0)
        {
            return;
        }

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
        if (PlayerHasActions())
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Begin Wave", "Need to spend all actions before starting the next wave.", false));
        }
        else
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Begin Wave", "Start the next wave of enemies!", true));
        }
    }
}
