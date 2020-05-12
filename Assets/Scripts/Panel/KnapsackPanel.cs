using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackPanel : BasePanel
{
    
    private CanvasGroup canvasGroup;

    public void Start(){
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void OnClosePanel(){
        UIManager.Instance.PopPanel();
    }

    
    public override void OnEntry(){
        if(canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public override void OnExit(){
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnItemButtonClikc(){
        UIManager.Instance.PushPanel(UIPanelType.ItemMessage);
    }

    public override void OnPause(){
        canvasGroup.blocksRaycasts = false;
    }

    public override void OnResume(){
        canvasGroup.blocksRaycasts = true;
    }

}
