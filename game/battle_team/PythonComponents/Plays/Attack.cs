using System.Collections.Generic;
using BattleTeam.PythonComponents.Team;
using WaveEngine.Common.Math;

namespace BattleTeam.PythonComponents.Plays
{
	/// <summary>
	/// The <see cref="IPlay"/> that represents an attack, when this is returned during a <see cref="Member.Turn"/> the
	/// <see cref="Member"/> does its attack
	/// Note: The <see cref="Class.Healer"/> has no attack, although <see cref="Class.Healer"/> can return this type of <see cref="IPlay"/>
	/// </summary>
	public class Attack : IPlay
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Attack"/> class.
		/// </summary>
		public Attack()
		{
			this.useAttack = true;
		}

		public bool IsUsingAttack() => this.useAttack;

		public Message GetMessage() => null;

		public Vector2 GetMoveDirection() => Vector2.Zero;

		public float GetRotation() => 0f;

		private readonly bool useAttack;
	}
}
