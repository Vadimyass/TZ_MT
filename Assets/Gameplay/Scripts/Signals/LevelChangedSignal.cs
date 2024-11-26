namespace Gameplay.Scripts.Signals
{
    public class LevelChangedSignal
    {
        public readonly int Level;

        public LevelChangedSignal(int value)
        {
            Level = value;
        }
    }
}