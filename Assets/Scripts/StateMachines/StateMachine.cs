
public abstract class StateMachine
{
    protected IState currentState;

    public void ChangeState(IState newSatae)
    {
        currentState?.Exit(); // ���� ���� ����
        
        currentState = newSatae; // ���� ���¸� ���ο� ���·�

        currentState?.Enter(); // ���ο� ���� ����
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
