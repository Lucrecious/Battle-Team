using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleTeam.PythonComponents.Team;
using WaveEngine.Common.Math;

namespace BattleTeam.PythonComponents.Objects
{
	public class Bullet
	{
		public static int GetSpeed() => 10;

		private readonly Member shooter;
		public Member GetShooter() => this.shooter;

		private readonly Vector2 direction;
		public Vector2 GetDirection() => this.direction;

		internal Bullet(Member shooter, Vector2 direction)
		{
			this.shooter = shooter;
			this.direction = direction;
		}

	}
}
