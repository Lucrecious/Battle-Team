using System.Collections.Generic;
using BattleTeam.PythonComponents.Team;
using Validation;
using WaveEngine.Common.Math;

namespace BattleTeam.PythonComponents.Plays
{
	/// <summary>
	/// The <see cref="IPlay"/> that represents a <see cref="Healing"/>, when returned during a <see cref="Member.Turn"/>,
	/// the "sender" heals the "reciever".
	/// Note: This <see cref="IPlay"/> is only avaliable on the <see cref="Class.Healer"/> <see cref="Class"/>
	/// </summary>
	public class Healing : IPlay
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Healing"/> class.
		/// </summary>
		/// <param name="reciever">The reciever of the <see cref="Healing"/></param>
		public Healing(Member reciever)
		{
			Requires.NotNull(reciever, nameof(reciever));
			this.message = new Message(null, reciever, Subject.Heal);
		}

		public Message GetMessage() => this.message;

		public Vector2 GetMoveDirection() => Vector2.Zero;

		public float GetRotation() => 0f;

		public bool IsUsingAttack() => false;

		private readonly Message message;
	}
}
