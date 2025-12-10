using Gui_Diplom.Helpers;
using Gui_Diplom.Models.ApiRequests;
using Gui_Diplom.Models.ApiResponses;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace Gui_Diplom.Forms
{
    public partial class StatisticsForm : Form
    {
        private readonly bool isAdmin;
        private readonly int? userId;
        private List<GetStatisticsResponse> currentStats;

        public StatisticsForm(bool isAdmin, int? userId = null)
        {
            InitializeComponent();
            this.isAdmin = isAdmin;
            this.userId = userId;
            SetupChart();
        }

        private void SetupChart()
        {
            chart1.Dock = DockStyle.Fill;

            var area = new ChartArea("Main");
            area.Position.Auto = false;
            area.Position = new ElementPosition(5, 5, 90, 90);
            area.InnerPlotPosition = new ElementPosition(10, 10, 80, 80);

            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            area.AxisX.LabelStyle.Angle = -45;
            area.AxisX.LabelStyle.Format = "MM/dd";

            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(area);

            var series = new Series("Detections")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.Date
            };
            series["PointWidth"] = "0.8";

            chart1.Series.Clear();
            chart1.Series.Add(series);
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            var request = new GetStatisticsByUserRequest
            {
                SessionId = GlobalVariables.CurrentSession,
                StartDate = startDateTimePicker.Value,
                EndDate = endDateTimePicker.Value,
                UserId = userId
            };

            var url = isAdmin ? "https://localhost:7245/api/statistics/getForAdmin" : "https://localhost:7245/api/statistics/getByUser";

            url = userId != null ? "https://localhost:7245/api/statistics/getByUserId" : url;

            var response = await HttpHelper<List<GetStatisticsResponse>>.PostAsync(url, request);

            var series = chart1.Series["Detections"];
            series.Points.Clear();

            if (response.Count == 0)
            {
                MessageBox.Show("No detections were made yet!");
                return;
            }

            exportDataButton.Visible = true;

            currentStats = response;

            foreach (var item in response)
            {
                series.Points.AddXY(item.DetectionDate, item.DetectionCount);
            }
        }

        private void exportDataButton_Click(object sender, EventArgs e)
        {
            ExportStatisticsToCsv(currentStats);
        }

        private void ExportStatisticsToCsv(List<GetStatisticsResponse> data)
        {
            if (data == null || data.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = "statistics.csv";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    var sb = new StringBuilder();

                    sb.AppendLine("DetectionDate,DetectionCount");

                    foreach (var item in data)
                    {
                        sb.AppendLine($"{item.DetectionDate:yyyy-MM-dd},{item.DetectionCount}");
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);

                    MessageBox.Show("Export completed successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting data: {ex.Message}", "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            ThemeHelper.ApplyTheme(this, GlobalVariables.CurrentTheme);
        }
    }
}
