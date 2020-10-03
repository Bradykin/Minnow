using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIBorderUnit : MonoBehaviour
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

    public void OnPointerClick(PointerEventData eventData)
    {
        UICameraController.Instance.SmoothCameraTransitionToGameObject(m_unit.gameObject);
    }

    void Update()
    {
        //Hide this if the WorldUnit is on screen.
        m_holder.SetActive(!m_unit.m_renderer.isVisible && m_unit.GetUnit().GetCurStamina() > 0);

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        /*Vector3 pos = Camera.main.transform.position;
        Vector3 dir = (m_unit.transform.position - Camera.main.transform.position);
        Vector3 normalizedDir = new Vector3(dir.x, dir.y, transform.position.z).normalized;

        float leftBorder = 0.0f;
        float rightBorder = Screen.width;
        float topBorder = 0.0f;
        float botBorder = Screen.height;

        Vector3 screenPointPos = Camera.main.ViewportToScreenPoint(new Vector3(Screen.width * normalizedDir.x, Screen.height * normalizedDir.y, normalizedDir.z));
        screenPointPos.z = 0;
        gameObject.GetComponent<RectTransform>().position = screenPointPos;

        m_rectTransform.anchoredPosition = new Vector2(Screen.width / 4, Screen.height / 4);

        /*print("m_entiy World Pos: " + m_unit.transform.position);
        print("m_entiy Screen Pos: " + Camera.main.WorldToScreenPoint(m_unit.transform.position));
        print("m_entiy Viewport Pos: " + Camera.main.WorldToViewportPoint(m_unit.transform.position));

        Vector3 unitScreenPos = Camera.main.WorldToScreenPoint(m_unit.transform.position);
        float dist = (Camera.main.WorldToScreenPoint(m_unit.transform.position) - Camera.main.transform.position).z;
        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float botBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        gameObject.transform.position = new Vector3(Mathf.Clamp(unitScreenPos.x, leftBorder, rightBorder), Mathf.Clamp(unitScreenPos.y, topBorder, botBorder), unitScreenPos.z);*/
    }
}

/*
 *     private void UpdatePosition()
    {
        float leftBorder = 0.0f;
        float rightBorder = Screen.width;
        float topBorder = 0.0f;
        float botBorder = Screen.height;

        Vector3 worldUnitScreenPos = Camera.main.WorldToScreenPoint(m_unit.gameObject.transform.position);

        gameObject.transform.position = new Vector3(Mathf.Clamp(worldUnitScreenPos.x, leftBorder, rightBorder), Mathf.Clamp(worldUnitScreenPos.y, topBorder, botBorder), gameObject.transform.position.z);
    }
 */
