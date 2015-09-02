using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleTeam.PythonComponents.Collections;
using BattleTeam.PythonComponents.Environment;
using BattleTeam.PythonComponents.Plays;

namespace BattleTeam.PythonComponents.Team
{
	/// <summary>
	/// A <see cref="Team"/> is what holds together Members. All <see cref="Member"/>s inside of a <see cref="Team"/> are suppose to work
	/// together to defeat another <see cref="Team"/>.
	/// </summary>
	public class Team
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Team"/> class.
		/// </summary>
		/// <param name="members">The <see cref="Member"/>s that are part of the <see cref="Team"/>.</param>
		public Team(params Member[] members)
		{
			if (members.Length > GetMaxTeamSize())
			{
				throw new ArgumentException("Exceeds " + nameof(GetMaxTeamSize), nameof(members));
			}

			foreach (Member member in members)
			{
				member.SetTeam(this);
			}

			this.members = new ImmutablePythonList<Member>(members.ToImmutableArray());
		}

		/// <summary>
		/// Gets the maximum number of <see cref="Member"/>s in a <see cref="Team"/>.
		/// </summary>
		/// <returns>The maximum number of <see cref="Member"/>s in one given <see cref="Team"/>.</returns>
		public static int GetMaxTeamSize() => 3;

		/// <summary>
		/// Gets all the <see cref="Member"/>s of the <see cref="Team"/>.
		/// </summary>
		/// <returns>An immutable array with the <see cref="Team"/>'s <see cref="Member"/>s.</returns>
		public ImmutablePythonList<Member> GetMembers() => this.members;

		internal void AddMessage(Message message)
		{
			this.messages.Add(message);
		}

		internal void ResolveMessages()
		{
			foreach (Message message in this.messages)
			{
				switch (message.GetSubject())
				{
					case Subject.Heal:
						{
							if (message.GetSender().GetClass() == Class.Healer)
							{
								message.GetReciever().Heal(2); // TODO: Heal amount is based on sender
							}

							break;
						}
				}
			}

			this.messages.Clear();
		}

		private readonly ImmutablePythonList<Member> members;
		private readonly List<Message> messages = new List<Message>();
	}
}
