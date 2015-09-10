using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleTeam.Entities;
using BattleTeam.PythonComponents.Team;
using Validation;
using WaveEngine.Framework;

namespace BattleTeam.Scenes.Arena
{
	internal class ArenaEnvironment
	{
		internal Entity TopWall { get; }
		internal Entity BottomWall { get; }
		internal Entity RightWall { get; }
		internal Entity LeftWall { get; }

		internal ImmutableArray<Entity> Walls { get; }

		internal ImmutableArray<Entity> Characters { get; }

		internal ArenaEnvironment(List<Team> teams)
		{
			Requires.NotNull(teams, nameof(teams));

			this.TopWall = Statics.CreateTopWall();
			this.BottomWall = Statics.CreateBottomWall();
			this.RightWall = Statics.CreateRightWall();
			this.LeftWall = Statics.CreateLeftWall();

			this.Walls = ImmutableArray.Create(this.TopWall, this.BottomWall, this.RightWall, this.LeftWall);

			this.Characters = this.GetCharactersFromMembers(teams);
		}

		private ImmutableArray<Entity> GetCharactersFromMembers(List<Team> teams)
		{
			Requires.NotNull(teams, nameof(teams));

			List<Entity> characters = new List<Entity>();

			foreach (Team team in teams)
			{
				if (team.GetMembers().GetLength() > 0)
				{
					for (int i = 0; i < team.GetMembers().GetLength(); ++i)
					{
						characters.Add(Entities.Characters.CreateCharacter(team.GetMembers().Get(i)));
                    }
				}
			}

			return characters.ToImmutableArray();
		}
	}
}
