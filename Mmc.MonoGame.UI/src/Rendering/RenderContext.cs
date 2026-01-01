using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mmc.MonoGame.UI.Rendering
{
    public sealed class RenderContext
    {
        public SpriteBatch SpriteBatch { get; init; }

        public SpriteSortMode SpriteSortMode { get; set; }
        public BlendState BlendState { get; init; }
        public SamplerState SamplerState { get; init; }
        public DepthStencilState DepthStencilState { get; init; }
        public RasterizerState RasterizerState { get; init; }

        public RenderContext(SpriteBatch spriteBatch, SpriteSortMode sort, BlendState blend, SamplerState sampler, DepthStencilState depth, RasterizerState rast)
        {
            SpriteBatch = spriteBatch;
            SpriteSortMode = sort;
            BlendState = blend;
            SamplerState = sampler;
            DepthStencilState = depth;
            RasterizerState = rast;
        }

        public void Begin()
        {
            SpriteBatch.Begin(SpriteSortMode, BlendState, SamplerState, DepthStencilState, RasterizerState);
        }

        public void End()
        {
            SpriteBatch.End();
        }

        /// <summary>
        /// Begins a new spritebatch run with a new scissor rectangle for clipping.
        /// </summary>
        /// <param name="newScissorRectangle">the new scissor rectangle to use.</param>
        /// <returns>previous scissor rectangle.</returns>
        public Rectangle RestartWithNewScissorRectangle(Rectangle newScissorRectangle)
        {
            var previousScissorRectangle = SpriteBatch.GraphicsDevice.ScissorRectangle;

            End(); // flush out previous draw calls
            SpriteBatch.GraphicsDevice.ScissorRectangle = newScissorRectangle;
            Begin();

            return previousScissorRectangle;
        }

        /// <summary>
        /// Calculates new scissor bounds based on parameter and current scissor bounds.
        /// </summary>
        /// <param name="newClippingBounds">new rectangle clipping bounds.</param>
        /// <returns>Intersectin of the current and new clipping bounds</returns>
        public Rectangle CalculateNewScissorRectangle(Rectangle newClippingBounds)
        {
            return Rectangle.Intersect(newClippingBounds, SpriteBatch.GraphicsDevice.ScissorRectangle);
        }
    }
}
