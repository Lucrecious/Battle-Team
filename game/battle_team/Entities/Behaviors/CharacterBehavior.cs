﻿using System;
using BattleTeam.PythonComponents.Plays;
using BattleTeam.PythonComponents.Team;
using BattleTeam.Shared;
using IronPython.Hosting;
using IronPython.Runtime.Types;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;

namespace BattleTeam.Entities.Behaviors
{
	internal abstract class CharacterBehavior : Behavior
	{
		private readonly Member member;

		protected abstract Transform2D Trans2D { get; }
		protected abstract RectangleCollider RectangleCollider { get; }

		internal CharacterBehavior(Member member)
		{
			this.member = member;
		}

		internal abstract void UseAttack();

		protected override void Update(TimeSpan gameTime)
		{
			this.member.SetPosition(this.Trans2D.Position);
			this.member.SetRotation(this.Trans2D.Rotation);
			this.member.SetRectangle(this.RectangleCollider.Transform2D.Rectangle);

			IPlay play = this.member.Turn();

			this.Trans2D.Position += play.GetMoveDirection() * this.member.GetSpeed() * (float)gameTime.TotalSeconds;
			this.Trans2D.Rotation = Utilities.WrapFloat(this.Trans2D.Rotation + play.GetRotation(), 0, 2 * Math.PI);

			if (play.GetMessage() != null)
			{
				play.GetMessage().Sender = this.member;
				this.member.GetTeam().AddMessage(play.GetMessage());
			}

			// Right now this should do nothing
			if (play.IsUsingAttack())
			{
				this.UseAttack();
			}
		}
	}
}
