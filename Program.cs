using SharpHook;
using System.Timers;

public class Program
{
    static Input input;
    static Piano piano;

    static List<string> playedNotes = new();

    private static void Main(string[] args)
    {
        input = new Input();
        piano = new Piano();

        input.OnKeyPressed += OnPressedKey;
        input.OnKeyReleased += OnKeyReleased;
        input.Start();
    }

    public static void OnPressedKey(string key)
    {
        if(key == "A")
        {
            playedNotes.Add(key);
            piano.PlayNote(261);
        }
    }

    public static void OnKeyReleased(string key)
    {
        if(playedNotes.Contains(key))
        {
            piano.StopNote();
            playedNotes.Remove(key);
        }
    }
}