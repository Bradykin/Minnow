using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Game.Util;

public class AnalyticsManager
{
    public void RecordCardChoice(in GameCard cardChoice, in GameCard optionOne, in GameCard optionTwo, in GameCard optionThree)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        WWWForm pickForm = new WWWForm();
        pickForm.AddField("Name", cardChoice.GetBaseName());
        UnityWebRequest pickWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataPick.php", pickForm);

        FactoryManager.Instance.StartCoroutine(UploadData(pickWWW));

        WWWForm seeForm1 = new WWWForm();
        seeForm1.AddField("Name", optionOne.GetBaseName());
        UnityWebRequest seeWWW1 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataSee.php", seeForm1);

        FactoryManager.Instance.StartCoroutine(UploadData(seeWWW1));

        WWWForm seeForm2 = new WWWForm();
        seeForm2.AddField("Name", optionTwo.GetBaseName());
        UnityWebRequest seeWWW2 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataSee.php", seeForm2);

        FactoryManager.Instance.StartCoroutine(UploadData(seeWWW2));

        WWWForm seeForm3 = new WWWForm();
        seeForm3.AddField("Name", optionThree.GetBaseName());
        UnityWebRequest seeWWW3 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataSee.php", seeForm3);

        FactoryManager.Instance.StartCoroutine(UploadData(seeWWW3));
    }

    public void RecordCardSingleChoice(in GameCard cardOption, bool taken)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        if (taken)
        {
            WWWForm pickForm = new WWWForm();
            pickForm.AddField("Name", cardOption.GetBaseName());
            UnityWebRequest pickWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataPick.php", pickForm);

            FactoryManager.Instance.StartCoroutine(UploadData(pickWWW));
        }

        WWWForm seeForm = new WWWForm();
        seeForm.AddField("Name", cardOption.GetBaseName());
        UnityWebRequest seeWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataSee.php", seeForm);

        FactoryManager.Instance.StartCoroutine(UploadData(seeWWW));
    }

    public void RecordCardStarter(in GameCard cardStarter)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        WWWForm pickForm = new WWWForm();
        pickForm.AddField("Name", cardStarter.GetBaseName());
        UnityWebRequest pickWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataPick.php", pickForm);

        FactoryManager.Instance.StartCoroutine(UploadData(pickWWW));

        WWWForm seeForm = new WWWForm();
        seeForm.AddField("Name", cardStarter.GetBaseName());
        UnityWebRequest seeWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataSee.php", seeForm);

        FactoryManager.Instance.StartCoroutine(UploadData(seeWWW));
    }

    public void RecordCardChaosGiven(in GameCard chaosCard)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        WWWForm chaosForm = new WWWForm();
        chaosForm.AddField("Name", chaosCard.GetBaseName());
        UnityWebRequest chaosWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataChaos.php", chaosForm);

        FactoryManager.Instance.StartCoroutine(UploadData(chaosWWW));
    }

    public void RecordCardDuplication(in GameCard cardDuplicated)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        WWWForm dupForm = new WWWForm();
        dupForm.AddField("Name", cardDuplicated.GetBaseName());
        UnityWebRequest dupWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataDup.php", dupForm);

        FactoryManager.Instance.StartCoroutine(UploadData(dupWWW));
    }

    public void RecordCardTransformation(in GameCard cardTransformed, in GameCard cardReceived)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        WWWForm remTransForm = new WWWForm();
        remTransForm.AddField("Name", cardTransformed.GetBaseName());
        UnityWebRequest remTransWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataTransRem.php", remTransForm);

        FactoryManager.Instance.StartCoroutine(UploadData(remTransWWW));

        WWWForm addTransForm = new WWWForm();
        addTransForm.AddField("Name", cardReceived.GetBaseName());
        UnityWebRequest addTransWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataTransAdd.php", addTransForm);

        FactoryManager.Instance.StartCoroutine(UploadData(addTransWWW));
    }

    public void RecordCardRemoval(in GameCard cardRemoved)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        WWWForm remForm = new WWWForm();
        remForm.AddField("Name", cardRemoved.GetBaseName());
        UnityWebRequest remWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataRem.php", remForm);

        FactoryManager.Instance.StartCoroutine(UploadData(remWWW));
    }

    public void RecordRelicChoice(in GameRelic relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        WWWForm pickForm = new WWWForm();
        pickForm.AddField("Name", relicChoice.GetBaseName());
        UnityWebRequest pickWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelRelicDataPick.php", pickForm);

        FactoryManager.Instance.StartCoroutine(UploadData(pickWWW));

        WWWForm seeForm1 = new WWWForm();
        seeForm1.AddField("Name", optionOne.GetBaseName());
        UnityWebRequest seeWWW1 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelRelicDataSee.php", seeForm1);

        FactoryManager.Instance.StartCoroutine(UploadData(seeWWW1));

        WWWForm seeForm2 = new WWWForm();
        seeForm2.AddField("Name", optionTwo.GetBaseName());
        UnityWebRequest seeWWW2 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelRelicDataSee.php", seeForm2);

        FactoryManager.Instance.StartCoroutine(UploadData(seeWWW2));
    }

    public void RecordRelicSingleChoice(in GameRelic relicOption, bool taken)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        if (taken)
        {
            WWWForm pickForm = new WWWForm();
            pickForm.AddField("Name", relicOption.GetBaseName());
            UnityWebRequest pickWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelRelicDataPick.php", pickForm);

            FactoryManager.Instance.StartCoroutine(UploadData(pickWWW));
        }

        WWWForm seeForm = new WWWForm();
        seeForm.AddField("Name", relicOption.GetBaseName());
        UnityWebRequest seeWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelRelicDataSee.php", seeForm);

        FactoryManager.Instance.StartCoroutine(UploadData(seeWWW));
    }

    public void RecordRelicStarter(in GameRelic relicStarter)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        WWWForm pickForm = new WWWForm();
        pickForm.AddField("Name", relicStarter.GetBaseName());
        UnityWebRequest pickWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelRelicDataPick.php", pickForm);

        FactoryManager.Instance.StartCoroutine(UploadData(pickWWW));

        WWWForm seeForm = new WWWForm();
        seeForm.AddField("Name", relicStarter.GetBaseName());
        UnityWebRequest seeWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelRelicDataSee.php", seeForm);

        FactoryManager.Instance.StartCoroutine(UploadData(seeWWW));
    }

    public void EndLevel(in RunEndType endType)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        GamePlayer player = GameHelper.GetPlayer();
        string mapNameChaos = $"{GameHelper.GetGameController().GetCurMap().GetBaseName()} - Chaos {Globals.m_curChaos}";

        List<GameRelic> relicList = player.GetRelics().GetRelicListForRead();

        for (int i = 0; i < relicList.Count; i++)
        {
            if (relicList[i].m_rarity == GameElementBase.GameRarity.Common)
            {
                WWWForm relicMapForm = new WWWForm();
                relicMapForm.AddField("Name", mapNameChaos);
                UnityWebRequest relicMapWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataRelicCommon.php", relicMapForm);

                FactoryManager.Instance.StartCoroutine(UploadData(relicMapWWW));
            }
            else if (relicList[i].m_rarity == GameElementBase.GameRarity.Uncommon)
            {
                WWWForm relicMapForm = new WWWForm();
                relicMapForm.AddField("Name", mapNameChaos);
                UnityWebRequest relicMapWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataRelicUncommon.php", relicMapForm);

                FactoryManager.Instance.StartCoroutine(UploadData(relicMapWWW));
            }
            else if (relicList[i].m_rarity == GameElementBase.GameRarity.Rare)
            {
                WWWForm relicMapForm = new WWWForm();
                relicMapForm.AddField("Name", mapNameChaos);
                UnityWebRequest relicMapWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataRelicRare.php", relicMapForm);

                FactoryManager.Instance.StartCoroutine(UploadData(relicMapWWW));
            }
        }

        if (endType == RunEndType.Win)
        {
            for (int i = 0; i < player.m_deckBase.Count(); i++)
            {
                WWWForm cardWinForm = new WWWForm();
                cardWinForm.AddField("Name", player.m_deckBase.GetCardByIndex(i).GetBaseName());
                UnityWebRequest cardWinWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataWin.php", cardWinForm);

                FactoryManager.Instance.StartCoroutine(UploadData(cardWinWWW));
            }

            for (int i = 0; i < player.GetRelics().GetSize(); i++)
            {
                WWWForm relicWinForm = new WWWForm();
                relicWinForm.AddField("Name", player.GetRelics().GetRelicListForRead()[i].GetBaseName());
                UnityWebRequest relicWinWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelRelicDataWin.php", relicWinForm);

                FactoryManager.Instance.StartCoroutine(UploadData(relicWinWWW));
            }

            WWWForm winForm = new WWWForm();
            winForm.AddField("Name", mapNameChaos);
            UnityWebRequest winWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataWin.php", winForm);

            FactoryManager.Instance.StartCoroutine(UploadData(winWWW));

            WWWForm winTimeForm = new WWWForm();
            winTimeForm.AddField("Name", mapNameChaos);
            winTimeForm.AddField("Time", ""+Timer.Instance.GetLevelTime());
            UnityWebRequest winTimeWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataWinTime.php", winTimeForm);

            FactoryManager.Instance.StartCoroutine(UploadData(winTimeWWW));

            if (player.m_numEventsTriggered > 0)
            {
                WWWForm winEventForm = new WWWForm();
                winEventForm.AddField("Name", mapNameChaos);
                winEventForm.AddField("Events", player.m_numEventsTriggered);
                UnityWebRequest winEventWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataEventWin.php", winEventForm);

                FactoryManager.Instance.StartCoroutine(UploadData(winEventWWW));
            }

            if (player.m_obtainedAltarName != null && player.m_obtainedAltarName != string.Empty)
            {
                if (player.m_obtainedAltarName == new ContentDorphinAltar(null).GetName())
                {
                    WWWForm winAltarForm = new WWWForm();
                    winAltarForm.AddField("Name", mapNameChaos);
                    UnityWebRequest winAltarWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataAltarWinDorphin.php", winAltarForm);

                    FactoryManager.Instance.StartCoroutine(UploadData(winAltarWWW));
                }
                else if (player.m_obtainedAltarName == new ContentMonAltar(null).GetName())
                {
                    WWWForm winAltarForm = new WWWForm();
                    winAltarForm.AddField("Name", mapNameChaos);
                    UnityWebRequest winAltarWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataAltarWinMon.php", winAltarForm);

                    FactoryManager.Instance.StartCoroutine(UploadData(winAltarWWW));
                }
                else if (player.m_obtainedAltarName == new ContentSugoAltar(null).GetName())
                {
                    WWWForm winAltarForm = new WWWForm();
                    winAltarForm.AddField("Name", mapNameChaos);
                    UnityWebRequest winAltarWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataAltarWinSugo.php", winAltarForm);

                    FactoryManager.Instance.StartCoroutine(UploadData(winAltarWWW));
                }
                else if (player.m_obtainedAltarName == new ContentTelloAltar(null).GetName())
                {
                    WWWForm winAltarForm = new WWWForm();
                    winAltarForm.AddField("Name", mapNameChaos);
                    UnityWebRequest winAltarWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataAltarWinTello.php", winAltarForm);

                    FactoryManager.Instance.StartCoroutine(UploadData(winAltarWWW));
                }
            }
        }
        else if (endType == RunEndType.Loss)
        {
            int endWave = GameHelper.GetCurrentWaveNum();

            WWWForm lossForm1 = new WWWForm();
            lossForm1.AddField("Name", mapNameChaos);
            UnityWebRequest lossWWW = UnityWebRequest.Post($"http://nmartino.com/gamescripts/citadel/CitadelMapDataLoss{endWave}.php", lossForm1);

            FactoryManager.Instance.StartCoroutine(UploadData(lossWWW));

            WWWForm lossTimeForm = new WWWForm();
            lossTimeForm.AddField("Name", mapNameChaos);
            lossTimeForm.AddField("Time", "" + Timer.Instance.GetLevelTime());
            UnityWebRequest lossTimeWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataLossTime.php", lossTimeForm);

            FactoryManager.Instance.StartCoroutine(UploadData(lossTimeWWW));
        }
        else if (endType == RunEndType.Quit)
        {
            int endWave = GameHelper.GetCurrentWaveNum();

            WWWForm quitForm = new WWWForm();
            quitForm.AddField("Name", mapNameChaos);
            UnityWebRequest quitWWW = UnityWebRequest.Post($"http://nmartino.com/gamescripts/citadel/CitadelMapDataQuit{endWave}.php", quitForm);

            FactoryManager.Instance.StartCoroutine(UploadData(quitWWW));

            WWWForm quitTimeForm = new WWWForm();
            quitTimeForm.AddField("Name", mapNameChaos);
            quitTimeForm.AddField("Time", "" + Timer.Instance.GetLevelTime());
            UnityWebRequest quitTimeWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataQuitTime.php", quitTimeForm);

            FactoryManager.Instance.StartCoroutine(UploadData(quitTimeWWW));
        }
    }

    public void RecordEventInteracted(GameEvent gameEvent, bool isFirstOption)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        if (isFirstOption)
        {
            WWWForm pickForm = new WWWForm();
            pickForm.AddField("Name", gameEvent.GetBaseName());
            UnityWebRequest pickWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelEventFirstPick.php", pickForm);

            FactoryManager.Instance.StartCoroutine(UploadData(pickWWW));
        }
        else
        {
            WWWForm pickForm = new WWWForm();
            pickForm.AddField("Name", gameEvent.GetBaseName());
            UnityWebRequest pickWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelEventSecondPick.php", pickForm);

            FactoryManager.Instance.StartCoroutine(UploadData(pickWWW));
        }
    }

    public void RecordGainGold(in int goldValue)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        string mapNameChaos = $"{GameHelper.GetGameController().GetCurMap().GetBaseName()} - Chaos {Globals.m_curChaos}";
        int wave = GameHelper.GetCurrentWaveNum();

        WWWForm goldForm = new WWWForm();
        goldForm.AddField("Name", mapNameChaos);
        goldForm.AddField("Gold", goldValue);
        UnityWebRequest goldWWW = UnityWebRequest.Post($"http://nmartino.com/gamescripts/citadel/CitadelMapDataGold{wave}.php", goldForm);

        FactoryManager.Instance.StartCoroutine(UploadData(goldWWW));
    }

    public void ShowCardData()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://nmartino.com/gamescripts/citadel/CitadelCardDataGet.php");

        FactoryManager.Instance.StartCoroutine(GetData(www));
    }

    IEnumerator UploadData(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Analytics Error: " + www.error);
        }
        else
        {
            //Debug.Log("Form upload complete");
        }
    }

    IEnumerator GetData(UnityWebRequest www)
    {
        var asyncOperation = www.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        while (!www.isDone)
        {
            yield return null;
        }

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Analytics Error: " + www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
        }
    }
}
