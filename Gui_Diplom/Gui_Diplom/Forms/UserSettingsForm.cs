using Gui_Diplom.Helpers;
using Gui_Diplom.Models;
using Gui_Diplom.Models.ApiRequests;
using Gui_Diplom.Models.ApiResponses;
using System.Diagnostics;

namespace Gui_Diplom.Forms
{
    public partial class UserSettingsForm : Form
    {
        private int userId;

        public UserSettingsForm()
        {
            InitializeComponent();
            var userResponse = Task.Run(async () =>
            {
                return await HttpHelper<UserResponse>
                    .GetAsync($"https://localhost:7245/api/user/getBySessionGuid/{GlobalVariables.CurrentSession}");
            }).GetAwaiter().GetResult();

            userNameTextBox.Text = userResponse.UserName;
            userEmailTextBox.Text = userResponse.UserEmail;

            notificationDropDown.Items.AddRange([NotificationType.None, NotificationType.Mailgun, NotificationType.Telegram]);

            notificationDropDown.SelectedItem = Enum.Parse(typeof(NotificationType), userResponse.NotificationType.ToString());

            authenticatedToTelegramLabel.Text = userResponse.AuthenticatedToTelegram ? "Yes" : "No";
            authenticateToTelegramButton.Visible = !userResponse.AuthenticatedToTelegram;
            userId = userResponse.Id;
            themeComboBox.SelectedIndex = GlobalVariables.CurrentTheme == AppThemes.Dark ? 1 : 0;
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordTextBox.Text) && passwordTextBox.Text.Length < 5)
            {
                MessageBox.Show("Password should be greater than 5 characters");
                return;
            }

            await HttpHelper<dynamic>.PostWithoutResponseAsync("https://localhost:7245/api/user/update",
                new UpdateUserRequest
                {
                    Id = userId,
                    UserName = userNameTextBox.Text,
                    Password = passwordTextBox.Text,
                    EmailAddress = userEmailTextBox.Text,
                    NotificationType = (int)Enum.Parse(typeof(NotificationType), notificationDropDown.SelectedItem.ToString())
                });
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void authenticateToTelegramButton_Click(object sender, EventArgs e)
        {
            string botName = "fire_detect_diplom_mag_bot";

            dynamic data = await HttpHelper<dynamic>.PostAsync("https://localhost:7245/api/login/create");

            string sessionId = data.sessionId;

            string webFallback = $"https://t.me/{botName}?start={sessionId}";

            Process.Start(new ProcessStartInfo
            {
                FileName = webFallback,
                UseShellExecute = true
            });

            while (true)
            {
                data = await HttpHelper<dynamic>.GetAsync($"https://localhost:7245/api/login/{sessionId}");

                if (bool.Parse(data?.isCompleted.ToString()))
                {
                    var getUserByTelegramIdResponse = await HttpHelper<GetUserResponse>
                        .GetAsync($"https://localhost:7245/api/user/getByTelegramId/{data.telegramUserId.ToString()}");

                    if (getUserByTelegramIdResponse.IsFound)
                    {
                        MessageBox.Show("User with this id is already authenticated!");
                        return;
                    }

                    await HttpHelper<dynamic>.PostWithoutResponseAsync("https://localhost:7245/api/user/update/telegram",
                        new UpdateUserTelegramRequest
                        {
                            UserId = userId,
                            TelegramId = data.telegramUserId.ToString()
                        });

                    GlobalVariables.TelegramId = data.telegramUserId.ToString();
                    authenticateToTelegramButton.Visible = false;
                    authenticatedToTelegramLabel.Text = "Yes";
                    break;
                }

                await Task.Delay(1000); // wait 1 sec
            }
        }

        private void themeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Theme selectedTheme = themeComboBox.SelectedIndex == 0 ? AppThemes.Light : AppThemes.Dark;

            // Save user preference in DB or config
            GlobalVariables.CurrentTheme = selectedTheme;

            // Apply to all open forms
            foreach (Form openForm in Application.OpenForms)
            {
                ThemeHelper.ApplyTheme(openForm, selectedTheme);
            }
        }

        private void UserSettingsForm_Load(object sender, EventArgs e)
        {
            ThemeHelper.ApplyTheme(this, GlobalVariables.CurrentTheme);
        }
    }
}
