using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class Player
{
    private ReactiveProperty<int> hp;
    public IObservable<int> Hp => hp;

    public Player()
    {
        hp = new ReactiveProperty<int>(100);
    }

    public void Dagame(int dagame)
    {
        hp.Value -= dagame;
    }
}

public class E05UniRxBasic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Subject Example
        /*
        Subject<int> subject = new Subject<int>();

        subject.Subscribe(v => Debug.Log("Value: " + v));
        
        subject.OnNext(1);
        subject.OnNext(2);
        subject.OnNext(3);

        subject.OnCompleted();
        */


        // Reactive Property Example
        /*
        Player player = new Player();
        player.Hp
            .Where(hp => hp <= 0)
            .Select(hp => $"{hp} points")
            .Subscribe(hp => Debug.Log("HP: " + hp));

        player.Dagame(10);
        player.Dagame(10);
        player.Dagame(10);
        player.Dagame(10);
        player.Dagame(10);
        player.Dagame(10);
        player.Dagame(10);
        player.Dagame(10);
        player.Dagame(10);
        player.Dagame(10);
        */


        // Factory Method Example
        /*
        IObservable<string> messageObject = Observable.Create<string>(observer =>
        {
            observer.OnNext("Hello World");
            int sum = 1 + 2;
            observer.OnNext(sum.ToString());

            return Disposable.Empty;
        });

        messageObject.Subscribe(m => Debug.Log(m));
        messageObject.Subscribe(m => Debug.Log("Value: " + m));
        */

        /*
        Observable.Start(() =>
        {
            Debug.Log("로그인 중...");
            System.Threading.Thread.Sleep(3000);
            return true;
        }).SelectMany(result => Observable.Start(() =>
        {
            if (result)
            {
                Debug.Log("로그인 완료!");
                Debug.Log("스테이지 로딩 중...");
                System.Threading.Thread.Sleep(3000);
                return true;
            }
            else
            {
                Debug.Log("로딩 실패");
                return false;
            }
        }))
        .ObserveOnMainThread()
        .Subscribe(result => Debug.Log(result ? "스테이지 로딩 완료" : "게임 종료"));
        */


        // Trigger Example
        /*
        this.OnCollisionEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag("Player"))
            .Subscribe(collision => Debug.Log("플레이어와 충돌 발생"));

        this.OnTriggerEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag("Player"))
            .Subscribe(collision => Debug.Log("플레이어와 충돌 발생"));

        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Space))
            .Subscribe(_ => Debug.Log("스페이스 버튼 입력"));

        this.FixedUpdateAsObservable()
            .Subscribe(_ => Debug.Log("Fixed Update 호출"));
        */

        var clickStream = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0));

        clickStream.Buffer(TimeSpan.FromMilliseconds(500))
            .Where(click => click.Count >= 2)
            .Subscribe(click => Debug.Log("더블클릭 발생!"));
    }
}
