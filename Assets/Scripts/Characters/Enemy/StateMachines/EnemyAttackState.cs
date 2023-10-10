
public class EnemyAttackState : EnemyBaseState
{

    private bool alreadyAppliedForce;
    private bool alreadyAppliedDealing;

    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        alreadyAppliedForce = false;
        alreadyAppliedDealing = false;

        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);

    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= stateMachine.Enemy.Data.ForceTransitionTime)
                TryApplyForce();

            if(!alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_Start_TransitionTime)
            {
                stateMachine.Enemy.Weapon.SetAttack(stateMachine.Enemy.Data.Damage, stateMachine.Enemy.Data.Force);
                stateMachine.Enemy.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
            }
            
            if(alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_End_TransitionTime)
            {
                stateMachine.Enemy.Weapon.gameObject.SetActive(false);
            }

        }
        else
        {
            if (IsInChaseRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }

    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Enemy.ForceReceiver.Reset();

        stateMachine.Enemy.ForceReceiver.AddForce(stateMachine.Enemy.transform.forward * stateMachine.Enemy.Data.Force);

    }
}