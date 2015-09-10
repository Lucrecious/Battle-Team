using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Gestures;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;
using WaveEngine.Framework.Services;

namespace BattleTeam.PythonComponents
{
	internal static class Text
	{
		public static readonly Entity PlayButton = new Entity()
		{
			Tag = "text"
		}
			.AddComponent(new Transform2D()
			{
				Origin = new Vector2(0.5f, 0.5f),
				X = WaveServices.Platform.ScreenWidth / 2,
				Y = WaveServices.Platform.ScreenHeight / 2,
				XScale = 2f,
				YScale = 2f
			})
			.AddComponent(new TextControl()
			{
				Text = "Play",
				Foreground = Color.White
			})
			.AddComponent(new TextControlRenderer())
			.AddComponent(new RectangleCollider())
			.AddComponent(new TouchGestures());
	}
}
