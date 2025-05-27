using System;

public interface E04IObserver<T>
{
    // ���� ��� ������Ʈ�� Ư�� �޽����� �����ϱ� ���� �޼���
    public void OnNext(T value);
    // ���� ��� ������Ʈ�� �����ڿ��� ������ �޽����� ó���ϴ� �������� ������ �߻����� �� ȣ��Ǵ� �޼���
    public void OnError(Exception error);
    // ���� ��� ������Ʈ�� �� �̻� ������ ������Ʈ���� �޽����� ������ ���� ���� ���
    public void OnCompleted();
}
