using System;
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
	internal class CharacterBehavior : Behavior
	{
		private readonly Member member;

		[RequiredComponent]
		private Transform2D trans2D = null;

		[RequiredComponent]
		private RectangleCollider rectangleCollider = null;

		internal CharacterBehavior(Member member)
		{
			this.member = member;
		}

		protected override void Update(TimeSpan gameTime)
		{
			this.member.SetPosition(this.trans2D.Position);
			this.member.SetRotation(this.trans2D.Rotation);
			this.member.SetRectangle(this.rectangleCollider.Transform2D.Rectangle);

			IPlay play = this.member.Turn();

			this.trans2D.Position += play.GetMoveDirection() * this.member.GetSpeed() * (float)gameTime.TotalSeconds;
			this.trans2D.Rotation = Utilities.WrapFloat(this.trans2D.Rotation + play.GetRotation(), 0, 2 * Math.PI);

			if (play.GetMessage() != null)
			{
				play.GetMessage().Sender = this.member;
				this.member.GetTeam().AddMessage(play.GetMessage());
			}

			// Use attack using play.UseAttack
		}
	}
}
