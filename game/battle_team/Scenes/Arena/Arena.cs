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
using Validation;
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
			this.EntityManager.Add(Cameras.FixedCamera);

			List<Team> teams = new List<Team>();
			World world = new World();
			MakeArenaModule(this.engine, teams, world);

			var scope = this.engine.CreateScope();
			var script = this.engine.CreateScriptSourceFromFile(@"C:\BattleTeam\Test.py");

			script.Execute(scope); // Updates teams

			this.AddEnvironmentEntities(teams);

			this.AddSceneBehavior(new ArenaBehavior(world, teams), SceneBehavior.Order.PreUpdate);
		}

		private static void MakeArenaModule(ScriptEngine engine, List<Team> teams, World world)
		{
			Requires.NotNull(engine, nameof(engine));
			Requires.NotNull(teams, nameof(teams));
			Requires.NotNull(world, nameof(world));

			var module = engine.CreateModule(Constants.ModuleName);

			module.SetVariable(nameof(Gunner), DynamicHelpers.GetPythonTypeFromType(typeof(Gunner)));
			module.SetVariable(nameof(Team), DynamicHelpers.GetPythonTypeFromType(typeof(Team)));
			module.SetVariable(nameof(Movement), DynamicHelpers.GetPythonTypeFromType(typeof(Movement)));
			module.SetVariable(nameof(Rotation), DynamicHelpers.GetPythonTypeFromType(typeof(Rotation)));
			module.SetVariable(nameof(Attack), DynamicHelpers.GetPythonTypeFromType(typeof(Attack)));
			module.SetVariable(nameof(teams), teams);
			module.SetVariable("World", world);
		}

		private void AddEnvironmentEntities(List<Team> teams)
		{
			Requires.NotNull(teams, nameof(teams));

			List<Entity> Walls = new List<Entity>();
			this.EntityManager.Add(Statics.CreateTopWall());
			this.EntityManager.Add(Statics.CreateBottomWall());
			this.EntityManager.Add(Statics.CreateLeftWall());
			this.EntityManager.Add(Statics.CreateRightWall());

			foreach (Entity entity in GetCharactersFromMembers(teams))
			{
				this.EntityManager.Add(entity);
			}
		}

		protected override void Start()
		{
			base.Start();
		}

		private static IEnumerable<Entity> GetCharactersFromMembers(List<Team> teams)
		{
			Requires.NotNull(teams, nameof(teams));

			foreach (Team team in teams)
			{
				foreach (Member member in team.GetMembers().Array)
				{
					yield return Characters.CreateCharacter(member);
				}
			}
		}
	}
}
