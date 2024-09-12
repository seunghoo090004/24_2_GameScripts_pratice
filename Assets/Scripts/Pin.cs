using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pin : MonoBehaviour
{
    [SerializeField]
    private Transform hitEffectSpawnPoint;
    [SerializeField]
    private GameObject hitEffectPrefab;

    private Movement2D movement2D;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShakeCamera shakeCamera = Camera.main.GetComponent<ShakeCamera>();

        if ( collision.CompareTag("Target"))
        {
            //이동방향을 (0, 0, 0)으로 설정해 이동
            movement2D.MoveTo(Vector3.zero);
            //과녁의 자식으로 설정해 과녁과 함께 회전
            transform.SetParent(collision.transform);
            collision.GetComponent<Target>().Hit();

            Instantiate(hitEffectPrefab, hitEffectSpawnPoint.position, hitEffectSpawnPoint.rotation);

            //귀찮아서 Camera.main을 썼지만..... PinSpawner에서 ShakeCamera 변수를 미리 받아두고,
            // Pin을 생성할 때 Pin.Setup(shakeCamera);와 같이 받아와서 쓰면 더 좋다..
            if (shakeCamera != null)
            {
                shakeCamera.Shake(0.1f, 1);
            }
            // 과녁에 배치된 핀은 OnTriggerEnter2D()를 호출하지 않도록 스크립트 삭제
            Destroy(this);
        }
        else if(collision.CompareTag("Pin"))
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
