using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSelectButton : WorldElementBase
{
    public int m_id;
    public SpriteRenderer m_tintRenderer;
    public Text m_titleText;

    //private Level level;

    void Start()
    {
        
    }

    void Update()
    {
        m_titleText.text = "Level Title";
    }

    void OnMouseDown()
    {
        SelectLevel();
    }

    private void SelectLevel()
    {
        UILevelSelectController.Instance.SetSelectedLevel();
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Level Name", "Level difficulty."));
    }
}
