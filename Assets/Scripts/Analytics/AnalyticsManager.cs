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

    public void EndLevel(in RunEndType endType)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        GamePlayer player = GameHelper.GetPlayer();

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
            winForm.AddField("Name", GameHelper.GetGameController().GetCurMap().GetBaseName());
            UnityWebRequest winWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataWin.php", winForm);

            FactoryManager.Instance.StartCoroutine(UploadData(winWWW));
        }
        else if (endType == RunEndType.Loss)
        {
            int endWave = GameHelper.GetCurrentWaveNum();
            if (endWave == 1)
            {
                WWWForm lossForm1 = new WWWForm();
                lossForm1.AddField("Name", GameHelper.GetGameController().GetCurMap().GetBaseName());
                UnityWebRequest lossWWW1 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataLoss1.php", lossForm1);

                FactoryManager.Instance.StartCoroutine(UploadData(lossWWW1));
            }
            else if (endWave == 2)
            {
                WWWForm lossForm2 = new WWWForm();
                lossForm2.AddField("Name", GameHelper.GetGameController().GetCurMap().GetBaseName());
                UnityWebRequest lossWWW2 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataLoss2.php", lossForm2);

                FactoryManager.Instance.StartCoroutine(UploadData(lossWWW2));
            }
            else if (endWave == 3)
            {
                WWWForm lossForm3 = new WWWForm();
                lossForm3.AddField("Name", GameHelper.GetGameController().GetCurMap().GetBaseName());
                UnityWebRequest lossWWW3 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataLoss3.php", lossForm3);

                FactoryManager.Instance.StartCoroutine(UploadData(lossWWW3));
            }
            else if (endWave == 4)
            {
                WWWForm lossForm4 = new WWWForm();
                lossForm4.AddField("Name", GameHelper.GetGameController().GetCurMap().GetBaseName());
                UnityWebRequest lossWWW4 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataLoss4.php", lossForm4);

                FactoryManager.Instance.StartCoroutine(UploadData(lossWWW4));
            }
            else if (endWave == 5)
            {
                WWWForm lossForm5 = new WWWForm();
                lossForm5.AddField("Name", GameHelper.GetGameController().GetCurMap().GetBaseName());
                UnityWebRequest lossWWW5 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataLoss5.php", lossForm5);

                FactoryManager.Instance.StartCoroutine(UploadData(lossWWW5));
            }
            else if (endWave == 6)
            {
                WWWForm lossForm6 = new WWWForm();
                lossForm6.AddField("Name", GameHelper.GetGameController().GetCurMap().GetBaseName());
                UnityWebRequest lossWWW6 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataLoss6.php", lossForm6);

                FactoryManager.Instance.StartCoroutine(UploadData(lossWWW6));
            }
        }
        else if (endType == RunEndType.Quit)
        {
            int endWave = GameHelper.GetCurrentWaveNum();
            if (endWave == 1)
            {
                WWWForm quitForm1 = new WWWForm();
                quitForm1.AddField("Name", GameHelper.GetGameController().GetCurMap().GetBaseName());
                UnityWebRequest quitWWW1 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataQuit1.php", quitForm1);

                FactoryManager.Instance.StartCoroutine(UploadData(quitWWW1));
            }
            else if (endWave == 2)
            {
                WWWForm quitForm2 = new WWWForm();
                quitForm2.AddField("Name", GameHelper.GetGameController().GetCurMap().GetBaseName());
                UnityWebRequest quitWWW2 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataQuit2.php", quitForm2);

                FactoryManager.Instance.StartCoroutine(UploadData(quitWWW2));
            }
            else if (endWave == 3)
            {
                WWWForm quitForm3 = new WWWForm();
                quitForm3.AddField("Name", GameHelper.GetGameController().GetCurMap().GetBaseName());
                UnityWebRequest quitWWW3 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataQuit3.php", quitForm3);

                FactoryManager.Instance.StartCoroutine(UploadData(quitWWW3));
            }
            else if (endWave == 4)
            {
                WWWForm quitForm4 = new WWWForm();
                quitForm4.AddField("Name", GameHelper.GetGameController().GetCurMap().GetBaseName());
                UnityWebRequest quitWWW4 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataQuit4.php", quitForm4);

                FactoryManager.Instance.StartCoroutine(UploadData(quitWWW4));
            }
            else if (endWave == 5)
            {
                WWWForm quitForm5 = new WWWForm();
                quitForm5.AddField("Name", GameHelper.GetGameController().GetCurMap().GetBaseName());
                UnityWebRequest quitWWW5 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataQuit5.php", quitForm5);

                FactoryManager.Instance.StartCoroutine(UploadData(quitWWW5));
            }
            else if (endWave == 6)
            {
                WWWForm quitForm6 = new WWWForm();
                quitForm6.AddField("Name", GameHelper.GetGameController().GetCurMap().GetBaseName());
                UnityWebRequest quitWWW6 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelMapDataQuit6.php", quitForm6);

                FactoryManager.Instance.StartCoroutine(UploadData(quitWWW6));
            }
        }
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
