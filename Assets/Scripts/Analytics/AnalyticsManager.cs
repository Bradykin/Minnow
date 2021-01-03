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
        pickForm.AddField("Name", cardChoice.GetName());
        UnityWebRequest pickWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataPick.php", pickForm);

        FactoryManager.Instance.StartCoroutine(UploadData(pickWWW));

        WWWForm seeForm1 = new WWWForm();
        seeForm1.AddField("Name", optionOne.GetName());
        UnityWebRequest seeWWW1 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataSee.php", seeForm1);

        FactoryManager.Instance.StartCoroutine(UploadData(seeWWW1));

        WWWForm seeForm2 = new WWWForm();
        seeForm2.AddField("Name", optionTwo.GetName());
        UnityWebRequest seeWWW2 = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataSee.php", seeForm2);

        FactoryManager.Instance.StartCoroutine(UploadData(seeWWW2));

        WWWForm seeForm3 = new WWWForm();
        seeForm3.AddField("Name", optionThree.GetName());
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
            pickForm.AddField("Name", cardOption.GetName());
            UnityWebRequest pickWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataPick.php", pickForm);

            FactoryManager.Instance.StartCoroutine(UploadData(pickWWW));
        }

        WWWForm seeForm = new WWWForm();
        seeForm.AddField("Name", cardOption.GetName());
        UnityWebRequest seeWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataSee.php", seeForm);

        FactoryManager.Instance.StartCoroutine(UploadData(seeWWW));
    }

    public static void RecordCardStarter(in GameCard cardStarter)
    {
        Debug.Log("STARTER CARD: " + cardStarter.GetName());
    }

    public void RecordCardChaosGiven(in GameCard chaosCard)
    {
        WWWForm chaosForm = new WWWForm();
        chaosForm.AddField("Name", chaosCard.GetName());
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
        dupForm.AddField("Name", cardDuplicated.GetName());
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
        remTransForm.AddField("Name", cardTransformed.GetName());
        UnityWebRequest remTransWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataTransRem.php", remTransForm);

        FactoryManager.Instance.StartCoroutine(UploadData(remTransWWW));

        WWWForm addTransForm = new WWWForm();
        addTransForm.AddField("Name", cardReceived.GetName());
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
        remForm.AddField("Name", cardRemoved.GetName());
        UnityWebRequest remWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataRem.php", remForm);

        FactoryManager.Instance.StartCoroutine(UploadData(remWWW));
    }

    public void RecordRelicChoice(in GameRelic relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }
    }

    public void RecordRelicSingleChoice(in GameRelic relicOption, bool taken)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }
    }

    public void EndLevel(in RunEndType endType, in GameDeck deck)
    {
        if (!Constants.AnalyticsOn)
        {
            return;
        }

        if (endType == RunEndType.Win)
        {
            for (int i = 0; i < deck.Count(); i++)
            {
                WWWForm winForm = new WWWForm();
                winForm.AddField("Name", deck.GetCardByIndex(i).GetName());
                UnityWebRequest winWWW = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataWin.php", winForm);

                FactoryManager.Instance.StartCoroutine(UploadData(winWWW));
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
