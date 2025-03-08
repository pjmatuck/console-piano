using NAudio.Wave;
using NAudio.Wave.SampleProviders;

public class Piano
{
    public void PlayNote(double frequency, int durationMs)
    {
        using var waveOut = new WaveOutEvent();
        var waveProvider = new SignalGenerator()
        {
            Gain = 0.2,
            Frequency = frequency,
            Type = SignalGeneratorType.Sin
        };

        waveOut.Init(waveProvider);
        waveOut.Play();
        Thread.Sleep(durationMs);
        waveOut.Stop();
    }
}