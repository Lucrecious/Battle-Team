using System;
using System.Collections.Immutable;

namespace BattleTeam.PythonComponents.Collections
{
	/// <summary>
	/// The <see cref="ImmutableArray"/> object doesn't work in Python since in Python everything is mutable, so this a wrapper
	/// that prevents the user from changing/adding/removing list values
	/// </summary>
	/// <typeparam name="T">Any object type for the immutable list</typeparam>
	public class ImmutablePythonList<T>
	{
		/// <summary>
		/// Gets the item at given <paramref name="index"/>
		/// </summary>
		/// <param name="index">The index the item is at</param>
		/// <returns>The item</returns>
		public T Get(int index)
		{
			return this.immutableArray[index];
		}

		/// <summary>
		/// Gets the length of the array
		/// </summary>
		/// <returns>The length of the array</returns>
		public int GetLength()
		{
			return this.immutableArray.Length;
		}

		internal ImmutablePythonList(ImmutableArray<T> immutableArray)
		{
			this.immutableArray = immutableArray;
		}

		internal ImmutableArray<T> Array => this.immutableArray;

		private readonly ImmutableArray<T> immutableArray;
	}
}
