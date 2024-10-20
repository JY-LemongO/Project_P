using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicTester : MonoBehaviour
{
    private Dictionary<int, TestLogicData> testerDict = new Dictionary<int, TestLogicData>();

    
    public List<TestLogic> testLogics = new List<TestLogic>();

    [Header("A: Tester만들기\n" +
        "S: 다음 Tester 선택\n" +
        "D: 이전 Tester 선택\n" +
        "F: 현재 Tester 데미지 받기\n" +
        "G: 현재 Tester 상태 로그 띄우기")]
    public int _currnetIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            testLogics[_currnetIndex].LogCurrentStatus();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            testLogics[_currnetIndex].GetDamage();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _currnetIndex = Mathf.Clamp(_currnetIndex - 1, 0, testLogics.Count - 1);
            Debug.Log($"이전 테스터 선택::{_currnetIndex}");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _currnetIndex = Mathf.Clamp(_currnetIndex + 1, 0, testLogics.Count - 1);
            Debug.Log($"다음 테스터 선택::{_currnetIndex}");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TestLogic tester = new TestLogic();
            testLogics.Add(tester);

            TestDataSetup(tester);

            Debug.Log($"새로운 테스터 생성:: 현재 개수{testLogics.Count}");
        }
    }

    private void TestDataSetup(TestLogic tester)
    {
        if (!testerDict.ContainsKey(0))
        {
            testerDict.Add(0, new TestLogicData()
            {
                hp = 10,
                attack = 10,
                defense = 10,
            });
        }

        tester.Setup(testerDict[0]);
    }
}

public class TestLogicData
{
    public int hp;
    public int attack;
    public int defense;
}

public class TestLogic
{
    int hp;
    int attack;
    int defense;

    public void Setup(TestLogicData data)
    {
        hp = data.hp;
        attack = data.attack;
        defense = data.defense;
    }

    public void LogCurrentStatus()
    {
        Debug.Log(hp);
        Debug.Log(attack);
        Debug.Log(defense);
    }

    public void GetDamage()
    {
        hp -= 1;
        attack -= 1;
        defense -= 1;
    }
}
