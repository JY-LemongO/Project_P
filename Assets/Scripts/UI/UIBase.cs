using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseData
{
    public Action onShow;
    public Action onHide;

    public int dev_Id;
    public string dev_Name;
}

public class UIBase : MonoBehaviour
{
    public Action onShow;
    public Action onHide;

    public int dev_Id;
    public string dev_Name;

    private bool _isInit = false;

    public virtual void Init()
    {
        if (_isInit)
            return;

        _isInit = true;        

        RectTransform rect = GetComponent<RectTransform>();
        rect.localPosition = Vector3.zero;
        rect.localScale = Vector3.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.one;        
    }

    public virtual void Setup(UIBaseData data, Transform anchor)
    {
        transform.SetParent(anchor);

        this.onShow = data.onShow;
        this.onHide = data.onHide;

        this.dev_Id = data.dev_Id;
        this.dev_Name = data.dev_Name;
    }

    private void OnDisable()
    {
        onShow = null;
        onHide = null;
    }
}
