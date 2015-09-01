using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleTeam.Entities;
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

		internal ArenaEnvironment()
		{
			this.TopWall = Statics.CreateTopWall();
			this.BottomWall = Statics.CreateBottomWall();
			this.RightWall = Statics.CreateRightWall();
			this.LeftWall = Statics.CreateLeftWall();

			this.Walls = ImmutableArray.Create(this.TopWall, this.BottomWall, this.RightWall, this.LeftWall);
		}
	}
}
