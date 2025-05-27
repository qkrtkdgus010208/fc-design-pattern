using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct E04PlayerData
{
    public float hp;
}

public class E04PlayerController : MonoBehaviour, E04IObservable<E04PlayerData>
{
    private CharacterController characterController;
    private float speed = 5f;

    private List<E04IObserver<E04PlayerData>> observers = new List<E04IObserver<E04PlayerData>>();

    private E04PlayerData playerData;

    #region Observer Pattern
    private void OnDestroy()
    {
        foreach (var observer in observers)
        {
            observer.OnCompleted();
        }
    }

    public void Notify(E04PlayerData value)
    {
        foreach (var observer in observers)
        {
            observer.OnNext(value);
        }
    }

    public void Subscribe(E04IObserver<E04PlayerData> observer)
    {
        if (!observers.Contains(observer)) 
        {
            observers.Add(observer);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        StartCoroutine(UpdatePosition());

        playerData.hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3 (horizontal, 0, vertical);

        characterController.Move(direction * speed * Time.deltaTime);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(targetPosition);
        }
    }

    IEnumerator UpdatePosition()
    {
        while (true)
        {
            // GameManager에게 Player 위치 전달
            E04GameManager.Instance.UpdatePlayerPosition(characterController.transform.position);
            yield return new WaitForSeconds(1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            playerData.hp -= 10;
            Notify(playerData);

            if (playerData.hp < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
