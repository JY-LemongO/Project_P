using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTester : MonoBehaviour
{
    [SerializeField] GameObject triggerObj;
    [SerializeField] int spawnCount;
    [SerializeField] float spawnDelay;
    [SerializeField] float distance;
    [SerializeField] float rotationSpeed;
    [SerializeField] float speed;
    [SerializeField] bool triggerSwitch;

    private float currentAngle;

    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(Co_Shoot());

        if (Input.GetMouseButtonDown(1))
            ShootOneTime();
    }

    private void ShootOneTime()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            currentAngle -= rotationSpeed * Time.deltaTime;

            // 각도를 라디안으로 변환
            float angleRadians = currentAngle * Mathf.Deg2Rad;

            // 각도에 따른 방향 벡터 계산
            Vector2 direction = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));

            GameObject go = Instantiate(triggerObj);
            go.transform.position = Vector2.zero + direction * distance;
            go.GetComponent<Rigidbody2D>().AddForce(-direction * speed, ForceMode2D.Impulse);            
        }
    }

    private IEnumerator Co_Shoot()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            currentAngle -= rotationSpeed * Time.deltaTime;

            // 각도를 라디안으로 변환
            float angleRadians = currentAngle * Mathf.Deg2Rad;

            // 각도에 따른 방향 벡터 계산
            Vector2 direction = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));

            GameObject go = Instantiate(triggerObj);
            go.transform.position = Vector2.zero + direction * distance;
            go.GetComponent<Rigidbody2D>().AddForce(-direction * speed, ForceMode2D.Impulse);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerSwitch)
            Debug.Log("트리거 감지");
    }
}
