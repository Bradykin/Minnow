using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoblinLegendCard : GameUnitCard
{
    public ContentGoblinLegendCard()
    {
        m_unit = new ContentGoblinLegend();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Midrange);
    }
}
