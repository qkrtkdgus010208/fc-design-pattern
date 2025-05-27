using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public struct E05PlayerData
{
    public float hp;
}

public class E05PlayerController : MonoBehaviour, E04IObservable<E05PlayerData>
{
    private CharacterController characterController;
    private float speed = 5f;

    private List<E04IObserver<E05PlayerData>> observers = new List<E04IObserver<E05PlayerData>>();

    private E05PlayerData playerData;

    #region Observer Pattern
    private void OnDestroy()
    {
        foreach (var observer in observers)
        {
            observer.OnCompleted();
        }
    }

    public void Notify(E05PlayerData value)
    {
        foreach (var observer in observers)
        {
            observer.OnNext(value);
        }
    }

    public void Subscribe(E04IObserver<E05PlayerData> observer)
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

        playerData.hp = 100;

        Observable.EveryUpdate()
            .Select(_ => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")))
            .Where(input => input != Vector2.zero)
            .Subscribe(input =>
            {
                Vector3 direction = new Vector3(input.x, 0, input.y);
                characterController.Move(transform.TransformDirection(direction) * speed * Time.deltaTime);
            }).AddTo(this);

        Observable.EveryUpdate()
            .Select(_ => Input.mousePosition)
            .Distinct()
            .Subscribe(mousePosition =>
            {
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo)) 
                {
                    Vector3 targetPosition = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
                    transform.LookAt(targetPosition);
                }
            }).AddTo(this);

        this.UpdateAsObservable()
            .Select(_ => characterController.transform.position)
            .DistinctUntilChanged()
            .Sample(TimeSpan.FromSeconds(1))
            .Subscribe(position =>
            {
                E05GameManager.Instance.UpdatePlayerPosition(position);
            });

        this.OnCollisionEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag("Bullet"))
            .Subscribe(collision =>
            {
                playerData.hp -= 10;
                Notify(playerData);

                if (playerData.hp < 0)
                {
                    Destroy(gameObject);
                }
            });
    }
}
