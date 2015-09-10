using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleTeam.Entities.Behaviors;
using BattleTeam.PythonComponents.Collections;
using BattleTeam.PythonComponents.Environment;
using BattleTeam.PythonComponents.Objects;
using BattleTeam.PythonComponents.Team;
using BattleTeam.Shared;
using Validation;
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

		internal ArenaBehavior(ArenaEnvironment environment, World world, List<Team> teams)
		{
			Requires.NotNull(environment, nameof(environment));
			Requires.NotNull(world, nameof(world));
			Requires.NotNull(teams, nameof(teams));

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

			this.BoundMembersByWalls();
			ImmutableDictionary<Entity, Bullet> entityBullet = this.GetLiveBullets();

			this.world.Bullets = new ImmutablePythonList<Bullet>(entityBullet.Values.ToImmutableArray());

			foreach (Team team in this.teams)
			{
				team.ResolveMessages();
			}
		}

		private ImmutableDictionary<Entity, Bullet> GetLiveBullets()
		{
			Dictionary<Entity, Bullet> bullets = new Dictionary<Entity, Bullet>();

			foreach (Entity entity in this.Scene.EntityManager.AllEntities)
			{
				Behavior behavior = entity.FindComponent<BulletBehavior>();
				if (behavior as BulletBehavior != null)
				{
					bullets[entity] = ((BulletBehavior)behavior).Bullet;
				}
			}

			return bullets.ToImmutableDictionary();
		}

		private void BoundMembersByWalls()
		{
			foreach (Entity wall in this.environment.Walls)
			{
				var wallCollider = wall.FindComponent<RectangleCollider>();
				foreach (Entity member in this.environment.Characters)
				{
					var memberCollider = member.FindComponent<RectangleCollider>();
					this.PushMemberOutOfWall(member, memberCollider, wallCollider);
				}
			}
		}

		private void PushMemberOutOfWall(Entity memberEntity, RectangleCollider memberCollider, RectangleCollider wallCollider)
		{
			Requires.NotNull(memberEntity, nameof(memberEntity));
			Requires.NotNull(memberCollider, nameof(memberCollider));
			Requires.NotNull(wallCollider, nameof(wallCollider));

			Vector2? pushOut;

			if (memberCollider.Intersects(wallCollider, out pushOut))
			{
				memberCollider.Transform2D.Position += (Vector2)pushOut;
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
