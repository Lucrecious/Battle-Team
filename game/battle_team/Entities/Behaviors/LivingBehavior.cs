using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Framework;

namespace BattleTeam.Entities.Behaviors
{
	internal class LivingBehavior : Behavior
	{
		private bool isDead = false;

		internal void End()
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
