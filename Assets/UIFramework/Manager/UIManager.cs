using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UIManager
{
    private static UIManager _instance;

    public static UIManager Instance{
        get{
            if(_instance == null){
                _instance = new UIManager();
            }
            return _instance;
        }
    }
    private Transform canvasTransform;
    private Transform CanvasTransform{
        get{
            if(canvasTransform == null){
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    private Dictionary<UIPanelType, string> panelPathDict;// save all ui panels path
    //Q: why save BasePanel , can we save GameObject?
    private Dictionary<UIPanelType, BasePanel> panelDict;// save all instantiated components of panel

    private Stack<BasePanel> panelStack;


    private UIManager(){
       ParseUIPanelJson();
    }

    public void PushPanel(UIPanelType panelType){
        if(panelStack == null){
            panelStack = new Stack<BasePanel>();
        }

        if(panelStack.Count > 0){
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }
        BasePanel panel = GetPanel(panelType);
        panel.OnEntry();
        panelStack.Push(panel);
    }

    public void PopPanel(){
        if(panelStack == null){
            panelStack = new Stack<BasePanel>();
        }

        if(panelStack.Count <= 0){
            return ;
        }
        
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        if(panelStack.Count > 0){
            BasePanel top2Panel = panelStack.Peek();
            top2Panel.OnResume();
        }

    }

    public BasePanel GetPanel(UIPanelType panelType){
        if(panelDict == null){
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

        BasePanel basePanel;
        panelDict.TryGetValue(panelType, out basePanel);
        
        if(basePanel == null){
            string path;
            panelPathDict.TryGetValue(panelType, out path);
            GameObject insPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            insPanel.transform.SetParent(CanvasTransform, false);
            panelDict.Add(panelType, insPanel.GetComponent<BasePanel>());
            return insPanel.GetComponent<BasePanel>();
        }
        else{
            return basePanel;
        }
    }

    [Serializable]
    class UIPanelTypeJson{
        public List<UIPanelnfo> infoList;
    }

    private void ParseUIPanelJson(){
        panelPathDict = new Dictionary<UIPanelType, string>();

        TextAsset ta = Resources.Load<TextAsset>("UIPanelType"); // Doc??

        //List<UIPanelnfo> panelInfoList = JsonUtility.FromJson<List<UIPanelnfo>>(ta.text);
        UIPanelTypeJson jsonObject =  JsonUtility.FromJson<UIPanelTypeJson>(ta.text);

        foreach(UIPanelnfo info in jsonObject.infoList){

            panelPathDict.Add(info.panelType, info.path);

        }
    }
}
 