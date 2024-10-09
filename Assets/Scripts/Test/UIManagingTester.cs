using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagingTester : MonoBehaviour
{
    private UIManager manager;

    private UI_Test test1UI;
    private UI_Test2 test2UI;


    private void Awake()
    {
        manager = FindObjectOfType<UIManager>();
    }

    public void SpawnTest1UI()
    {
        UIBaseData data = new UIBaseData();

        test1UI = manager.OpenUI<UI_Test>(data, manager.OpenedUITrs);
    }

    public void SpawnTest2UI()
    {
        UIBaseData data = new UIBaseData();

        test2UI = manager.OpenUI<UI_Test2>(data, manager.OpenedUITrs);
    }

    public void SpawnTest1UIAnimVer() // 여기서 하는 건 의미가 없어 보이는데 ㅇㅅㅇ
    {
        UIBaseData data = new UIBaseData();
        data.onShow = () => Debug.Log("일단 이걸로 때우기");
    }

    public void SpawnTest2UIAnimVer()
    {

    }

    public void CloseTest1UI()
    {
        manager.CloseUI(test1UI);
    }

    public void CloseFrontUI() => UIManager.Instance.CloseFrontUI();
    public void CloseAllUI() => UIManager.Instance.CloseAllUI();
}
