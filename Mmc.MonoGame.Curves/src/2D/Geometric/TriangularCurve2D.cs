using Microsoft.Xna.Framework;
using Mmc.MonoGame.Curves._2D.Polynomial;

namespace Mmc.MonoGame.Curves._2D.Geometric
{
    public class TriangularCurve2D : CompoundCurve2D
    {
        private Vector2 _vertex1;
        private Vector2 _vertex2;
        private Vector2 _vertex3;

        public override bool IsSmooth => false;

        /// <summary>
        /// First vertex of the triangle.
        /// </summary>
        public Vector2 Vertex1
        {
            get => _vertex1;
            set
            {
                _vertex1 = value;
                RebuildTriangularCurve();
            }
        }

        /// <summary>
        /// Second vertex of the triangle.
        /// </summary>
        public Vector2 Vertex2
        {
            get => _vertex2;
            set
            {
                _vertex2 = value;
                RebuildTriangularCurve();
            }
        }

        /// <summary>
        /// Third vertex of the triangle.
        /// </summary>
        public Vector2 Vertex3
        {
            get => _vertex3;
            set
            {
                _vertex3 = value;
                RebuildTriangularCurve();
            }
        }

        /// <summary>
        /// Instantiate new instance of TriangularCurve2D.
        /// </summary>
        /// <param name="vertex1">First vertex of the triangle.</param>
        /// <param name="vertex2">Second vertex of the triangle.</param>
        /// <param name="vertex3">Third vertex of the triangle.</param>
        public TriangularCurve2D(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3)
        {
            _vertex1 = vertex1;
            _vertex2 = vertex2;
            _vertex3 = vertex3;
            RebuildTriangularCurve();
        }

        /// <summary>
        /// Removes old line segments and creates new ones based on the current values of Vertex 1-3.
        /// </summary>
        protected virtual void RebuildTriangularCurve()
        {
            Curves.Clear();
            Curves.Add(new LinearCurve2D(Vertex1, Vertex2));
            Curves.Add(new LinearCurve2D(Vertex2, Vertex3));
            Curves.Add(new LinearCurve2D(Vertex3, Vertex1));
        }
    }
}
