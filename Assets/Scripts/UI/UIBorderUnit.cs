using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIBorderUnit : MonoBehaviour
    , IPointerClickHandler
{
    public Image m_image;
    public GameObject m_holder;

    private UIEntity m_entity;

    public void Init(UIEntity toTrack)
    {
        m_entity = toTrack;

        m_image.sprite = toTrack.GetEntity().m_icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UICameraController.Instance.SmoothCameraTransitionToGameObject(m_entity.gameObject);
    }

    void Update()
    {
        //Hide this if the UIEntity is on screen.
        m_holder.SetActive(!m_entity.m_renderer.isVisible && m_entity.GetEntity().GetCurAP() > 0);

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 pos = Camera.main.transform.position;
        Vector3 dir = (m_entity.transform.position - Camera.main.transform.position);
        Vector3 normalizedDir = new Vector3(dir.x, dir.y, transform.position.z).normalized;

        float leftBorder = 0.0f;
        float rightBorder = Screen.width;
        float topBorder = 0.0f;
        float botBorder = Screen.height;

        Vector3 screenPointPos = Camera.main.ViewportToScreenPoint(new Vector3(Screen.width * normalizedDir.x, Screen.height * normalizedDir.y, normalizedDir.z));
        screenPointPos.z = 0;
        gameObject.transform.position = screenPointPos;

        /*print("m_entiy World Pos: " + m_entity.transform.position);
        print("m_entiy Screen Pos: " + Camera.main.WorldToScreenPoint(m_entity.transform.position));
        print("m_entiy Viewport Pos: " + Camera.main.WorldToViewportPoint(m_entity.transform.position));

        Vector3 entityScreenPos = Camera.main.WorldToScreenPoint(m_entity.transform.position);
        float dist = (Camera.main.WorldToScreenPoint(m_entity.transform.position) - Camera.main.transform.position).z;
        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float botBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        gameObject.transform.position = new Vector3(Mathf.Clamp(entityScreenPos.x, leftBorder, rightBorder), Mathf.Clamp(entityScreenPos.y, topBorder, botBorder), entityScreenPos.z);*/
    }
}

/*
 *     private void UpdatePosition()
    {
        float leftBorder = 0.0f;
        float rightBorder = Screen.width;
        float topBorder = 0.0f;
        float botBorder = Screen.height;

        Vector3 uiEntityScreenPos = Camera.main.WorldToScreenPoint(m_entity.gameObject.transform.position);

        gameObject.transform.position = new Vector3(Mathf.Clamp(uiEntityScreenPos.x, leftBorder, rightBorder), Mathf.Clamp(uiEntityScreenPos.y, topBorder, botBorder), gameObject.transform.position.z);
    }
 */
