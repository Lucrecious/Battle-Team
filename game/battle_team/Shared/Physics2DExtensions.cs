using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Common.Math;
using WaveEngine.Framework.Physics2D;

namespace BattleTeam.Shared
{
	/// <summary>
	/// Gives a little more functionality and flexibility to the rectangle collider from the Wave Engine
	/// </summary>
	public static class Physics2DExtensions
	{
		/// <summary>
		/// Gets whether or not the rectangle colliders intersect
		/// </summary>
		/// <param name="rectCollider">The given collider</param>
		/// <param name="otherCollider">The other collider</param>
		/// <param name="mtv">
		/// The minimum translation vector, the smallest vector that can move <paramref name="otherCollider"/> out of
		/// <paramref name "rectCollider"/>.
		/// </param>
		/// <returns>true if there's an intersection, otherwise false</returns>
		public static bool Intersects(this RectangleCollider rectCollider, RectangleCollider otherCollider, out Vector2? mtv)
		{
			List<Vector2> testingAxes = new List<Vector2>();
			testingAxes.AddRange(GetTestingAxes(rectCollider));
			testingAxes.AddRange(GetTestingAxes(otherCollider));

			mtv = null;

			float smallestOverlap = float.MaxValue;
			Vector2 smallestOverlapAxis = Vector2.Zero;

			foreach (Vector2 axis in testingAxes)
			{
				Projection rectProjection = rectCollider.ProjectOn(axis);
				Projection otherProjection = otherCollider.ProjectOn(axis);
				float overlap;
				if (!rectProjection.GetOverlap(otherProjection, out overlap))
				{
					return false;
				}
				else
				{
					if (overlap < smallestOverlap)
					{
						smallestOverlap = overlap;
						smallestOverlapAxis = axis;
					}
				}
			}

			if (Vector2.Dot(rectCollider.Transform2D.Position - otherCollider.Transform2D.Position, smallestOverlapAxis) < 0)
			{
				smallestOverlapAxis = -smallestOverlapAxis;
			}

			mtv = smallestOverlap * smallestOverlapAxis;

			return true;
		}

		private static Projection ProjectOn(this RectangleCollider rectCollider, Vector2 vector)
		{
			float min = Vector2.Dot(rectCollider.GetPoints2D()[0], vector);
			float max = min;

			for (int i = 1; i < rectCollider.GetPoints2D().Length; ++i)
			{
				float dot = Vector2.Dot(rectCollider.GetPoints2D()[i], vector);
				min = Math.Min(dot, min);
				max = Math.Max(dot, max);
			}

			return new Projection(min, max);
		}

		// Since a rectangle is a type of parallelogram, I only need to get two axes - the perpendicular ones. In the
		//   more general case, I would get the normals of two adjecent edges.
		// This function assumes the "GetPoints2D()" method is ordered (first and second points are an edge, second and third are an edge, etc)
		private static IEnumerable<Vector2> GetTestingAxes(RectangleCollider rectCollider)
		{
			Vector2 edge1 = rectCollider.GetPoints2D()[1] - rectCollider.GetPoints2D()[0];
			Vector2 axis1 = edge1.Normal();
			axis1.Normalize();

			yield return axis1;

			Vector2 edge2 = rectCollider.GetPoints2D()[0] - rectCollider.GetPoints2D()[3];
			Vector2 axis2 = edge2.Normal();
			axis2.Normalize();

			yield return axis2;
		}

		/// <summary>
		/// Gets the normal of the given vector <paramref name="v1"/>.
		/// </summary>
		/// <param name="v1">The vector to get the normal of.</param>
		/// <returns>An arbitrary normal to <paramref name="v1"/>.</returns>
		public static Vector2 Normal(this Vector2 v1)
		{
			float x = v1.X;
			float y = v1.Y;

			return new Vector2(-y, x);
		}

		private struct Projection
		{
			internal float Min { get; }
			internal float Max { get; }

			internal Projection(float min, float max)
			{
				this.Min = min;
				this.Max = max;
			}

			internal bool GetOverlap(Projection projection, out float overlap)
			{
				overlap = Math.Min(this.Max, projection.Max) - Math.Max(this.Min, projection.Min);
				return overlap > 0;
			}
		}
	}
}
