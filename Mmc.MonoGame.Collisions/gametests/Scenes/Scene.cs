using Mmc.MonoGame.Input;
using Mmc.MonoGame.Input.Conditions;
using System;
using System.Collections.Generic;

namespace Mmc.MonoGame.Collisions.GameTests.Scenes
{
    public abstract class Scene
    {
        protected readonly Dictionary<Condition, Action> _commandDictionary = [];

        public abstract string Name { get; }

        public virtual void Initialize()
        {
            InitializeKeyBinds();
        }

        protected abstract void InitializeKeyBinds();

        public virtual void LoadContent() { }

        public virtual void UnloadContent() { }

        public virtual void Update()
        {
            InputManager.Instance.Update();
        }

        public abstract void Draw();

        public virtual void OnEnter()
        {
            InputManager.Instance.SetNewInput(_commandDictionary);
        }

        public virtual void OnExit()
        {
            InputManager.Instance.ClearInputs();
        }
    }
}
