using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIEndTurnButton : UIElementBase
    , IPointerClickHandler
{
    public Image m_image;
    public Text m_endTurnText;

    void Start()
    {
        m_stopScrolling = true;
    }

    void Update()
    {
        if (PlayerHasActions())
        {
            m_image.color = UIHelper.m_fadedColor;
            m_endTurnText.color = UIHelper.m_fadedColor;
        }
        else
        {
            m_image.color = UIHelper.m_defaultColor;
            m_endTurnText.color = UIHelper.m_defaultColor;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            EndTurn();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EndTurn();
    }

    private void EndTurn()
    {
        if (!Globals.m_canSelect)
        {
            return;
        }

        if (!WorldGridManager.Instance.IsSetup())
        {
            return;
        }

        UITooltipController.Instance.ClearTooltipStack();

        Globals.m_selectedCard = null;
        UIHelper.UnselectEntity();
        UIHelper.UnselectEnemy();
        Globals.m_selectedTile = null;

        WorldController.Instance.MoveToNextTurn();

        m_tintImage.color = UIHelper.GetDefaultTintColor();
    }

    private bool PlayerHasActions()
    {
        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return false;
        }

        return player.HasEntitiesThatWillOvercapAP() || player.CanPlayAnythingInHand();
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("End Turn", "<b>Hotkey: Space</b>\n\nThis will refresh your energy and regen some AP for your units.  You will also discard your hand a draw a new one.  Your enemies will all take their turns.", !PlayerHasActions()));
    }
}
