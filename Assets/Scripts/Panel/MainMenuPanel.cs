using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    public void  Start(){
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void OnPause(){
        canvasGroup.blocksRaycasts = false;
    }

    public override void OnResume(){
        canvasGroup.blocksRaycasts = true;
    }


    public void OnPushPanel(string panelName){
        UIPanelType uIPanelType = (UIPanelType) System.Enum.Parse(typeof(UIPanelType), panelName);
        UIManager.Instance.PushPanel(uIPanelType);
    }
}
