using Microsoft.Xna.Framework;
using Mmc.MonoGame.Input.Conditions;

namespace Mmc.MonoGame.Input
{
    public class InputManager : IUpdateable
    {
        private Dictionary<Condition, Action> commandDictionary = [];

        private static readonly InputManager instance = new InputManager();

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public static InputManager Instance => instance;

        public bool Enabled { get; set; }

        public int UpdateOrder => 0;

        private InputManager()
        {
            Enabled = true;

            EnabledChanged += (s, e) => { };
            UpdateOrderChanged += (s, e) => { };
        }

        public void Update(GameTime gameTime) => Update();

        public void Update()
        {
            if (!Enabled) return;

            foreach (var input in commandDictionary)
            {
                if (input.Key.IsConditionMet()) input.Value();
            }
        }

        public void SetNewInput(Dictionary<Condition, Action> newCommandDictionary)
        {
            commandDictionary = newCommandDictionary;
        }

        public void ClearInputs()
        {
            commandDictionary = [];
        }
    }
}
