using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;
using TMPro;

public class UIIntermissionActionSelectController : Singleton<UIIntermissionActionSelectController>
{
    public GameObject m_holder;

    public UIIntermissionAction m_firstButton;
    public UIIntermissionAction m_secondButton;
    public UIIntermissionAction m_thirdButton;

    void Start()
    {
        m_holder.SetActive(false);
    }

    public void Init(GameActionIntermission actionOne, GameActionIntermission actionTwo, GameActionIntermission actionThree)
    {
        Globals.m_canSelect = false;

        m_firstButton.Init(actionOne);
        m_secondButton.Init(actionTwo);
        m_thirdButton.Init(actionThree);

        m_holder.SetActive(true);
    }

    public void EndSelection()
    {
        Globals.m_canSelect = true;
        m_holder.SetActive(false);
    }

    public void AcceptAction(GameActionIntermission action)
    {
        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        GameNotificationManager.RecordAction(action, m_firstButton.m_action, m_secondButton.m_action, m_thirdButton.m_action);

        action.Activate();

        EndSelection();
    }
}
