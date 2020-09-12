using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSelectButton : WorldElementBase
{
    public int m_id;
    public SpriteRenderer m_tintRenderer;
    public Text m_titleText;

    private JsonMapMetaData m_level;

    void Start()
    {
        List<JsonMapMetaData> mapList = Globals.LoadMapMetaData();
        for (int i = 0; i < mapList.Count; i++)
        {
            if (mapList[i].mapID == m_id)
            {
                m_level = mapList[i];
                break;
            }
        }
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
        if (m_id == -1)
        {
            UILevelSelectController.Instance.m_levelBuilderSelected = true;
            return;
        }
        else
        {
            UILevelSelectController.Instance.m_levelBuilderSelected = false;
            UILevelSelectController.Instance.SetSelectedLevel(m_level);
        }
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
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(m_level.mapName, UIHelper.GetDifficultyText(((MapDifficulty)m_level.mapDifficulty))));
    }
}
