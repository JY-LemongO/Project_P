using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseData
{
    public Action onShow;
    public Action onHide;    
}

public class UIBase : MonoBehaviour
{
    public Action onShow;
    public Action onHide;

    public int dev_Id;
    public string dev_Name;

    private RectTransform _rect;
    private bool _isInit = false;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        if (_isInit)
            return;

        _isInit = true;

        _rect = GetComponent<RectTransform>();        
    }

    public virtual void Setup(UIBaseData data, Transform anchor)
    {
        transform.SetParent(anchor);
        _rect.localPosition = Vector3.zero;

        this.onShow = data.onShow;
        this.onHide = data.onHide;
    }

    public virtual void CloseUI()
    {
        onShow = null;
        onHide = null;

        gameObject.SetActive(false);
    }
}
