﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleTeam.Shared;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;
using WaveEngine.Framework.Services;

namespace BattleTeam.Entities
{
	internal static class Statics
	{
		internal static Entity CreateTopWall() => new Entity()
		{
			Tag = Constants.Tags.Wall
		}
			.AddComponent(new RectangleCollider())
			.AddComponent(new Transform2D()
			{
				X = -WaveServices.Platform.ScreenWidth / 2,
				Y = -WaveServices.Platform.ScreenHeight / 2
			})
			.AddComponent(new Sprite("Content/horizontal_wall.png"))
			.AddComponent(new SpriteRenderer(DefaultLayers.Alpha));

		internal static Entity CreateBottomWall() => new Entity()
		{
			Tag = Constants.Tags.Wall
		}
			.AddComponent(new RectangleCollider())
			.AddComponent(new Transform2D()
			{
				X = -WaveServices.Platform.ScreenWidth / 2,
				Y = WaveServices.Platform.ScreenHeight / 5
			})
			.AddComponent(new Sprite("Content/horizontal_wall.png"))
			.AddComponent(new SpriteRenderer(DefaultLayers.Alpha));

		internal static Entity CreateLeftWall() => new Entity()
		{
			Tag = Constants.Tags.Wall
		}
			.AddComponent(new RectangleCollider())
			.AddComponent(new Transform2D()
			{
				X = -WaveServices.Platform.ScreenWidth / 2,
				Y = -WaveServices.Platform.ScreenHeight / 2
			})
			.AddComponent(new Sprite("Content/vertical_wall.png"))
			.AddComponent(new SpriteRenderer(DefaultLayers.Alpha));

		internal static Entity CreateRightWall() => new Entity()
		{
			Tag = Constants.Tags.Wall
		}
			.AddComponent(new RectangleCollider())
			.AddComponent(new Transform2D()
			{
				X = WaveServices.Platform.ScreenWidth / 2,
				Y = -WaveServices.Platform.ScreenHeight / 2
			})
			.AddComponent(new Sprite("Content/vertical_wall.png"))
			.AddComponent(new SpriteRenderer(DefaultLayers.Alpha));
	}
}
