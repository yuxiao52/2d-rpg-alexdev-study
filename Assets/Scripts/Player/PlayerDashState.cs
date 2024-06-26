using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.clone.CreateCloneOnDashStart();// 冲刺开始时创建分身

        stateTime = player.dashDuration;

    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
            return;
        }

        player.SetVelocity(player.dashSpeed * player.dashDirection, 0);

        if (stateTime < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.skill.clone.CreateCloneOnDashOver();// 冲刺结束时创建分身
        player.SetVelocity(0, rb.velocity.y);
    }
}
