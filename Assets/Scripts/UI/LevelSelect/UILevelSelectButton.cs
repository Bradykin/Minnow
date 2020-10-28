using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelSelectButton : MonoBehaviour
{
    public int m_id;

    private GameMap m_map;
    private bool m_isHovered;

    public SpriteRenderer m_iconRenderer;
    public SpriteRenderer m_tintRenderer;

    void Start()
    {
        m_map = GameMapFactory.GetMapById(m_id);

        m_iconRenderer.sprite = m_map.m_icon;
    }

    void Update()
    {
        if (UILevelSelectController.Instance.m_curMap != null &&
            UILevelSelectController.Instance.m_curMap.m_id == m_map.m_id)
        {
            m_tintRenderer.color = UIHelper.GetSelectTintColor(true);
        }
        else if (m_isHovered)
        {
            m_tintRenderer.color = UIHelper.GetValidTintColor(true);
        }
        else
        {
            m_tintRenderer.color = UIHelper.GetDefaultTintColor();
        }
    }

    public GameMap GetMap()
    {
        return m_map;
    }

    private void SelectLevel()
    {
        if (UILevelSelectController.Instance.m_curMap == m_map)
        {
            UILevelSelectController.Instance.SetSelectedLevel(null);

            AudioBackgroundController.Instance.StopBackgroundMusic();
        }
        else
        {
            UILevelSelectController.Instance.SetSelectedLevel(m_map);
            UICameraController.Instance.SmoothCameraTransitionToGameObject(gameObject);

            AudioBackgroundController.Instance.StartBackgroundMusic(m_map);
        }
    }

    void OnMouseDown()
    {
        if (UIHelper.UIShouldBlockClick())
        {
            return;
        }

        SelectLevel();
    }
    void OnMouseOver()
    {
        if (UIHelper.UIShouldBlockClick())
        {
            return;
        }

        if (!m_isHovered)
        {
            AudioHelper.PlaySFX(AudioHelper.UIHover);
        }

        m_isHovered = true;
    }

    void OnMouseExit()
    {
        m_isHovered = false;
    }
}
