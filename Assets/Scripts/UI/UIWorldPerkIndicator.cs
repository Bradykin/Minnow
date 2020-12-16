using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIWorldPerkIndicator : UIElementBase
    , IPointerClickHandler
{
    private GameWorldPerk m_gameWorldPerk;

    private WorldTile m_ownerTile;

    public void Init(GameWorldPerk worldPerk, WorldTile ownerTile)
    {
        m_gameWorldPerk = worldPerk;
        m_ownerTile = ownerTile;

        if (m_gameWorldPerk != null)
        {
            gameObject.GetComponent<Image>().sprite = m_gameWorldPerk.GetIcon();

            m_tintImage.sprite = m_gameWorldPerk.GetWIcon();
            m_tintImage.gameObject.SetActive(true);
        }
    }

    public override void HandleTooltip()
    {
        if (m_gameWorldPerk != null)
        {
            UITooltipController.Instance.ClearTooltipStack();

            UIHelper.CreateWorldPerkTooltip(m_gameWorldPerk);

            m_isShowingTooltip = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_ownerTile.GetGameTile().IsOccupied())
        {
            m_ownerTile.GetGameTile().GetOccupyingUnit().m_worldUnit.OnMouseDownExt();
        }
        else
        {
            m_ownerTile.OnMouseDownExt();
        }
    }
}
