using System;

public interface E04IObservable<T>
{
    // �����ڸ� ����ϴ� �޼���
    public void Subscribe(E04IObserver<T> observer);
    // �����ڿ��� �޽����� �����ϴ� �޼���
    public void Notify(T value);
}
