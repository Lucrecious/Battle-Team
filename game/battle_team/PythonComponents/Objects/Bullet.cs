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
		public static int GetMaxSpeed() => 10;

		private readonly Member shooter;
		public Member GetShooter() => this.shooter;

		private readonly Vector2 direction;
		public Vector2 GetDirection() => this.direction;

		internal Vector2 Position { get; set; }
		public Vector2 GetPosition() => this.Position;

		private RectangleF bounds;
		public RectangleF GetBounds() => this.bounds;

		internal bool Initialized { get; private set; }

		internal Bullet(Member shooter, Vector2 direction)
		{
			this.shooter = shooter;
			this.direction = direction;
			this.Initialized = false;
		}

		internal void Initialize(RectangleF bounds)
		{
			this.Initialized = true;
			this.bounds = bounds;
		}
	}
}
