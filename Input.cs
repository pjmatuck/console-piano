using SharpHook;

public class Input
{
    readonly Dictionary<string, System.Timers.Timer> keyTimers = new();

    public event Action<string> OnKeyPressed;
    public event Action<string> OnKeyHold;
    public event Action<string> OnKeyReleased;
    TaskPoolGlobalHook hook;

    public Input()
    {
        hook = new TaskPoolGlobalHook();
    }

    public void Start()
    {
        if(hook == null)
            throw new ArgumentNullException("Hook must not be null.");

        hook.KeyPressed += (s, e) => {

            var key = e.Data.KeyCode.ToString().Replace("Vc","");

            if(keyTimers.ContainsKey(key))
                return;

            var timer = new System.Timers.Timer(2000);
            timer.Elapsed += (s, a) => OnKeyHeld(key);
            keyTimers[key] = timer;
            timer.Start();

            Console.WriteLine($"Key pressed {key}");
            OnKeyPressed?.Invoke(key);
        };

        hook.KeyReleased += (s, e) => {

            var key = e.Data.KeyCode.ToString().Replace("Vc","");

            if(keyTimers.ContainsKey(key))
            {
                keyTimers[key].Stop();
                keyTimers[key].Dispose();
                keyTimers.Remove(key);
            }

            Console.WriteLine($"Key released {key}");
            OnKeyReleased?.Invoke(key);
        };

        hook.RunAsync();
    }

    private void OnKeyHeld(string key)
    {
        Console.WriteLine($"Key hold {key}");
        OnKeyHold?.Invoke(key);
    }

    public void DisposeInput()
    {
        hook.Dispose();
    }
}