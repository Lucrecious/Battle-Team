using System;
using System.Collections.Generic;
using BattleTeam.PythonComponents.Team;
using WaveEngine.Common.Math;

namespace BattleTeam.PythonComponents.Plays
{
	/// <summary>
	/// An <see cref="IPlay"/> that represents a rotation, when returned during a <see cref="Member.Turn"/>,
	/// the <see cref="Member"/> rotates
	/// </summary>
	public class Rotation : IPlay
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Rotation"/> class.
		/// </summary>
		/// <param name="angle">The angle to rotate the <see cref="Member"/> in radians</param>
		/// <param name="usingDegrees">Allows <paramref name="angle"/> to be represented in degrees when true</param>
		public Rotation(float angle, bool usingDegrees = false)
		{
			if (usingDegrees)
			{
				this.rotation = (angle * (float)Math.PI) / 180;
			}
			else
			{
				this.rotation = angle;
			}
		}

		public float GetRotation() => this.rotation;

		public Message GetMessage() => null;

		public Vector2 GetMoveDirection() => Vector2.Zero;

		public bool IsUsingAttack() => false;

		private readonly float rotation;
	}
}
