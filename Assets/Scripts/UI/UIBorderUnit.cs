using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.Util;

public class UIBorderUnit : MonoBehaviour, IReset
    , IPointerClickHandler
{
    RectTransform m_rectTransform;
    
    public Image m_image;
    public GameObject m_holder;

    private WorldUnit m_unit;

    public void Init(WorldUnit toTrack)
    {
        m_unit = toTrack;

        m_rectTransform = GetComponent<RectTransform>();
        m_image.sprite = toTrack.GetUnit().m_icon;
    }

    public void Activate()
    {
        
    }

    public void Reset()
    {
        Recycler.Recycle<UIBorderUnit>(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UICameraController.Instance.SmoothCameraTransitionToGameObject(m_unit.gameObject);
    }

    void Update()
    {
        if (m_unit == null || m_unit.gameObject.activeSelf == false || m_unit.GetUnit() == null || m_unit.GetUnit().m_isDead || GameHelper.GetGameController().m_runStateType == RunStateType.Intermission)
        {
            Recycler.Recycle<UIBorderUnit>(this);
            return;
        }

        //Hide this if the WorldUnit is on screen.
        m_holder.SetActive(!m_unit.m_renderer.isVisible && m_unit.GetUnit().GetCurStamina() > 0 && GameHelper.IsPlayerTurn());

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 unitWorldToScreen = Camera.main.WorldToScreenPoint(m_unit.transform.position);
        Vector2 clampedUnitWorldToScreen = new Vector2(Mathf.Clamp(unitWorldToScreen.x, m_rectTransform.rect.width / 2, UIHUDController.Instance.m_rectTransform.rect.width - (m_rectTransform.rect.width / 2)), 
                                                       Mathf.Clamp(unitWorldToScreen.y, m_rectTransform.rect.height / 2, UIHUDController.Instance.m_rectTransform.rect.height - (m_rectTransform.rect.height / 2)));
        m_rectTransform.anchoredPosition = clampedUnitWorldToScreen;
    }
}
