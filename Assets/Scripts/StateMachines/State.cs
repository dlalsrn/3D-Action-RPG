// Idle
// Attack
// Targeting
// Dead
public abstract class State
{
    public abstract void Enter();               // 상태 진입 시 호출되는 메서드 
    public abstract void Tick(float deltaTime); // 상태가 활성화되어 있는 동안 매 프레임 호출되는 메서드
    public abstract void Exit();                // 상태가 종료될 때 호출되는 메서드
}
