using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleTeam.PythonComponents.Team;
using Validation;

namespace BattleTeam.PythonComponents.Plays
{
	/// <summary>
	/// A <see cref="Message"/> is a storage unit for the method of communication between <see cref="Member"/>s
	/// </summary>
	public class Message
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Message"/> class.
		/// </summary>
		/// <param name="sender">The <see cref="Member"/> sending the <see cref="Message"/></param>
		/// <param name="reciever">The <see cref="Member"/> recieving the <see cref="Message"/></param>
		/// <param name="messageSubject">The <see cref="Subject"/> to be delivered</param>
		public Message(Member sender, Member reciever, Subject messageSubject)
		{
			Requires.NotNull(sender, nameof(sender));
			Requires.NotNull(reciever, nameof(reciever));

			this.Sender = sender;
			this.reciever = reciever;
			this.subject = messageSubject;
		}

		/// <summary>
		/// Gets the sender of the <see cref="Message"/>
		/// </summary>
		/// <returns>The <see cref="Member"/> who is sending the <see cref="Message"/></returns>
		public Member GetSender() => this.Sender;

		/// <summary>
		/// Gets the reciever of the <see cref="Message"/>
		/// </summary>
		/// <returns>The <see cref="Member"/> who is recieving the <see cref="Message"/></returns>
		public Member GetReciever() => this.reciever;

		/// <summary>
		/// Gets the <see cref="Subject"/> of the <see cref="Message"/>
		/// </summary>
		/// <returns>The <see cref="Subject"/></returns>
		public Subject GetSubject() => this.subject;

		internal Member Sender;
		private readonly Member reciever;
		private readonly Subject subject;
	}
}
