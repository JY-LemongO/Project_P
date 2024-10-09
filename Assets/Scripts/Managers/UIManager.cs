using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    // [To Do]
    // UI들은 모두 한 개 씩 열리는 것을 전제로 한다.
    // 2개 이상 뜨는 팝업은 없다.    

    [field: SerializeField] public Transform OpenedUITrs { get; private set; }
    [field: SerializeField] public Transform ClosedUITrs { get; private set; }

    private Dictionary<Type, UIBase> _openedUIDict = new Dictionary<Type, UIBase>();
    private Dictionary<Type, UIBase> _closedUIDict = new Dictionary<Type, UIBase>();

    // 가장 앞에 나와있는 UI
    [SerializeField] private UIBase _frontUI;

    [SerializeField] private int _frontIndex;

    protected override void Init()
    {
        _frontIndex = OpenedUITrs.transform.childCount - 1;

        base.Init();
    }

    // OpenUI용 메서드. 딕셔너리에서 타입 확인 후 꺼내오기
    private UIBase GetUI(Type type, out bool isOpened)
    {
        UIBase ui = null;

        isOpened = false;

        // 이미 opened이거나 closed일 경우 Dict에서 가져오기
        // 둘 다 아닐경우, 새로 생성하기.

        if (_openedUIDict.TryGetValue(type, out UIBase openedUI))
        {
            ui = openedUI;
            isOpened = true;
        }
        else if (_closedUIDict.TryGetValue(type, out UIBase closedUI))
        {
            ui = closedUI;
            _closedUIDict.Remove(type);
        }
        else
        {
            ui = Instantiate(Resources.Load<UIBase>($"Prefabs/UI/{type}"));
        }

        return ui;
    }

    // T Type UIBase를 활성화    
    public T OpenUI<T>(UIBaseData data, Transform anchor) where T : UIBase
    {
        // 이미 열려져 있는가?
        bool isOpened = false;
        Type uiType = typeof(T);

        UIBase ui = GetUI(uiType, out isOpened);

        if (!isOpened)
        {
            _openedUIDict.Add(uiType, ui);
            _frontIndex++;
        }

        // 같은 UI를 쓰더라도 들어가는 정보는 다르기 때문에 UIData를 받아 넘겨주고 Setup 하도록 한다.        
        _frontUI = ui;
        ui.gameObject.SetActive(true);
        ui.Setup(data, anchor);
        ui.transform.SetSiblingIndex(_frontIndex);

        return ui as T;
    }

    /// <summary>
    /// 닫고자 하는 ui를 넘겨 해당하는 ui가 open 상태면 닫는다.
    /// </summary>
    /// <param name="ui">UIBase에서 호출하기 때문에 왠만하면 this를 넘긴다.</param>
    public void CloseUI(UIBase ui)
    {
        if (ui == null)
        {
            Debug.Log("모든 UI가 다 닫혔습니다.");
            return;
        }

        Type uiType = ui.GetType();
        Debug.Log(uiType);

        if (_closedUIDict.ContainsKey(uiType))
        {
            Debug.Log("이미 닫힌 UI 입니다.");
            return;
        }

        ui.gameObject.SetActive(false);
        ui.transform.SetParent(ClosedUITrs);
        _openedUIDict.Remove(uiType);
        _closedUIDict.Add(uiType, ui);
        _frontIndex--;

        var lastUI = OpenedUITrs.GetChild(OpenedUITrs.childCount - 1);
        if (lastUI.TryGetComponent(out UIBase front))
            _frontUI = front;
        else
            _frontUI = null;
        // 가장 마지막 UI를 Queue에 담는 방법도 고려해보자.
    }

    public void CloseFrontUI() => CloseUI(_frontUI);

    public void CloseAllUI()
    {
        while (_frontUI)
            CloseFrontUI();
    }
}
