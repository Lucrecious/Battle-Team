using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleTeam.Entities.Behaviors;
using BattleTeam.PythonComponents.Team;
using Validation;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;

namespace BattleTeam.Entities
{
	internal static class Characters
	{
		private static readonly ImmutableDictionary<Class, string> ClassToSpritePath = new Dictionary<Class, string>
		{
			{ Class.Gunner, "Content/gunner" }
		}.ToImmutableDictionary();

		private static CharacterBehavior CreateCharacterBehaviorFromMember(Member member)
		{
			Requires.NotNull(member, nameof(member));

			switch (member.GetClass())
			{
				case Class.Gunner:
					{
						return new GunnerBehavior(member);
					}
			}

			throw new NotImplementedException();
		}

		internal static Entity CreateCharacter(Member member)
		{
			Requires.NotNull(member, nameof(member));

			return new Entity()
			{
				Tag = "member"
			}
			.AddComponent(new RectangleCollider())
			.AddComponent(new Transform2D()
			{
				XScale = 3,
				YScale = 3,
			})
			.AddComponent(new SpriteRenderer(DefaultLayers.Alpha, samplerMode: AddressMode.PointClamp))
			.AddComponent(new Sprite(ClassToSpritePath[member.GetClass()]))
			.AddComponent(CreateCharacterBehaviorFromMember(member));
		}

		internal static Entity CreateBullet(Member shooter, Vector2 position, float rotation, Vector2 direction)
		{
			Requires.NotNull(shooter, nameof(shooter));

			return new Entity()
			{
				Tag = "bullet"
			}
				.AddComponent(new RectangleCollider())
				.AddComponent(new Transform2D()
				{
					Position = position,
					Rotation = rotation
				})
				.AddComponent(new SpriteRenderer(DefaultLayers.Alpha, samplerMode: AddressMode.PointClamp))
				.AddComponent(new Sprite("Content/bullet"))
				.AddComponent(new BulletBehavior(shooter, direction));
		}
	}
}
