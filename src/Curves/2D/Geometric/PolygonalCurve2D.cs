using Microsoft.Xna.Framework;
using Mmc.MonoGame.Utils.Curves._2D.Polynomial;

namespace Mmc.MonoGame.Utils.Curves._2D.Geometric
{
    public class PolygonalCurve2D : CompoundCurve2D
    {
        private Vector2[] _vertices = [];

        /// <summary>
        /// All verticies defining the polygon.
        /// </summary>
        public Vector2[] Vertices
        {
            set
            {
                _vertices = value;
                RebuildPolygon();
            }
        }

        /// <summary>
        /// Instantiate new instance of PolygonalCurve2D.
        /// </summary>
        /// <param name="vertices">All verticies defining the polygon.</param>
        public PolygonalCurve2D(params Vector2[] vertices)
        {
            Vertices = vertices ?? [];
            RebuildPolygon();
        }

        /// <summary>
        /// Removes old line segments and creates new ones based on the current vertices.
        /// </summary>
        protected virtual void RebuildPolygon()
        {
            Curves.Clear();

            int vertexCount = _vertices.Length;

            for (int i = 0; i < vertexCount; i++)
            {
                LinearCurve2D line = new LinearCurve2D(_vertices[i], _vertices[(i + 1) % (vertexCount)]);

                Curves.Add(line);
            }
        }
    }
}
