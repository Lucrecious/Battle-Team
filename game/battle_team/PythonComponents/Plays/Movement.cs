using System.Collections.Generic;
using BattleTeam.PythonComponents.Team;
using WaveEngine.Common.Math;

namespace BattleTeam.PythonComponents.Plays
{
	/// <summary>
	/// A <see cref="IPlay"/> that represents a movement for the <see cref="Member.Turn"/>, when
	/// returned during a <see cref="Member.Turn"/> this will cause the corresponding <see cref="Member"/> to move
	/// </summary>
	public class Movement : IPlay
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Movement"/> class.
		/// </summary>
		/// <param name="x">The amount of movement in the x direction</param>
		/// <param name="y">The amount of movement in the y direction</param>
		public Movement(float x, float y)
		{
			if (x == 0 && y == 0)
			{
				this.moveDirection = Vector2.Zero;
			}
			else
			{
				var vector = new Vector2(x, y);
				vector.Normalize();

				this.moveDirection = vector;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Movement"/> class.
		/// </summary>
		/// <param name="direction">A vector that represents the direction of movement</param>
		public Movement(Vector2 direction)
		{
			if (direction.X == 0 && direction.Y == 0)
			{
				this.moveDirection = Vector2.Zero;
			}
			else
			{
				direction.Normalize();
				this.moveDirection = direction;
			}
		}

		public Vector2 GetMoveDirection() => this.moveDirection;

		public float GetRotation() => 0f;

		public Message GetMessage() => null;

		public bool IsUsingAttack() => false;

		private readonly Vector2 moveDirection;
	}
}
