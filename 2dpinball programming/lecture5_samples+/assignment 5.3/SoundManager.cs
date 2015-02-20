using System;
using System.Collections.Generic;
using System.Timers;
using System.IO;

namespace GXPEngine
{
	public class SoundManager
	{

		static SoundChannel musicChannel = new SoundChannel(0);
		static SoundChannel musicChannel2 = new SoundChannel(1);
		static SoundChannel soundChannel = new SoundChannel(2);
		private static Timer Fadetimer;
		private static Timer FadeIntimer;

		public delegate void AfterFadeout(Object sender, EventArgs e);
		static public event AfterFadeout fadeoutevent;



		public static void Playsound(SoundEffect value, float volume = 1f, float channel = 0f)
		{
		
		Sound sound = new Sound (SoundDictionary [value]);
		soundChannel = sound.Play();
		soundChannel.Volume = volume;
		soundChannel.Pan = channel;

		}



		public static void Playmusic(String soundfile)
		{
			Sound sound = new Sound (soundfile,true,true);
			musicChannel = sound.Play ();
			musicChannel.Volume = 0f;
			if (FadeIntimer != null) {
				FadeIntimer.Dispose ();
			}
			FadeIntimer = new Timer (500);
			FadeIntimer.Elapsed += OnTimedEventStart;
			FadeIntimer.Start ();

		}

		public static void OnTimedEventStart(Object source, ElapsedEventArgs e){
			if (musicChannel.Volume < 1) {
				musicChannel.Volume += 0.1f;
			} else {
				FadeIntimer.Stop ();
				FadeIntimer.Dispose ();
			}
		}
		public static void OnTimedEvent(Object source, ElapsedEventArgs e)
		{
			musicChannel.Volume -= 0.1f;
			if (musicChannel.Volume <= 0) {
				Fadetimer.Stop ();
				Fadetimer.Dispose ();
				musicChannel.Stop ();
				fadeoutevent.Invoke (source, e);

			}
		}

		public static void StopMusic(bool fadeout = false)
		{	
			if (fadeout) {
				Fadetimer = new Timer (500);
				Fadetimer.Elapsed += OnTimedEvent;
				Fadetimer.Start ();
				//FadeIntimer.Stop ();
			} else {
				musicChannel.Stop ();
			}
		}

		public static void StopSound()
		{
			soundChannel.Stop ();
		}

		private static readonly Dictionary <SoundEffect,string> SoundDictionary
		= new Dictionary <SoundEffect,string> {

			{ SoundEffect.bounce, "bounce.wav" },
			{ SoundEffect.bounce2, "bounce2.wav" },
			{ SoundEffect.bounce3,"bounce3.wav" },
			{ SoundEffect.enemyhit,"enemyhit.wav" },
			{ SoundEffect.enemyhit2,"enemyhit2.wav" },
			{ SoundEffect.gameover, "gameover.wav" },
			{ SoundEffect.nyan,"nyan.wav" }
		
		};
			


	}

	public enum SoundEffect
	{	
		nyan,
		bounce,
		bounce2,
		bounce3,
		enemyhit,
		enemyhit2,
		gameover
	}

}

