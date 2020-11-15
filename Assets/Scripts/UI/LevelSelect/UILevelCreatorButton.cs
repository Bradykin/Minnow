using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UILevelCreatorButton : UIElementBase
    , IPointerClickHandler
{
    void Start()
    {
        gameObject.SetActive(Constants.DevMode);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        SceneLoader.ActivateScene("LevelCreatorScene", "LevelSelectScene");
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
