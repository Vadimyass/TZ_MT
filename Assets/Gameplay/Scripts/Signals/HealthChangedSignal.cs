namespace Gameplay.Scripts.Signals
{
    public class HealthChangedSignal
    {
        public readonly int Health;

        public HealthChangedSignal(int health)
        {
            Health = health;
        }
    }
}