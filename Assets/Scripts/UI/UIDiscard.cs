using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDiscard : MonoBehaviour
{
    public Text m_countText;

    public SpriteRenderer m_tintRenderer;

    void Update()
    {
        GameDeck deck = WorldController.Instance.m_gameController.m_player.m_curDeck;
        m_countText.text = deck.DiscardCount() + "";
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
