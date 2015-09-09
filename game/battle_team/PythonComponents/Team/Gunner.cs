using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleTeam.PythonComponents.Environment;

namespace BattleTeam.PythonComponents.Team
{
	/// <summary>
	/// The <see cref="Class.Gunner"/> as described in <see cref="Class"/>.
	/// </summary>
	public class Gunner : Member
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Gunner"/> class.
		/// </summary>
		/// <param name="displayName">The name that will show over the <see cref="Member"/>'s head.</param>
		public Gunner(string displayName) : base(Class.Gunner, displayName)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Gunner"/> class.
		/// A default display name is assigned.
		/// </summary>
		public Gunner() : base(Class.Gunner, nameof(Gunner))
		{
		}

		internal override float Speed => 20f;
	}
}
