using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

public class UIEndTurnButton : UIElementBase
    , IPointerClickHandler
{
    public Image m_image;
    public TMP_Text m_endTurnText;

    void Start()
    {
        m_stopScrolling = true;
    }

    void Update()
    {
        if (UIHelper.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

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

        if (GameHelper.IsOpponentsTurn())
        {
            return;
        }

        if (WorldController.Instance.m_isEndTurnLocked)
        {
            return;
        }

        if (GameHelper.GetPlayer().m_controlledUnits.Any(u => u.m_worldUnit.IsMoving))
        {
            return;
        }

        if (!HasActionsRemaining())
        {
            FinalizeEndTurn();
        }
        else
        {
            UIConfirmationController.Instance.Init("Are you sure you want to end the turn (actions remain)?", FinalizeEndTurn);
        }
    }

    private void FinalizeEndTurn()
    {
        UITooltipController.Instance.ClearTooltipStack();

        Globals.m_selectedCard = null;
        UIHelper.UnselectUnit();
        UIHelper.UnselectEnemy();
        Globals.m_selectedTile = null;
        WorldController.Instance.m_isEndTurnLocked = true;

        WorldController.Instance.MoveToNextTurn();

        m_tintImage.color = UIHelper.GetDefaultTintColor();
    }

    private bool HasActionsRemaining()
    {
        GamePlayer player = GameHelper.GetPlayer();

        for (int i = 0; i < player.m_controlledUnits.Count; i++)
        {
            GameUnit unit = player.m_controlledUnits[i];
            if (unit.GetCurStamina() + unit.GetStaminaRegen() > unit.GetMaxStamina())
            {
                if (!unit.GetGameTile().HasBuilding())
                {
                    if (unit.GetCurStamina() != unit.GetMaxStamina())
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("End Turn (Space)", "Refresh energy and regen allied units' stamina. Discard your hand and draw a new one."));
    }
}
