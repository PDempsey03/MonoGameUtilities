using Mmc.MonoGame.Collisions.GameTests.Scenes;

namespace Mmc.MonoGame.Collisions.GameTests
{
    public static class SceneManager
    {
        private static Scene _currentScene;

        public static void ChangeScene<T>() where T : Scene, new() => ChangeScene(new T());

        public static void ChangeScene(Scene newScene)
        {
            // exit old scene
            _currentScene?.OnExit();
            _currentScene?.UnloadContent();

            // switch scenes
            _currentScene = newScene;

            // enter new scene
            _currentScene?.Initialize();
            _currentScene?.LoadContent();
            _currentScene?.OnEnter();
        }

        public static void Update()
        {
            _currentScene?.Update();
        }

        public static void Draw()
        {
            _currentScene?.Draw();
        }
    }
}
