using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleTeam.PythonComponents.Team;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;

namespace BattleTeam.Entities.Behaviors
{
	internal class GunnerBehavior : CharacterBehavior
	{
		private static readonly Vector2 RelativeShootingPoint = new Vector2(6, 7);

		internal GunnerBehavior(Member member) : base(member)
		{
		}

		[RequiredComponent]
		private readonly Transform2D trans2D = null;

		[RequiredComponent]
		private readonly RectangleCollider rectangleCollider = null;

		protected override RectangleCollider RectangleCollider => this.rectangleCollider;
		protected override Transform2D Trans2D => this.trans2D;

		internal override void UseAttack()
		{
		}
	}
}
