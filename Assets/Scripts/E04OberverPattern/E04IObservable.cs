using System;

public interface E04IObservable<T>
{
    // 관찰자를 등록하는 메서드
    public void Subscribe(E04IObserver<T> observer);

    // 관찰자에게 메시지를 전달하는 메서드
    public void Notify(T value);
}
