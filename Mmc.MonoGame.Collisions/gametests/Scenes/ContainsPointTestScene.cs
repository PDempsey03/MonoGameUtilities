using Microsoft.Xna.Framework;
using Mmc.MonoGame.Collisions.CollisionShapes;
using Mmc.MonoGame.Input;
using Mmc.MonoGame.Input.Conditions;
using System;

namespace Mmc.MonoGame.Collisions.GameTests.Scenes
{
    public class ContainsPointTestScene : Scene
    {
        public override string Name => "Contains Point Test";

        private ICollisionShape[] _collisionShapes;
        private Color[] _colors;

        public override void Initialize()
        {
            base.Initialize();

            _collisionShapes =
            [
                new CircleCollisionShape(new Vector2(500, 500), 50),
                new RectangleCollisionShape(new Vector2(300, 300), new Vector2(100, 200), MathF.PI / 4f),
                //new CapsuleCollisionShape(),
            ];

            _colors =
            [
                Color.Red,
                Color.Red,
                //Color.Red,
            ];
        }

        protected override void InitializeKeyBinds()
        {
            _commandDictionary.Add(new MouseButtonCondition(MouseButton.Left, InputType.Pressed),
                () => UpdateColors(InputHelper.NewMousePosition.ToVector2()));
        }

        public override void Draw()
        {
            for (int i = 0; i < _collisionShapes.Length; i++)
            {
                ICollisionShape shape = _collisionShapes[i];

                switch (shape)
                {
                    case CircleCollisionShape circle:
                        Drawer.DrawCircle(circle.Position, circle.Radius, _colors[i], 50, 1, 0, MathF.Tau);
                        break;

                    case RectangleCollisionShape rectangle:
                        var corners = rectangle.GetCorners();
                        Drawer.DrawRectangle(corners[0], corners[1], corners[2], corners[3], _colors[i], 1);
                        break;

                    case CapsuleCollisionShape capsule:

                        break;
                }
            }
        }

        private void UpdateColors(Vector2 newMousePosition)
        {
            for (int i = 0; i < _collisionShapes.Length; i++)
            {
                bool containsPoint = _collisionShapes[i].ContainsPoint(newMousePosition);
                _colors[i] = containsPoint ? Color.Green : Color.Red;
            }
        }
    }
}
