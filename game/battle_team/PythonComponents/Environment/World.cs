using System.Collections.Immutable;
using BattleTeam.PythonComponents.Collections;
using BattleTeam.PythonComponents.Objects;
using BattleTeam.PythonComponents.Team;
using Validation;
using WaveEngine.Common.Math;

namespace BattleTeam.PythonComponents.Environment
{
	/// <summary>
	/// <see cref="World"/> holds all the information the user needs to understand the
	/// environment of every <see cref="Team"/> <see cref="Member"/>
	/// </summary>
	public class World
	{
		/// <summary>
		/// Gets the top wall of the arena
		/// </summary>
		/// <returns>A rectangle representing the top wall bounds</returns>
		public RectangleF GetTopWall() => this.topWall;

		/// <summary>
		/// Gets the bottom wall of the arena
		/// </summary>
		/// <returns>A rectangle representing the bottom wall bounds</returns>
		public RectangleF GetBottomWall() => this.bottomWall;

		/// <summary>
		/// Gets the right wall of the arena
		/// </summary>
		/// <returns>A rectangle representing the right wall bounds</returns>
		public RectangleF GetRightWall() => this.rightWall;

		/// <summary>
		/// Gets the left wall of the arena
		/// </summary>
		/// <returns>A rectangle representing the left wall bounds</returns>
		public RectangleF GetLeftWall() => this.leftWall;

		/// <summary>
		/// An array holding all <see cref="Team"/> <see cref="Member"/>s in the current arena
		/// </summary>
		/// <returns>The array holding all <see cref="Team"/> <see cref="Member"/>s in the arena</returns>
		public ImmutablePythonList<Member> GetMembers() => this.members;

		internal ImmutablePythonList<Bullet> Bullets { get; set; }
		public ImmutablePythonList<Bullet> GetBullets() => this.Bullets;

		internal void Initialize(RectangleF topWall, RectangleF bottomWall, RectangleF rightWall, RectangleF leftWall, ImmutableArray<Member> members)
		{
			if (!this.initialized)
			{
				this.initialized = true;

				this.topWall = topWall;
				this.bottomWall = bottomWall;
				this.rightWall = rightWall;
				this.leftWall = leftWall;

				this.members = new ImmutablePythonList<Member>(members);
			}
		}

		private bool initialized = false;

		private RectangleF topWall;
		private RectangleF bottomWall;
		private RectangleF rightWall;
		private RectangleF leftWall;
		private ImmutablePythonList<Member> members;
	}
}
