using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleTeam.PythonComponents.Objects;
using BattleTeam.PythonComponents.Team;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;

namespace BattleTeam.Entities.Behaviors
{
	internal class BulletBehavior : LivingBehavior
	{
		internal Bullet Bullet { get; }

		[RequiredComponent]
		private readonly Transform2D trans2D = null;

		private Vector2 Velocity { get; }

		internal BulletBehavior(Member shooter, Vector2 direction)
		{
			this.Bullet = new Bullet(shooter, direction);
			direction.Normalize();
			this.Velocity = direction * Bullet.GetMaxSpeed();
		}

		protected override void Update(TimeSpan gameTime)
		{
			base.Update(gameTime);

			if (!this.Bullet.Initialized)
			{
				this.Bullet.Initialize(this.trans2D.Rectangle);
			}

			this.trans2D.Position += this.Velocity * (float)gameTime.TotalSeconds;
			this.Bullet.Position = this.trans2D.Position;
		}
	}
}
