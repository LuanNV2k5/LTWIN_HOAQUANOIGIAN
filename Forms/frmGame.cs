using System;
using System.Drawing;
using System.Windows.Forms;
using HoaQuaNoiGian.Engine;
using HoaQuaNoiGian.Managers;
using HoaQuaNoiGian.Models;

namespace HoaQuaNoiGian.Forms
{
    /// <summary>
    /// Form Gameplay chính của trò chơi (frmGame).
    /// Windows Forms Designer kết hợp GamePanel GDI+.
    /// </summary>
    public partial class frmGame : Form
    {
        public bool ReturnedToMainMenu { get; private set; } = false;
        private DateTime _lastFrameTime;
        private int _levelNum;

        public frmGame(int levelNum)
        {
            InitializeComponent();
            DoubleBuffered = true;
            try { Icon = ImageLoader.GetAppIcon(); } catch { }

            gamePanel.OnPauseRequested += ShowPauseDialog;
            gamePanel.OnGameOver += HandleGameOver;

            StartLevel(levelNum);
        }

        public void StartLevel(int levelNum)
        {
            _levelNum = levelNum;
            Text = $"Hoa Quả Nổi Giận - Màn {_levelNum}";

            gameTimer.Stop();
            gamePanel.InitializeEngine(_levelNum);
            _lastFrameTime = DateTime.Now;
            gameTimer.Start();
            gamePanel.Invalidate();
        }

        private void gameTimer_Tick(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            float dt = (float)(now - _lastFrameTime).TotalSeconds;
            _lastFrameTime = now;

            if (dt > 0.05f) dt = 0.05f;

            if (gamePanel.Engine != null && gamePanel.Engine.State == GameState.Playing)
            {
                gamePanel.Engine.Update(dt);
                gamePanel.Invalidate();
            }
        }

        private void frmGame_KeyDown(object? sender, KeyEventArgs e)
        {
            if (gamePanel.Engine == null) return;

            // P hoặc Esc: Tạm dừng / Tiếp tục
            if (e.KeyCode == Keys.P || e.KeyCode == Keys.Escape)
            {
                ShowPauseDialog();
            }
            // M: Bật/tắt âm thanh nhanh
            else if (e.KeyCode == Keys.M)
            {
                SoundManager.ToggleSound();
                SoundManager.ToggleMusic();
            }
            // Phím 1-8: Chọn nhanh thẻ hoa quả
            else if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D8)
            {
                int index = e.KeyCode - Keys.D1;
                var unlocked = gamePanel.Engine.CurrentLevel.UnlockedFruits;
                if (index < unlocked.Count)
                {
                    FruitType ft = unlocked[index];
                    if (gamePanel.Engine.SunEnergy >= GameEngine.GetPlantCost(ft) && gamePanel.Engine.CardCooldowns[ft] <= 0)
                    {
                        gamePanel.Engine.SelectedCard = ft;
                        gamePanel.Engine.IsShovelSelected = false;
                        SoundManager.PlaySound("sfx_click");
                    }
                }
            }
        }

        private void ShowPauseDialog()
        {
            if (gamePanel.Engine == null) return;
            gamePanel.Engine.PauseGame();
            gameTimer.Stop();

            using (var pauseForm = new frmPause())
            {
                var res = pauseForm.ShowDialog(this);
                if (res == DialogResult.Retry)
                {
                    RestartGame();
                }
                else if (res == DialogResult.Abort)
                {
                    ReturnToMenu();
                }
                else
                {
                    gamePanel.Engine.ResumeGame();
                    _lastFrameTime = DateTime.Now;
                    gameTimer.Start();
                }
            }
        }

        private void HandleGameOver()
        {
            if (gamePanel.Engine == null) return;

            gameTimer.Stop();
            bool isWin = gamePanel.Engine.State == GameState.Won;

            // 1. Tự động lưu tiến trình người chơi vào file profiles.json
            ProfileManager.Instance.SaveCurrentProgress(
                _levelNum,
                isWin ? gamePanel.Engine.Score : 0,
                isWin ? gamePanel.Engine.StarsCount : 0,
                isWin
            );

            // 2. Tự động ghi nhận thành tích vào ScoreManager
            string playerName = ProfileManager.Instance.CurrentPlayer?.Name ?? "Người chơi";
            ScoreManager.Instance.AddScore(new ScoreRecord
            {
                PlayerName = playerName,
                LevelIndex = _levelNum,
                Score = gamePanel.Engine.Score,
                Stars = gamePanel.Engine.StarsCount,
                TimeSeconds = (int)gamePanel.Engine.GameTime
            });

            // 3. Hiện Menu nhỏ gồm: CHƠI TIẾP, CHƠI LẠI, TRỞ VỀ MENU
            using (var resultForm = new frmResult(_levelNum, isWin, gamePanel.Engine.Score, gamePanel.Engine.StarsCount, gamePanel.Engine.ZombiesKilled, (int)gamePanel.Engine.GameTime))
            {
                var res = resultForm.ShowDialog(this);
                if (res == DialogResult.OK && _levelNum < 5)
                {
                    // Bấm chữ chơi tiếp thì màn tiếp theo mới hiện lên (ngay trên cửa sổ hiện tại, không đóng mở form)
                    StartLevel(_levelNum + 1);
                }
                else if (res == DialogResult.Retry)
                {
                    // Chơi lại màn hiện tại
                    StartLevel(_levelNum);
                }
                else
                {
                    // Trở về Menu chính
                    ReturnToMenu();
                }
            }
        }

        private void RestartGame()
        {
            StartLevel(_levelNum);
        }

        private void ReturnToMenu()
        {
            ReturnedToMainMenu = true;
            gameTimer.Stop();

            // Lưu lại tiến trình màn đang chơi dở khi người chơi thoát ra Menu
            if (gamePanel.Engine != null)
            {
                ProfileManager.Instance.SaveCurrentProgress(_levelNum, 0, 0, false);
            }

            Close();
        }

        private void frmGame_FormClosing(object? sender, FormClosingEventArgs e)
        {
            gameTimer.Stop();

            // Lưu tiến trình nếu người chơi bấm nút X đóng cửa sổ
            if (gamePanel.Engine != null && !ReturnedToMainMenu)
            {
                ProfileManager.Instance.SaveCurrentProgress(_levelNum, 0, 0, false);
            }
        }
    }
}
