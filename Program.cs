using SharpHook;
using System.Timers;

public class Program
{
    static Input input;
    static Piano piano;

    private static void Main(string[] args)
    {
        input = new Input();
        piano = new Piano();

        input.OnKeyPressed += OnPressedKey;
        input.Start();
    }

    public static void OnPressedKey(string key)
    {
        if(key == "A")
        {
            piano.PlayNote(440, 1000);
        }
    }
}