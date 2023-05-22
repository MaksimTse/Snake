using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.ComponentModel;
using System.Numerics;
using NAudio;
using NAudio.Wave;

namespace Snake
{
    public class Sounds
    {
        private IWavePlayer waveOutDevice;
        private AudioFileReader audioFileReader;
        private string pathToMedia;
        private Sounds sounds;
        public Sounds(string pathToResources)
        {
            pathToMedia = pathToResources;
            waveOutDevice = new WaveOutEvent();
            audioFileReader = new AudioFileReader(pathToMedia + "/Background_Music.mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }

        public void PlayBack()
        {
            waveOutDevice.Stop();
            audioFileReader = new AudioFileReader(pathToMedia + "/Background_Music.mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }

        public void PlayEat()
        {
            waveOutDevice.Stop();
            audioFileReader = new AudioFileReader(pathToMedia + "/Snack.mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }

        public void PlayGameOver()
        {
            waveOutDevice.Stop();
            audioFileReader = new AudioFileReader(pathToMedia + "/GameOver.mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }

        public void Stop()
        {
            waveOutDevice.Stop();
        }

        public void Dispose()
        {
            waveOutDevice.Dispose();
            audioFileReader.Dispose();
        }
    }
}