using Microsoft.Xna.Framework;
using Mmc.MonoGame.Utils.Curves._2D.Polynomial;

namespace Mmc.MonoGame.Utils.Curves._2D.Geometric
{
    public class QuadrilateralCurve2D : CompoundCurve2D
    {
        private Vector2 _vertex1;
        private Vector2 _vertex2;
        private Vector2 _vertex3;
        private Vector2 _vertex4;

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
                RebuildQuadrilateralCurve();
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
                RebuildQuadrilateralCurve();
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
                RebuildQuadrilateralCurve();
            }
        }

        /// <summary>
        /// Fourth vertex of the triangle.
        /// </summary>
        public Vector2 Vertex4
        {
            get => _vertex4;
            set
            {
                _vertex4 = value;
                RebuildQuadrilateralCurve();
            }
        }

        /// <summary>
        /// Instantiate new instance of QuadrilateralCurve2D.
        /// </summary>
        /// <param name="vertex1">First vertex of the quadtrilateral.</param>
        /// <param name="vertex2">Second vertex of the quadtrilateral.</param>
        /// <param name="vertex3">Third vertex of the quadtrilateral.</param>
        /// <param name="vertex4">Fourth vertex of the quadtrilateral.</param>
        public QuadrilateralCurve2D(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, Vector2 vertex4)
        {
            _vertex1 = vertex1;
            _vertex2 = vertex2;
            _vertex3 = vertex3;
            _vertex4 = vertex4;
            RebuildQuadrilateralCurve();
        }

        /// <summary>
        /// Removes old line segments and creates new ones based on the current values of Vertex 1-4.
        /// </summary>
        protected virtual void RebuildQuadrilateralCurve()
        {
            Curves.Clear();
            Curves.Add(new LinearCurve2D(Vertex1, Vertex2));
            Curves.Add(new LinearCurve2D(Vertex2, Vertex3));
            Curves.Add(new LinearCurve2D(Vertex3, Vertex4));
            Curves.Add(new LinearCurve2D(Vertex4, Vertex1));
        }
    }
}
