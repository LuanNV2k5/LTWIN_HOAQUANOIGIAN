using System;
using System.Drawing;
using System.Windows.Forms;
using HoaQuaNoiGian.Managers;
using HoaQuaNoiGian.Models;

namespace HoaQuaNoiGian.Forms
{
    public partial class frmProgress : Form
    {
        public bool LaunchGameDirectly { get; private set; } = false;
        public int LevelToLaunch { get; private set; } = 1;

        public frmProgress()
        {
            InitializeComponent();
            try { Icon = ImageLoader.GetAppIcon(); } catch { }
            LoadProfilesList();
        }

        private void LoadProfilesList()
        {
            dgvProfiles.Rows.Clear();
            var profiles = ProfileManager.Instance.Profiles;

            if (profiles.Count == 0)
            {
                // Nếu chưa có ai, tự động tạo 1 profile mặc định
                ProfileManager.Instance.CreateNewProfile("Người chơi 1");
                profiles = ProfileManager.Instance.Profiles;
            }

            for (int i = 0; i < profiles.Count; i++)
            {
                var p = profiles[i];
                string starsText = p.TotalStars > 0 ? $"{p.TotalStars} ⭐" : "0 ⭐";
                string timeText = p.LastPlayed.ToString("HH:mm dd/MM/yyyy");
                string currentLevelDesc = p.GetLevelTitle();
                string unlockedDesc = $"Màn {p.UnlockedLevel}/5";

                int rowIndex = dgvProfiles.Rows.Add(
                    i + 1,
                    p.Name,
                    currentLevelDesc,
                    unlockedDesc,
                    $"{p.TotalScore:N0} đ",
                    starsText,
                    timeText
                );

                dgvProfiles.Rows[rowIndex].Tag = p;

                if (ProfileManager.Instance.CurrentPlayer?.Id == p.Id)
                {
                    dgvProfiles.Rows[rowIndex].Selected = true;
                }
            }

            UpdateButtonStates();
        }

        private PlayerProfile? GetSelectedProfile()
        {
            if (dgvProfiles.SelectedRows.Count > 0)
            {
                return dgvProfiles.SelectedRows[0].Tag as PlayerProfile;
            }
            return null;
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = GetSelectedProfile() != null;
            btnPlayContinue.Enabled = hasSelection;
            btnSelectLevel.Enabled = hasSelection;
            btnDeleteProfile.Enabled = hasSelection && ProfileManager.Instance.Profiles.Count > 1;
        }

        private void btnPlayContinue_Click(object sender, EventArgs e)
        {
            var p = GetSelectedProfile();
            if (p == null) return;

            SoundManager.PlaySound("sfx_click");
            ProfileManager.Instance.SelectProfile(p.Id);

            LaunchGameDirectly = true;
            LevelToLaunch = p.CurrentLevel;

            var gameForm = new frmGame(p.CurrentLevel);
            gameForm.FormClosed += (s, ev) =>
            {
                if (gameForm.ReturnedToMainMenu)
                {
                    Close(); // Quay thẳng ra menu đầu game
                }
                else
                {
                    ProfileManager.Instance.LoadProfiles();
                    LoadProfilesList();
                    Show();
                }
            };
            gameForm.Show();
            Hide();
        }

        private void dgvProfiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnPlayContinue_Click(sender, EventArgs.Empty);
            }
        }

        private void btnSelectLevel_Click(object sender, EventArgs e)
        {
            var p = GetSelectedProfile();
            if (p == null) return;

            SoundManager.PlaySound("sfx_click");
            ProfileManager.Instance.SelectProfile(p.Id);

            var levelForm = new frmLevelSelect();
            levelForm.FormClosed += (s, ev) =>
            {
                if (levelForm.ReturnedToMainMenu)
                {
                    Close(); // Quay thẳng ra menu đầu game
                }
                else
                {
                    ProfileManager.Instance.LoadProfiles();
                    LoadProfilesList();
                    Show();
                }
            };
            levelForm.Show();
            Hide();
        }

        private void btnNewProfile_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            using (var newPlayerForm = new frmNewPlayer())
            {
                if (newPlayerForm.ShowDialog(this) == DialogResult.OK)
                {
                    var newProfile = ProfileManager.Instance.CreateNewProfile(newPlayerForm.PlayerName);
                    LoadProfilesList();

                    var res = MessageBox.Show(
                        $"Đã tạo hồ sơ cho '{newProfile.Name}'!\nBạn có muốn bắt đầu chơi Màn 1 ngay bây giờ không?",
                        "Bắt đầu chơi",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (res == DialogResult.Yes)
                    {
                        var gameForm = new frmGame(1);
                        gameForm.FormClosed += (s, ev) =>
                        {
                            if (gameForm.ReturnedToMainMenu)
                            {
                                Close(); // Quay thẳng ra menu đầu game
                            }
                            else
                            {
                                ProfileManager.Instance.LoadProfiles();
                                LoadProfilesList();
                                Show();
                            }
                        };
                        gameForm.Show();
                        Hide();
                    }
                }
            }
        }

        private void btnDeleteProfile_Click(object sender, EventArgs e)
        {
            var p = GetSelectedProfile();
            if (p == null) return;

            SoundManager.PlaySound("sfx_click");
            var res = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa tiến trình của '{p.Name}' không?\n(Dữ liệu tiến trình này sẽ bị mất hoàn toàn)",
                "Xác nhận xóa tiến trình",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (res == DialogResult.Yes)
            {
                ProfileManager.Instance.DeleteProfile(p.Id);
                LoadProfilesList();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            Close();
        }

        private void dgvProfiles_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvProfiles_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
