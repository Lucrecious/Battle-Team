using System;
using BattleTeam.PythonComponents.Plays;
using BattleTeam.PythonComponents.Team;
using BattleTeam.Shared;
using Validation;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;

namespace BattleTeam.Entities.Behaviors
{
	internal abstract class CharacterBehavior : Behavior
	{
		internal Member Member { get; }

		protected abstract Transform2D Trans2D { get; }
		protected abstract RectangleCollider RectangleCollider { get; }

		internal CharacterBehavior(Member member)
		{
			Requires.NotNull(member, nameof(member));
			this.Member = member;
		}

		internal abstract void UseAttack();

		internal bool DealDamageToMemberOrFail(Member sender, string tag)
		{
			if (sender == null)
			{
				return false;
			}

			if (!sender.GetTeam().GetMembers().Array.Contains(this.Member))
			{
				// Here I'd use the tag to calculate the correct amount of damage (given the weapon)
				this.Member.Heal(-1);
				return true;
			}

			return false;
		}

		protected override void Update(TimeSpan gameTime)
		{
			this.Member.Position = this.Trans2D.Position;
			this.Member.Rotation = this.Trans2D.Rotation;
			this.Member.Rectangle = this.RectangleCollider.Transform2D.Rectangle;

			IPlay play = this.Member.Turn();

			if (play == null)
			{
				return;
			}

			this.Trans2D.Position += play.GetMoveDirection() * this.Member.GetSpeed() * (float)gameTime.TotalSeconds;
			this.Trans2D.Rotation = Utilities.WrapFloat(this.Trans2D.Rotation + play.GetRotation(), 0, 2 * Math.PI);

			if (play.GetMessage() != null)
			{
				play.GetMessage().Sender = this.Member;
				this.Member.GetTeam().AddMessage(play.GetMessage());
			}

			if (play.IsUsingAttack())
			{
				this.UseAttack();
			}
		}
	}
}
