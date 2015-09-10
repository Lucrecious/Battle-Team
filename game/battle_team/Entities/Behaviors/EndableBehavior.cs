using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Framework;

namespace BattleTeam.Entities.Behaviors
{
	internal class EndableBehavior : Behavior
	{
		private bool isDead = false;

		internal void MarkAsDead()
		{
			this.isDead = true;
		}

		protected override void Update(TimeSpan gameTime)
		{
			if (this.isDead)
			{
				this.EntityManager.Remove(this.Owner);
			}
		}
	}
}
