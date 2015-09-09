using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleTeam.Entities.Behaviors;
using BattleTeam.PythonComponents.Team;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;
using WaveEngine.Framework.Services;
using Validation;

namespace BattleTeam.Entities
{
	internal static class Characters
	{
		private static readonly ImmutableDictionary<Class, string> ClassToSpritePath = new Dictionary<Class, string>
		{

		}.ToImmutableDictionary();

		internal static Entity GetCharacter(Member member)
		{
			Requires.NotNull(member, nameof(member));

			return new Entity()
			.AddComponent(new RectangleCollider())
			.AddComponent(new Transform2D()
			{
				Origin = new Vector2(0.5f, 0.5f),
				XScale = 3,
				YScale = 3,
			})
			.AddComponent(new SpriteRenderer(DefaultLayers.Alpha, samplerMode: AddressMode.PointClamp))
			.AddComponent(new Sprite(ClassToSpritePath[member.GetClass()]))
			.AddComponent(new CharacterBehavior(member));
		}
	}
}
