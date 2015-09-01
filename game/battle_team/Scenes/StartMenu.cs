/********************************
*			Lucrecious			*
*			Under MIT			*
*********************************/

#region Using Statements
using System;
using BattleTeam.Entities;
using BattleTeam.PythonComponents;
using BattleTeam.Shared;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Cameras;
using WaveEngine.Components.Gestures;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;
using WaveEngine.Framework.Services;
#endregion

namespace BattleTeam.Scenes
{
	public class StartMenu : Scene
	{
		protected override void CreateScene()
		{
			this.EntityManager.Add(Cameras.FixedCamera);
		}

		protected override void Start()
		{
			base.Start();
		}
	}
}
