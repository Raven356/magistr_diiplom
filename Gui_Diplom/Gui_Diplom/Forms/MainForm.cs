using Gui_Diplom.Forms;
using Gui_Diplom.Helpers;
using Gui_Diplom.Models;
using Gui_Diplom.Models.ApiRequests;
using Gui_Diplom.Models.ApiResponses;
using Gui_Diplom.Responses;
using Newtonsoft.Json;
using OpenCvSharp;
using Python.Runtime;

namespace Gui_Diplom
{
    public partial class MainForm : Form
    {
        private bool isLoggingOut = false;

        private Task _detectionTask;

        private string _selectedFile;

        private CancellationTokenSource _cts;

        private DateTime _lastSendMessageDate;

        private int _secondsSendPause = 60;

        private volatile bool _isPaused = true;

        private bool isPlaying = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void RunDetectionRealTime(string videoPath)
        {
            _cts?.Cancel();

            if (_detectionTask != null)
            {
                await _detectionTask;
            }
            _cts = new CancellationTokenSource();

            isPlaying = true;

            _detectionTask = Task.Run(async () =>
            {
                using var cap = videoPath == "webcam"
                    ? new VideoCapture(0)
                    : new VideoCapture(videoPath);
                Mat frame = new Mat();

                while (cap.Read(frame))
                {
                    try
                    {
                        _cts.Token.ThrowIfCancellationRequested();
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }

                    while (_isPaused)
                    {
                        Thread.Sleep(100);
                        Application.DoEvents();

                        try
                        {
                            _cts.Token.ThrowIfCancellationRequested();
                        }
                        catch (OperationCanceledException)
                        {
                            return;
                        }
                    }

                    bool isFireDetected = false;
                    byte[] annotatedBytes;

                    using (Py.GIL())
                    {
                        dynamic np = Py.Import("numpy"); // import NumPy

                        // Convert OpenCvSharp Mat to NumPy array
                        byte[] data = frame.ImEncode(".bmp"); // optional, or use Raw data
                        int h = frame.Height;
                        int w = frame.Width;

                        // Create NumPy array on Python side
                        dynamic py_frame = np.array(data); // byte array to numpy

                        // Call Python annotation
                        dynamic jsonStr = PythonVenvHelper.Detector.process_frame_np(py_frame, float.Parse(confidenceUpDown.Value.ToString()));

                        var detectionResponse = JsonConvert.DeserializeObject<DetectionResponse>((string)jsonStr);

                        isFireDetected = detectionResponse.Detected;

                        // Convert back to Bitmap
                        annotatedBytes = Convert.FromBase64String(detectionResponse.Image);
                        using var ms = new MemoryStream(annotatedBytes);

                        try
                        {
                            this.Invoke(new Action(() =>
                            {
                                pictureBox1.Image?.Dispose();
                                pictureBox1.Image = new Bitmap(ms);
                            }));
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }

                    if (isFireDetected && (DateTime.UtcNow - _lastSendMessageDate).TotalSeconds > _secondsSendPause)
                    {
                        var detectionCreateResult = await HttpHelper<CreateDetectionResponse>.PostAsync("https://localhost:7245/api/detection/create",
                            new CreateDetectionRequest
                            {
                                DetectionDate = DateTime.UtcNow,
                                SessionGuid = GlobalVariables.CurrentSession,
                                VideoPath = selectedFileTextBox.Text
                            });

                        await Task.Run(async () => await HandleNotification(detectionCreateResult.DetectionId, annotatedBytes));
                    }

                    Application.DoEvents();
                    Thread.Sleep(30);
                }
                isPlaying = false;
            }, _cts.Token);
        }

        private async Task HandleNotification(int detectionId, byte[] annotatedBytes)
        {
            var userResponse = await HttpHelper<UserResponse>
                    .GetAsync($"https://localhost:7245/api/user/getBySessionGuid/{GlobalVariables.CurrentSession}");

            if ((NotificationType)userResponse.NotificationType == NotificationType.Mailgun && !string.IsNullOrEmpty(userResponse.UserEmail))
            {
                bool result = await MailgunHelper.TriggerMailgunNotification(annotatedBytes);
            }

            if ((NotificationType)userResponse.NotificationType == NotificationType.Telegram && !string.IsNullOrEmpty(GlobalVariables.TelegramId))
            {
                await HttpHelper<dynamic>.PostWithoutResponseAsync(
                        "https://localhost:7245/api/telegram/sendNotification",
                        new SendNotificationTelegram
                        {
                            PhotoBytes = annotatedBytes,
                            TelegramUserId = long.Parse(GlobalVariables.TelegramId)
                        });
            }

            await HttpHelper<dynamic>.PostWithoutResponseAsync(
                    "https://localhost:7245/api/notification/create",
                    new CreateNotificationRequest
                    {
                        DetectionId = detectionId,
                        NotificationDate = DateTime.UtcNow,
                        NotificationSource = userResponse.NotificationType
                    });

            _lastSendMessageDate = DateTime.UtcNow;
        }

        private async void startDetectionButton_Click(object sender, EventArgs e)
        {
            var result = await HttpHelper<CreateVideoResponse>.PostAsync("https://localhost:7245/api/video/createOrUpdateVideo",
                new CreateVideoRequest
                {
                    VideoPath = selectedFileTextBox.Text,
                    AccessTime = DateTime.UtcNow
                });

            _isPaused = !_isPaused;

            startDetectionButton.Text = _isPaused ? "Start detection" : "Pause detection";

            if (!result.Success)
            {
                MessageBox.Show(result.Error);
                return;
            }

            if (!_isPaused && !isPlaying)
            {
                RunDetectionRealTime(_selectedFile);
                _lastSendMessageDate = DateTime.MinValue;
            }
        }

        private async void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _cts?.Cancel();

            GlobalVariables.TelegramId = string.Empty;

            if (_detectionTask != null)
            {
                await _detectionTask;
            }

            PythonEngine.Shutdown();
            foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
            {
                if (isLoggingOut && form is AuthForm)
                {
                    form.Show();
                    continue;
                }
                form.Close();
            }
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a Video File";
            openFileDialog.Filter = "Video Files|*.mp4;*.avi;*.mov;*.mkv|All Files|*.*";
            openFileDialog.InitialDirectory = @"D:\python\fire_diplom\Version2(RCNN)\test_videos";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _selectedFile = openFileDialog.FileName;
                selectedFileTextBox.Text = _selectedFile;
                startDetectionButton.Enabled = true;
                isPlaying = false;
            }
        }

        private void userSettingsButton_Click(object sender, EventArgs e)
        {
            new UserSettingsForm().Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                selectedFileTextBox.Text = "webcam";
                selectFileButton.Enabled = false;
                startDetectionButton.Enabled = true;
                _selectedFile = selectedFileTextBox.Text;
                isPlaying = false;
            }
            else
            {
                selectedFileTextBox.Text = "";
                selectFileButton.Enabled = true;
                startDetectionButton.Enabled = false;
                isPlaying = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new StatisticsForm(false).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isLoggingOut = true;
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ThemeHelper.ApplyTheme(this, GlobalVariables.CurrentTheme);
        }
    }
}
