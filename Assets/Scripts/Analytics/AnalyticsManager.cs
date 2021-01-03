using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Game.Util;

public class AnalyticsManager
{
    public void RecordCardChoice(in GameCard cardChoice, in GameCard optionOne, in GameCard optionTwo, in GameCard optionThree)
    {
        /*WWWForm form = new WWWForm();
        form.AddField("Name", cardChoice.GetName());
        form.AddField("AppearRate", cardChoice.GetName());
        form.AddField("PickRate", cardChoice.GetName());
        form.AddField("WinRate", cardChoice.GetName());
        UnityWebRequest www = UnityWebRequest.Post("http://nmartino.com/gamescripts/citadel/CitadelCardDataServer.php", form);

        FactoryManager.Instance.StartCoroutine(UploadData(www));*/
    }

    public void RecordCardSingleChoice(in GameCard cardOption, bool taken)
    {

    }

    public void RecordCardDuplication(in GameCard cardDuplicated)
    {

    }

    public void RecordCardTransformation(in GameCard cardTransformed, in GameCard cardReceived)
    {

    }

    public void RecordCardRemoval(in GameCard cardRemoved)
    {

    }

    public void RecordRelicChoice(in GameRelic relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        
    }

    public void RecordRelicSingleChoice(in GameRelic relicOption, bool taken)
    {

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
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete");
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
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
        }
    }
}
