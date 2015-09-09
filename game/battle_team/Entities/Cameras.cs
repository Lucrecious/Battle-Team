using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Cameras;
using WaveEngine.Framework.Graphics;

namespace BattleTeam.Entities
{
	internal static class Cameras
	{
		internal static readonly FixedCamera2D FixedCamera = new FixedCamera2D("Camera2D", Vector2.Zero)
		{
			BackgroundColor = Color.LightGray
		};
	}
}
