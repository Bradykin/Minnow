using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDuplicateSpellIntermissionAction : GameActionIntermission
{
    public ContentDuplicateSpellIntermissionAction()
    {
        m_actionCost = 4;
        m_name = "Duplicate Spell";
        m_desc = "Duplicate a spell card in your deck!";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate()
    {
        UIDeckViewController.Instance.Init(GameHelper.GetPlayerBaseDeckOfSpells(), UIDeckViewController.DeckViewType.Duplicate, "Duplicate a Spell");

        SpendCost();
    }
}