using System.Timers;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

public class Piano
{
    WaveOutEvent waveOutEvent;
    SignalGenerator signalGenerator;
    VolumeSampleProvider volumeSampleProvider;

    public async void PlayNote(double frequency)
    {
        await Task.Run(() => {
            waveOutEvent = new WaveOutEvent()
            {
                DesiredLatency = 100
            };
           
            signalGenerator = new SignalGenerator()
            {
                Frequency = frequency
            };

            volumeSampleProvider = new VolumeSampleProvider(signalGenerator);

            waveOutEvent.Init(volumeSampleProvider);
            waveOutEvent.Play();
        });
    }

    public async void StopNote()
    {
        float volume = 1f;

        while(volume > 0.01f)
        {
            volumeSampleProvider.Volume = volume;
            volume *= .92f;
            await Task.Delay(25);
        }

        waveOutEvent.Stop();
        waveOutEvent.Dispose();
    }
}