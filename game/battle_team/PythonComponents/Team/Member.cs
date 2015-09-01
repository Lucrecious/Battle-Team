using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleTeam.PythonComponents.Environment;
using BattleTeam.PythonComponents.Plays;
using BattleTeam.Shared;
using WaveEngine.Common.Math;

namespace BattleTeam.PythonComponents.Team
{
	/// <summary>
	/// The general character type is a <see cref="Member"/>, this is where all the <see cref="Class"/>es derive from.
	/// </summary>
	public abstract class Member
	{
		internal Member(Class @class, string displayName)
		{
			this.@class = @class;
			this.displayName = displayName;

			this.currentHealth = this.GetMaxHealth();
		}

		/// <summary>
		/// A <see cref="Turn"/> is what a <see cref="Member"/> does at a given frame in the game environment.
		/// A <see cref="Member"/> can only make one <see cref="IPlay"/> per turn.
		/// <see cref="Member"/> <see cref="Turn"/>s happen in arbitrary order.
		/// No <see cref="Member"/> should ever be ahead by more than one <see cref="Turn"/>, in other words, each frame
		/// all <see cref="Member"/>s get only one <see cref="Turn"/>.
		/// </summary>
		/// <returns>A <see cref="IPlay"/> is returned representing what the <see cref="Member"/> is going to do.</returns>
		public virtual IPlay Turn()
		{
			return null;
		}

		/// <summary>
		/// Gets the speed of the <see cref="Member"/>.
		/// </summary>
		/// <returns>The maximum amount of units a <see cref="Member"/> can move during any given <see cref="Turn"/>.</returns>
		public virtual float GetSpeed() => 2f;

		/// <summary>
		/// Gets the <see cref="Class"/> of the <see cref="Member"/>.
		/// </summary>
		/// <returns>The <see cref="Class"/>.</returns>
		public Class GetClass() => this.@class;

		/// <summary>
		/// Gets the display name of the <see cref="Member"/>.
		/// </summary>
		/// <returns>The display name.</returns>
		public string GetDisplayName() => this.displayName;

		/// <summary>
		/// Gets the maximum health a <see cref="Member"/> can have.
		/// </summary>
		/// <returns>
		/// An integer representing the maximum amount of hits avaliable before above death (zero).
		/// </returns>
		public int GetMaxHealth() => 10;

		/// <summary>
		/// Gets the current health of the <see cref="Member"/>.
		/// </summary>
		/// <returns>
		/// An integer representing the current amount of hits avaliable before death (zero).
		/// </returns>
		public int GetHealth() => this.currentHealth;

		/// <summary>
		/// Gets the current position of the <see cref="Member"/> on the 2D plane.
		/// </summary>
		/// <returns>A <see cref="Vector2"/> with components corresponding to the 2D coordinates of the <see cref="Member"/>.</returns>
		public Vector2 GetPosition() => this.position;

		/// <summary>
		/// Gets the current orientation of the <see cref="Member"/>.
		/// </summary>
		/// <returns>A radian value.</returns>
		public float GetRotation() => this.rotation;

		/// <summary>
		/// Gets the rectangle representing the <see cref="Member"/>'s collision box.
		/// </summary>
		/// <returns>A rectangle with the top-left as its X, Y.</returns>
		public RectangleF GetRectangle() => this.rectangle;

		/// <summary>
		/// Gets the <see cref="Team"/> the <see cref="Member"/> is apart of.
		/// </summary>
		/// <returns>A <see cref="Team"/>.</returns>
		public Team GetTeam() => this.team;

		internal void Heal(int healAmount)
		{
			this.currentHealth = Utilities.Bound(this.currentHealth + healAmount, 0, this.GetMaxHealth());
		}

		internal void SetPosition(Vector2 position)
		{
			this.position = position;
		}

		internal void SetRotation(float angle)
		{
			this.rotation = angle;
		}

		internal void SetRectangle(RectangleF rectangle)
		{
			this.rectangle = rectangle;
		}

		internal void SetTeam(Team team)
		{
			if (this.team == null)
			{
				this.team = team;
			}
			else
			{
				throw new InvalidProgramException("Can't use this twice");
			}
		}

		private readonly Class @class;
		private readonly string displayName;
		private int currentHealth;
		private Vector2 position;
		private float rotation;
		private RectangleF rectangle;
		private Team team;
	}
}
