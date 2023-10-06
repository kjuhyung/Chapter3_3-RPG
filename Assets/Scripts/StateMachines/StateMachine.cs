
public abstract class StateMachine
{
    protected IState currentState;

    public void ChangeState(IState newSatae)
    {
        currentState?.Exit(); // 현재 상태 종료
        
        currentState = newSatae; // 현재 상태를 새로운 상태로

        currentState?.Enter(); // 새로운 상태 시작
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}
