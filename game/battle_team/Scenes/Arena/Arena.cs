/********************************
*			Lucrecious			*
*			Under MIT			*
*********************************/

#region Using Statements
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using BattleTeam.Entities;
using BattleTeam.PythonComponents.Environment;
using BattleTeam.PythonComponents.Plays;
using BattleTeam.PythonComponents.Team;
using BattleTeam.Shared;
using IronPython.Hosting;
using IronPython.Runtime.Types;
using Microsoft.Scripting.Hosting;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;
#endregion

namespace BattleTeam.Scenes.Arena
{
	public class Arena : Scene
	{
		private readonly ScriptEngine engine = Python.CreateEngine();

		protected override void CreateScene()
		{
			List<Entity> Walls = new List<Entity>();
			this.EntityManager.Add(Cameras.FixedCamera);

			var environment = new ArenaEnvironment();

			this.EntityManager.Add(environment.TopWall);
			this.EntityManager.Add(environment.BottomWall);
			this.EntityManager.Add(environment.LeftWall);
			this.EntityManager.Add(environment.RightWall);

			List<Team> teams = new List<Team>();
			World world = new World();
			MakeArenaModule(this.engine, teams, world);

			var scope = this.engine.CreateScope();
			var script = this.engine.CreateScriptSourceFromFile(@"C:\BattleTeam\Test.py");

			script.Execute(scope);

			foreach (Team team in teams)
			{
				if (team.GetMembers().GetLength() > 0)
				{
					for (int i = 0; i < team.GetMembers().GetLength(); ++i)
					{
						this.EntityManager.Add(Characters.GetCharacter(team.GetMembers().Get(i)));
					}

					this.AddSceneBehavior(new ArenaBehavior(environment, world, team), SceneBehavior.Order.PreUpdate);
				}
			}
		}

		private static void MakeArenaModule(ScriptEngine engine, List<Team> teams, World world)
		{
			var module = engine.CreateModule(Constants.ModuleName);

			module.SetVariable(nameof(Gunner), DynamicHelpers.GetPythonTypeFromType(typeof(Gunner)));
			module.SetVariable(nameof(Team), DynamicHelpers.GetPythonTypeFromType(typeof(Team)));
			module.SetVariable(nameof(Movement), DynamicHelpers.GetPythonTypeFromType(typeof(Movement)));
			module.SetVariable(nameof(Rotation), DynamicHelpers.GetPythonTypeFromType(typeof(Rotation)));
			module.SetVariable(nameof(teams), teams);
			module.SetVariable("World", world);
		}

		protected override void Start()
		{
			base.Start();
		}
	}
}
