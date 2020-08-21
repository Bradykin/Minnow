using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergy : MonoBehaviour
{
    public Text m_countText;

    public SpriteRenderer m_tintRenderer;

    void Update()
    {
        GamePlayer player = WorldController.Instance.m_gameController.m_player;
        m_countText.text = player.m_curEnergy + "/" + player.m_maxEnergy;
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }
}
