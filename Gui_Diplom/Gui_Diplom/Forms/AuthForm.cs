using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Gui_Diplom.Helpers;
using Gui_Diplom.Models;
using Gui_Diplom.Models.ApiRequests;
using Gui_Diplom.Models.ApiResponses;
using System.Diagnostics;

namespace Gui_Diplom.Forms
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private async void googleAuthButton_Click(object sender, EventArgs e)
        {
            string[] scopes = { "openid", "email", "profile" };
            string credPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                ".credentials/google-winforms");

            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "id",
                    ClientSecret = "secret"
                },
                scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true));

            if (credential.Token.IsStale)
            {
                await credential.RefreshTokenAsync(CancellationToken.None);
            }

            string idToken = credential.Token.IdToken;
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
            string googleUserId = payload.Subject;

            var oauthService = new Oauth2Service(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "WinForms App"
                });

            var userInfo = await oauthService.Userinfo.Get().ExecuteAsync();

            string gmail = userInfo.Email;
            int at = gmail.IndexOf('@');

            string username = at > 0
                ? gmail[..at]
                : gmail;

            var response = await HttpHelper<GetUserResponse>
                .GetAsync($"https://localhost:7245/api/user/getByGoogleUserId/{googleUserId}");

            int userId = response.UserId;

            if (!response.IsFound)
            {
                var createUserRequest = new CreateUserRequest
                {
                    UserName = username,
                    AuthType = (int)AuthType.Google,
                    GoogleUserId = googleUserId,
                    EmailAddress = gmail,
                    NotificatonType = (int)NotificationType.Mailgun
                };

                var createUserResponse = await CreateUserAsync(createUserRequest);

                if (!createUserResponse.Success)
                {
                    MessageBox.Show(createUserResponse.ErrorMessage);
                    return;
                }

                userId = createUserResponse.UserId;
            }

            GlobalVariables.TelegramId = response.TelegramId;

            await CreateNewUserSession(userId, Guid.NewGuid());

            LoginSuccessfullOperation(false);
        }

        private async void telegramAuthButton_Click(object sender, EventArgs e)
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
                    int currentUserId;
                    bool isAdmin;
                    var getUserByTelegramIdResponse = await HttpHelper<GetUserResponse>
                        .GetAsync($"https://localhost:7245/api/user/getByTelegramId/{data.telegramUserId.ToString()}");

                    if (!getUserByTelegramIdResponse.IsFound)
                    {
                        var createUserRequest = new CreateUserRequest
                        {
                            UserName = data.username.ToString(),
                            AuthType = (int)AuthType.Telegram,
                            TelegramId = data.telegramUserId.ToString(),
                            NotificatonType = (int)NotificationType.Telegram,
                        };

                        var createUserResponse = await CreateUserAsync(createUserRequest);

                        if (!createUserResponse.Success)
                        {
                            MessageBox.Show(createUserResponse.ErrorMessage);
                            return;
                        }

                        currentUserId = createUserResponse.UserId;
                    }
                    else
                    {
                        currentUserId = getUserByTelegramIdResponse.UserId;
                    }

                    await CreateNewUserSession(currentUserId, Guid.Parse(sessionId));
                    GlobalVariables.TelegramId = data.telegramUserId.ToString();

                    LoginSuccessfullOperation(false);
                    break;
                }

                await Task.Delay(1000); // wait 1 sec
            }
        }

        private async void authorizeButton_Click(object sender, EventArgs e)
        {
            var response = await HttpHelper<LoginByPasswordResponse>.PostAsync("https://localhost:7245/api/login/loginPassword",
                new LoginByPasswordRequest
                {
                    UserName = userNameTextBox.Text,
                    Password = passwordTextBox.Text,
                });

            if (!response.IsSuccess)
            {
                MessageBox.Show(response.Error);
                return;
            }

            await CreateNewUserSession(response.UserId, Guid.NewGuid());
            GlobalVariables.TelegramId = response.TelegramId;

            LoginSuccessfullOperation(response.IsAdmin);
        }

        private void LoginSuccessfullOperation(bool isAdmin)
        {
            passwordTextBox.Text = string.Empty;

            if (!isAdmin)
            {
                new MainForm().Show();
            }
            else
            {
                new AdminForm().Show();
            }
            Hide();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            SwitchRegistrationValues(true);
        }

        private async void confirmRegistrationButton_Click(object sender, EventArgs e)
        {
            if (passwordTextBox.Text.Length < 5)
            {
                MessageBox.Show("Password should be at least 5 characters long!");
                return;
            }

            if (!passwordTextBox.Text.Equals(confirmPasswordTextBox.Text))
            {
                MessageBox.Show("Fields password and confirm password should be equal!");
                return;
            }

            var createUserRequest = new CreateUserRequest
            {
                UserName = userNameTextBox.Text,
                AuthType = (int)AuthType.Password,
                Password = passwordTextBox.Text,
            };

            var createUserResponse = await CreateUserAsync(createUserRequest);

            if (!createUserResponse.Success)
            {
                MessageBox.Show(createUserResponse.ErrorMessage);
                return;
            }

            SwitchRegistrationValues(false);
        }

        private void cancelRegistrationButton_Click(object sender, EventArgs e)
        {
            SwitchRegistrationValues(false);
        }

        private void SwitchRegistrationValues(bool isStartingRegistration)
        {
            passwordTextBox.Text = string.Empty;
            userNameTextBox.Text = string.Empty;
            confirmPasswordTextBox.Text = string.Empty;

            confirmRegistrationButton.Visible = isStartingRegistration;
            confirmPasswordTextBox.Visible = isStartingRegistration;
            googleAuthButton.Visible = !isStartingRegistration;
            telegramAuthButton.Visible = !isStartingRegistration;
            authorizeButton.Visible = !isStartingRegistration;
            registerButton.Visible = !isStartingRegistration;
            cancelRegistrationButton.Visible = isStartingRegistration;
        }

        private async Task CreateNewUserSession(int userId, Guid sessionId)
        {
            var createSessionResponse = await HttpHelper<CreateSessionResponse>
                        .PostAsync($"https://localhost:7245/api/session/create", new CreateSessionRequest
                        {
                            SessionId = sessionId,
                            UserId = userId
                        });

            GlobalVariables.CurrentSession = createSessionResponse.AccessToken;
        }

        private async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            var createUserResponse = await HttpHelper<CreateUserResponse>
                .PostAsync($"https://localhost:7245/api/user/createUser", createUserRequest);

            return createUserResponse;
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {
            ThemeHelper.ApplyTheme(this, GlobalVariables.CurrentTheme);
        }
    }
}
