using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Snake
{
    public class Music
    {
        public async Task Background_music(String Path)
        {
            await Task.Run(() =>
            {
                using (AudioFileReader audiofileReader = new AudioFileReader(Path))
                using (IWavePlayer waveOutDevice = new WaveOutEvent { DesiredLatency = 200, Volume = 50})
                {
                    waveOutDevice.Init(audiofileReader);
                    waveOutDevice.Play();
                    while(waveOutDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(1000);
                    }
                }
            });
        }

        public async Task Music_Empty(String Path)
        {
            await Task.Run(() =>
            {
                using (AudioFileReader audiofileReader = new AudioFileReader(Path))
                using (IWavePlayer waveOutDevice = new WaveOutEvent())
                {
                    waveOutDevice.Init(audiofileReader);
                    waveOutDevice.Play();
                    while (waveOutDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(50);
                    }
                }
            });
        }
    }
}