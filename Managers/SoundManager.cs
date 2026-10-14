using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Managers
{
    /// <summary>
    /// Quản lý phát nhạc nền và hiệu ứng âm thanh bằng System.Media.SoundPlayer.
    /// Tự động tổng hợp nhạc nền BGM đa âm sắc (Polyphonic Chiptune) phong phú,
    /// bài nhạc Menu, bài nhạc Gameplay hấp dẫn, âm hiệu ứng va chạm zombie, nhạc chiến thắng/thua cuộc!
    /// (FR-30 đến FR-37).
    /// </summary>
    public static class SoundManager
    {
        private static readonly Dictionary<string, string> _soundFiles = new Dictionary<string, string>();
        private static SoundPlayer? _bgmPlayer;
        private static string? _currentBgmName;

        static SoundManager()
        {
            EnsureSoundsExist(forceRegenerate: true);
        }

        public static void EnsureSoundsExist(bool forceRegenerate = false)
        {
            try
            {
                if (!Directory.Exists(GameConfig.SOUNDS_DIR))
                {
                    Directory.CreateDirectory(GameConfig.SOUNDS_DIR);
                }

                // Kiểm tra thư mục bin output
                string binSoundsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Sounds");
                if (!Directory.Exists(binSoundsDir))
                {
                    Directory.CreateDirectory(binSoundsDir);
                }

                string[] soundNames = {
                    "bgm_menu.wav", "bgm_game.wav", "sfx_plant.wav", "sfx_shoot.wav",
                    "sfx_hit.wav", "sfx_sun.wav", "sfx_explode.wav", "sfx_zombie_eat.wav",
                    "sfx_zombie_die.wav", "sfx_wave_alert.wav", "sfx_win.wav", "sfx_lose.wav", "sfx_click.wav"
                };

                foreach (string name in soundNames)
                {
                    string path1 = Path.Combine(GameConfig.SOUNDS_DIR, name);
                    string path2 = Path.Combine(binSoundsDir, name);

                    if (forceRegenerate || !File.Exists(path1) || new FileInfo(path1).Length < 20000)
                    {
                        byte[] wavBytes = GenerateAdvancedWavData(name);
                        File.WriteAllBytes(path1, wavBytes);
                        File.WriteAllBytes(path2, wavBytes);
                    }

                    _soundFiles[name.Replace(".wav", "")] = path2;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi khởi tạo hoặc tổng hợp âm thanh trong SoundManager", ex);
            }
        }

        public static void PlaySound(string soundName)
        {
            if (!SettingsManager.Instance.SoundEnabled) return;

            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    string key = soundName.Replace(".wav", "");
                    if (_soundFiles.TryGetValue(key, out string? path) && File.Exists(path))
                    {
                        using (var sp = new SoundPlayer(path))
                        {
                            sp.Play();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Lỗi phát âm thanh {soundName}", ex);
                }
            });
        }

        public static void PlayMusic(string musicName)
        {
            if (!SettingsManager.Instance.MusicEnabled)
            {
                StopMusic();
                return;
            }

            string key = musicName.Replace(".wav", "");
            if (_currentBgmName == key && _bgmPlayer != null) return;

            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    StopMusic();
                    if (_soundFiles.TryGetValue(key, out string? path) && File.Exists(path))
                    {
                        _bgmPlayer = new SoundPlayer(path);
                        _bgmPlayer.PlayLooping();
                        _currentBgmName = key;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Lỗi phát nhạc nền {musicName}", ex);
                }
            });
        }

        public static void StopMusic()
        {
            try
            {
                if (_bgmPlayer != null)
                {
                    _bgmPlayer.Stop();
                    _bgmPlayer.Dispose();
                    _bgmPlayer = null;
                    _currentBgmName = null;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi dừng nhạc nền", ex);
            }
        }

        public static void ToggleSound()
        {
            SettingsManager.Instance.SoundEnabled = !SettingsManager.Instance.SoundEnabled;
            SettingsManager.Instance.SaveSettings();
        }

        public static void ToggleMusic()
        {
            SettingsManager.Instance.MusicEnabled = !SettingsManager.Instance.MusicEnabled;
            if (!SettingsManager.Instance.MusicEnabled)
            {
                StopMusic();
            }
            else
            {
                PlayMusic("bgm_menu");
            }
            SettingsManager.Instance.SaveSettings();
        }

        #region Bộ Tổng Hợp Âm Thanh Đa Âm (Polyphonic Audio Synthesizer)

        private static double NoteToFreq(int midiNote)
        {
            return 440.0 * Math.Pow(2.0, (midiNote - 69) / 12.0);
        }

        private static byte[] GenerateAdvancedWavData(string name)
        {
            int sampleRate = 22050;
            double duration = 1.0;

            if (name == "bgm_menu.wav") duration = 12.0;
            else if (name == "bgm_game.wav") duration = 16.0;
            else if (name == "sfx_win.wav") duration = 4.0;
            else if (name == "sfx_lose.wav") duration = 3.5;
            else if (name == "sfx_explode.wav") duration = 0.8;
            else duration = 0.25;

            int totalSamples = (int)(sampleRate * duration);
            byte[] pcmData = new byte[totalSamples * 2];

            if (name == "bgm_menu.wav")
            {
                SynthesizeMenuBgm(pcmData, totalSamples, sampleRate);
            }
            else if (name == "bgm_game.wav")
            {
                SynthesizeGameBgm(pcmData, totalSamples, sampleRate);
            }
            else if (name == "sfx_win.wav")
            {
                SynthesizeWinFanfare(pcmData, totalSamples, sampleRate);
            }
            else if (name == "sfx_lose.wav")
            {
                SynthesizeLoseJingle(pcmData, totalSamples, sampleRate);
            }
            else if (name == "sfx_hit.wav")
            {
                SynthesizeHitSfx(pcmData, totalSamples, sampleRate);
            }
            else if (name == "sfx_shoot.wav")
            {
                SynthesizeShootSfx(pcmData, totalSamples, sampleRate);
            }
            else if (name == "sfx_sun.wav")
            {
                SynthesizeSunSfx(pcmData, totalSamples, sampleRate);
            }
            else if (name == "sfx_explode.wav")
            {
                SynthesizeExplodeSfx(pcmData, totalSamples, sampleRate);
            }
            else
            {
                SynthesizeGenericSfx(name, pcmData, totalSamples, sampleRate);
            }

            return WrapPcmInWavHeader(pcmData, sampleRate);
        }

        private static void SynthesizeMenuBgm(byte[] pcm, int numSamples, int sampleRate)
        {
            // Nhạc Menu vui tươi, giai điệu C major (C-G-Am-F)
            int[] melodyNotes = { 60, 64, 67, 72, 71, 67, 64, 62, 60, 64, 67, 72, 74, 72, 71, 67 };
            int[] bassNotes = { 36, 36, 43, 43, 45, 45, 41, 41 };

            double beatDuration = 0.375; // 160 BPM

            for (int i = 0; i < numSamples; i++)
            {
                double t = (double)i / sampleRate;

                // Melody Track
                int mIdx = ((int)(t / beatDuration)) % melodyNotes.Length;
                double mFreq = NoteToFreq(melodyNotes[mIdx]);
                double mEnv = 1.0 - ((t % beatDuration) / beatDuration) * 0.4;
                double wave1 = Math.Sin(2 * Math.PI * mFreq * t) * 0.4 * mEnv;

                // Harmony Track (3rds above)
                double hFreq = NoteToFreq(melodyNotes[mIdx] + 4);
                double wave2 = Math.Sin(2 * Math.PI * hFreq * t) * 0.25 * mEnv;

                // Bass Track
                int bIdx = ((int)(t / (beatDuration * 2))) % bassNotes.Length;
                double bFreq = NoteToFreq(bassNotes[bIdx]);
                double wave3 = ((t * bFreq % 1.0) < 0.5 ? 1.0 : -1.0) * 0.3; // Square wave bass

                short sample = (short)((wave1 + wave2 + wave3) * 10000);
                pcm[i * 2] = (byte)(sample & 0xFF);
                pcm[i * 2 + 1] = (byte)((sample >> 8) & 0xFF);
            }
        }

        private static void SynthesizeGameBgm(byte[] pcm, int numSamples, int sampleRate)
        {
            // Nhạc chiến đấu sôi động (A minor funky battle theme - A-F-Dm-E7)
            int[] melodyNotes = { 69, 72, 76, 74, 72, 71, 69, 67, 69, 72, 76, 77, 76, 74, 72, 71 };
            int[] bassNotes = { 45, 45, 41, 41, 38, 38, 40, 40 };

            double beatDuration = 0.25; // 240 BPM (nhanh, hồi hộp)

            for (int i = 0; i < numSamples; i++)
            {
                double t = (double)i / sampleRate;

                // Lead Giai điệu
                int mIdx = ((int)(t / beatDuration)) % melodyNotes.Length;
                double mFreq = NoteToFreq(melodyNotes[mIdx]);
                double mEnv = Math.Max(0, 1.0 - ((t % beatDuration) / beatDuration));
                double lead = (Math.Sin(2 * Math.PI * mFreq * t) + 0.3 * Math.Sin(4 * Math.PI * mFreq * t)) * 0.4 * mEnv;

                // Bassline nhịp nhàng
                int bIdx = ((int)(t / (beatDuration * 2))) % bassNotes.Length;
                double bFreq = NoteToFreq(bassNotes[bIdx]);
                double bass = Math.Sin(2 * Math.PI * bFreq * t) * 0.45;

                // Hi-Hat rộn ràng
                double noise = ((i % 17) / 17.0 - 0.5) * (t % 0.125 < 0.03 ? 0.2 : 0);

                short sample = (short)((lead + bass + noise) * 11000);
                pcm[i * 2] = (byte)(sample & 0xFF);
                pcm[i * 2 + 1] = (byte)((sample >> 8) & 0xFF);
            }
        }

        private static void SynthesizeWinFanfare(byte[] pcm, int numSamples, int sampleRate)
        {
            // Khúc nhạc chiến thắng rực rỡ C -> E -> G -> C5 -> E5 -> G5
            int[] notes = { 60, 64, 67, 72, 76, 79, 84 };
            double noteTime = 0.25;

            for (int i = 0; i < numSamples; i++)
            {
                double t = (double)i / sampleRate;
                int idx = Math.Min(notes.Length - 1, (int)(t / noteTime));
                double freq = NoteToFreq(notes[idx]);
                double env = Math.Exp(-2.0 * (t % noteTime));

                double wave = (Math.Sin(2 * Math.PI * freq * t) + 0.5 * Math.Sin(4 * Math.PI * freq * t)) * env;
                short sample = (short)(wave * 12000);
                pcm[i * 2] = (byte)(sample & 0xFF);
                pcm[i * 2 + 1] = (byte)((sample >> 8) & 0xFF);
            }
        }

        private static void SynthesizeLoseJingle(byte[] pcm, int numSamples, int sampleRate)
        {
            // Điệu nhạc thua cuộc u buồn (A4 -> F4 -> E4 -> Dm)
            int[] notes = { 69, 65, 64, 62 };
            double noteTime = 0.6;

            for (int i = 0; i < numSamples; i++)
            {
                double t = (double)i / sampleRate;
                int idx = Math.Min(notes.Length - 1, (int)(t / noteTime));
                double freq = NoteToFreq(notes[idx]);
                double env = Math.Exp(-1.5 * (t % noteTime));

                double wave = Math.Sin(2 * Math.PI * freq * t) * env;
                short sample = (short)(wave * 12000);
                pcm[i * 2] = (byte)(sample & 0xFF);
                pcm[i * 2 + 1] = (byte)((sample >> 8) & 0xFF);
            }
        }

        private static void SynthesizeHitSfx(byte[] pcm, int numSamples, int sampleRate)
        {
            // Âm va chạm trúng zombie (bịch! punch impact sound)
            Random rnd = new Random();
            for (int i = 0; i < numSamples; i++)
            {
                double t = (double)i / sampleRate;
                double freq = 350.0 - 250.0 * (t / 0.25); // Sweep 350 -> 100 Hz
                double env = Math.Exp(-15.0 * t);
                double thud = Math.Sin(2 * Math.PI * freq * t);
                double noise = (rnd.NextDouble() - 0.5) * 0.5;

                short sample = (short)((thud * 0.7 + noise * 0.3) * env * 14000);
                pcm[i * 2] = (byte)(sample & 0xFF);
                pcm[i * 2 + 1] = (byte)((sample >> 8) & 0xFF);
            }
        }

        private static void SynthesizeShootSfx(byte[] pcm, int numSamples, int sampleRate)
        {
            // Âm bắn hạt giòn giã (Pop / Peashooter sound)
            for (int i = 0; i < numSamples; i++)
            {
                double t = (double)i / sampleRate;
                double freq = 600.0 - 400.0 * (t / 0.25);
                double env = Math.Exp(-20.0 * t);
                double pop = Math.Sin(2 * Math.PI * freq * t);

                short sample = (short)(pop * env * 14000);
                pcm[i * 2] = (byte)(sample & 0xFF);
                pcm[i * 2 + 1] = (byte)((sample >> 8) & 0xFF);
            }
        }

        private static void SynthesizeSunSfx(byte[] pcm, int numSamples, int sampleRate)
        {
            // Âm thu Nắng lấp lánh (E5 -> G5 -> C6 high chime)
            int[] notes = { 76, 79, 84 };
            double noteTime = 0.08;

            for (int i = 0; i < numSamples; i++)
            {
                double t = (double)i / sampleRate;
                int idx = Math.Min(notes.Length - 1, (int)(t / noteTime));
                double freq = NoteToFreq(notes[idx]);
                double env = Math.Exp(-10.0 * (t % noteTime));

                double chime = Math.Sin(2 * Math.PI * freq * t) * env;
                short sample = (short)(chime * 13000);
                pcm[i * 2] = (byte)(sample & 0xFF);
                pcm[i * 2 + 1] = (byte)((sample >> 8) & 0xFF);
            }
        }

        private static void SynthesizeExplodeSfx(byte[] pcm, int numSamples, int sampleRate)
        {
            // Âm nổ cực mạnh (BOOM!)
            Random rnd = new Random();
            for (int i = 0; i < numSamples; i++)
            {
                double t = (double)i / sampleRate;
                double subFreq = 90.0 - 70.0 * (t / 0.8);
                double env = Math.Exp(-4.0 * t);

                double boom = Math.Sin(2 * Math.PI * subFreq * t);
                double noise = (rnd.NextDouble() - 0.5) * 1.2;

                short sample = (short)((boom * 0.6 + noise * 0.4) * env * 15000);
                pcm[i * 2] = (byte)(sample & 0xFF);
                pcm[i * 2 + 1] = (byte)((sample >> 8) & 0xFF);
            }
        }

        private static void SynthesizeGenericSfx(string name, byte[] pcm, int numSamples, int sampleRate)
        {
            for (int i = 0; i < numSamples; i++)
            {
                double t = (double)i / sampleRate;
                double freq = name.Contains("plant") ? 350 + 200 * t : 440;
                double env = Math.Exp(-5.0 * t);

                short sample = (short)(Math.Sin(2 * Math.PI * freq * t) * env * 10000);
                pcm[i * 2] = (byte)(sample & 0xFF);
                pcm[i * 2 + 1] = (byte)((sample >> 8) & 0xFF);
            }
        }

        private static byte[] WrapPcmInWavHeader(byte[] pcmData, int sampleRate)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(new char[] { 'R', 'I', 'F', 'F' });
                bw.Write(36 + pcmData.Length);
                bw.Write(new char[] { 'W', 'A', 'V', 'E' });
                bw.Write(new char[] { 'f', 'm', 't', ' ' });
                bw.Write(16); // Subchunk1Size
                bw.Write((short)1); // AudioFormat PCM
                bw.Write((short)1); // NumChannels
                bw.Write(sampleRate);
                bw.Write(sampleRate * 2); // ByteRate
                bw.Write((short)2); // BlockAlign
                bw.Write((short)16); // BitsPerSample
                bw.Write(new char[] { 'd', 'a', 't', 'a' });
                bw.Write(pcmData.Length);
                bw.Write(pcmData);

                return ms.ToArray();
            }
        }

        #endregion
    }
}
