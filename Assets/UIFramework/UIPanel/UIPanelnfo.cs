using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UIPanelnfo:ISerializationCallbackReceiver
{
    [NonSerialized]
    public UIPanelType panelType;
    public string panelTypeString;
    // {
    //     get{
    //         return panelType.ToString();
    //     }
    //     set{
    //         UIPanelType type  = (UIPanelType)System.Enum.Parse( typeof(UIPanelType), value);
    //         panelType = type;
    //     }
    // }
    public string path;

    //反序列化
    public void OnAfterDeserialize()
    {
        UIPanelType type  = (UIPanelType)System.Enum.Parse( typeof(UIPanelType), panelTypeString);
        panelType = type;
    }

    public void OnBeforeSerialize()
    {
        //return panelType.ToString();
    }
}
