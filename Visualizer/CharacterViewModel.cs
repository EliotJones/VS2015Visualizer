using System.Text;

namespace Visualizer
{
    internal class CharacterViewModel
    {
        public string Character { get; }

        public int CharacterValue { get; }

        public string Byte { get; }

        public CharacterViewModel(char character)
        {
            Character = character.ToString();
            CharacterValue = character;
            Byte = string.Join(",", Encoding.UTF8.GetBytes(new[] {character}));
        }
    }
}