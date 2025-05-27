using System;

public interface E04IObserver<T>
{
    // 관찰 대상 오브젝트가 특정 메시지를 전달하기 위한 메서드
    public void OnNext(T value);
    // 관찰 대상 오브젝트가 관찰자에게 전달할 메시지를 처리하는 과정에서 오류가 발생했을 때 호출되는 메서드
    public void OnError(Exception error);
    // 관찰 대상 오브젝트가 더 이상 관찰자 오브젝트에게 메시지를 전달할 것이 없는 경우
    public void OnCompleted();
}
