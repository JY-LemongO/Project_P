using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : UIBase
{
    [SerializeField] Button btn_Retry;
    [SerializeField] Button btn_Home;

    public override void Setup(UIBaseData data, Transform anchor)
    {
        base.Setup(data, anchor);

        btn_Retry.onClick.AddListener(Btn_Retry);
        btn_Home.onClick.AddListener(Btn_Home);
    }

    public void Btn_Retry()
    {

        SceneLoader.Instance.LoadScene(SceneType.CombatTestScene);
    }

    public void Btn_Home()
    {
        SceneLoader.Instance.LoadScene(SceneType.Title);
    }    
}
