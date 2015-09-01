using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleTeam.PythonComponents.Environment;
using BattleTeam.PythonComponents.Team;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;

namespace BattleTeam.Scenes.Arena
{
	internal class ArenaBehavior : SceneBehavior
	{
		private readonly ImmutableArray<Team> teams;
		private readonly ArenaEnvironment environment;
		private readonly World world;

		private bool moduleInitialized = false;

		internal ArenaBehavior(ArenaEnvironment environment, World world, params Team[] teams)
		{
			this.environment = environment;
			this.world = world;
			this.teams = teams.ToImmutableArray();
		}

		protected override void ResolveDependencies()
		{
		}

		protected override void Update(TimeSpan gameTime)
		{
			if (!this.moduleInitialized)
			{
				this.moduleInitialized = true;

				this.InitializeWorld();
			}

			foreach (Team team in this.teams)
			{
				team.ResolveMessages();
			}
		}

		private void InitializeWorld()
		{
			List<Member> allMembers = new List<Member>();

			foreach (Team team in this.teams)
			{
				allMembers.AddRange(team.GetMembers().Array);
			}

			List<RectangleF> rectangles = new List<RectangleF>();

			foreach (Entity entity in this.environment.Walls)
			{
				var trans2D = entity.FindComponent<Transform2D>();
				var rect = entity.FindComponent<RectangleCollider>().Transform2D.Rectangle;
				rectangles.Add(new RectangleF(trans2D.X, trans2D.Y, rect.Width, rect.Height));
			}

			this.world.Initialize(
				rectangles[0],
				rectangles[1],
				rectangles[2],
				rectangles[3],
				allMembers.ToImmutableArray());
		}
	}
}
