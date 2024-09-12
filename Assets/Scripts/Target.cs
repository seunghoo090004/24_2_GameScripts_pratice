using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float rotateSpeed = 100;
    private Vector3 rotateAngle = Vector3.forward;

    private SpriteRenderer spriteRenderer;

    private IEnumerator Start()
    {
        // 1~4초에 한 번씩 회전 속도를 10~299 사이의 값으로 설정하고,
        // 이동방향을 (0, 0, -1) or (0, 0, 1)으로 설정

        spriteRenderer = GetComponent<SpriteRenderer>();

        while ( true )
        {
            int time = Random.Range(1, 5);

            yield return new WaitForSeconds(time);

            int speed = Random.Range(10, 300);
            int dir = Random.Range(0, 2);

            rotateSpeed = speed;
            rotateAngle = new Vector3(0, 0, dir * 2 - 1);
        }
    }

    private void Update()
    {
        transform.Rotate(rotateAngle * rotateSpeed * Time.deltaTime);
    }

    public void Hit()
    {
        StopCoroutine("OnHit");
        StartCoroutine("OnHit");
    }

    private IEnumerator OnHit()
    {
        spriteRenderer.color = new Color(0.15f, 0.15f, 0.15f, 1);

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.black;
    }
}
