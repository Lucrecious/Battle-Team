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
		private readonly World world;

		private bool moduleInitialized = false;

		internal ArenaBehavior(World world, List<Team> teams)
		{
			Requires.NotNull(world, nameof(world));
			Requires.NotNull(teams, nameof(teams));

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
			this.HandleHitCollisions();

			this.world.Bullets = new ImmutablePythonList<Bullet>(this.GetLiveBullets());

			foreach (Team team in this.teams)
			{
				team.ResolveMessages();
			}
		}

		private void HandleHitCollisions()
		{
			foreach (Entity damager in
				Utilities.Chain(
					this.Scene.EntityManager.FindAllByTag(Constants.Tags.Bullet),
					this.Scene.EntityManager.FindAllByTag(Constants.Tags.Sword)))
			{
				RectangleCollider damagerCollider = damager.FindComponent<RectangleCollider>();
				foreach (Entity other in
					Utilities.Chain(
						this.Scene.EntityManager.FindAllByTag(Constants.Tags.Wall),
						this.Scene.EntityManager.FindAllByTag(Constants.Tags.Member)))
				{
					RectangleCollider otherCollider = other.FindComponent<RectangleCollider>();

					if (damagerCollider.Intersects(otherCollider))
					{
						bool isGhost = false;
						if (other.Tag == Constants.Tags.Member)
						{
							CharacterBehavior behavior = other.FindComponent<CharacterBehavior>(isExactType: false);
							if (!behavior.DealDamageToMemberOrFail(this.GetDamageSender(behavior), damager.Tag))
							{
								isGhost = true;
							}
						}

						if (!isGhost && damager.Tag == Constants.Tags.Bullet)
						{
							damager.FindComponent<BulletBehavior>().MarkAsDead();
						}
					}
				}
			}
		}

		private Member GetDamageSender(CharacterBehavior behavior)
		{
			Requires.NotNull(behavior, nameof(behavior));

			if (behavior.Owner.Tag == Constants.Tags.Bullet)
			{
				return behavior.Owner.FindComponent<BulletBehavior>().Bullet.GetShooter();
			}

			return null;
		}

		private ImmutableArray<Bullet> GetLiveBullets()
		{
			List<Bullet> bullets = new List<Bullet>();

			foreach (Entity entity in this.Scene.EntityManager.FindAllByTag(Constants.Tags.Bullet))
			{
				Behavior behavior = entity.FindComponent<BulletBehavior>();
				if (behavior as BulletBehavior != null)
				{
					bullets.Add(((BulletBehavior)behavior).Bullet);
				}
			}

			return bullets.ToImmutableArray();
		}

		private void BoundMembersByWalls()
		{
			foreach (Entity wall in this.Scene.EntityManager.FindAllByTag(Constants.Tags.Wall))
			{
				var wallCollider = wall.FindComponent<RectangleCollider>();
				foreach (Entity member in this.Scene.EntityManager.FindAllByTag(Constants.Tags.Member))
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

			foreach (Entity entity in this.Scene.EntityManager.FindAllByTag(Constants.Tags.Wall))
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
