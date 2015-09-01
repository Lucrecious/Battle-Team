using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTeam.PythonComponents.Team
{
	/// <summary>
	/// The types of characters in the game with different abilities.
	/// </summary>
	public enum Class
	{
		/// <summary>
		/// The <see cref="Healer"/>'s speed is in between the <see cref="Gunner"/>'s and <see cref="Swordsman"/>.
		/// A <see cref="Healer"/> has no attack but can Heal other <see cref="Team"/> <see cref="Member"/>s.
		/// </summary>
		Healer,

		/// <summary>
		/// The <see cref="Gunner"/>'s speed is the lowest of the <see cref="Class"/>es.
		/// A <see cref="Gunner"/>'s attack is long-ranged.
		/// </summary>
		Gunner,

		/// <summary>
		/// The <see cref="Swordsman"/>'s speed is the highest of the <see cref="Class"/>es.
		/// A <see cref="Swordsman"/>'s attack is very short-ranged.
		/// </summary>
		Swordsman
	}
}
