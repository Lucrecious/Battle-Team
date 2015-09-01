using System.Collections.Generic;
using BattleTeam.PythonComponents.Team;
using WaveEngine.Common.Math;

namespace BattleTeam.PythonComponents.Plays
{
	/// <summary>
	/// An <see cref="IPlay"/> represents what a <see cref="Member"/> can do during a <see cref="Member.Turn"/>.
	/// </summary>
	public interface IPlay
	{
		/// <summary>
		/// Gets the direction of movement for the <see cref="IPlay"/>
		/// </summary>
		/// <returns>A normalized vector representing the move direction</returns>
		Vector2 GetMoveDirection();

		/// <summary>
		/// Gets the rotation for the <see cref="IPlay"/>
		/// </summary>
		/// <returns>An Euler rotation in radians</returns>
		float GetRotation();

		/// <summary>
		/// Gets the <see cref="Message"/> for the <see cref="Team"/> for the <see cref="IPlay"/>
		/// </summary>
		/// <returns>The <see cref="Message"/></returns>
		Message GetMessage();

		/// <summary>
		/// Gets whether or not the <see cref="IPlay"/> is an attack
		/// </summary>
		/// <returns>true if the <see cref="IPlay"/> is an attack, otherwise, false</returns>
		bool IsUsingAttack();
	}
}
