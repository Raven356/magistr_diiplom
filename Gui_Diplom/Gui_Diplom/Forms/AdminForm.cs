using Gui_Diplom.Helpers;
using Gui_Diplom.Models.ApiResponses;
using System.ComponentModel;

namespace Gui_Diplom.Forms
{
    public partial class AdminForm : Form
    {
        private bool isLoggingOut = false;

        public AdminForm()
        {
            InitializeComponent();

            var userResponse = Task.Run(async () =>
            {
                return await HttpHelper<List<UserResponse>>
                    .GetAsync("https://localhost:7245/api/user/all");
            }).GetAwaiter().GetResult();

            usersGridView.DataSource = new BindingList<UserResponse>(userResponse);

            DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn
            {
                HeaderText = "Actions",
                Text = "Statistics",
                Name = "getStatistics",
                UseColumnTextForButtonValue = true
            };

            usersGridView.Columns.Add(btn2);

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn
            {
                HeaderText = "Actions",
                Text = "Delete",
                Name = "deleteButton",
                UseColumnTextForButtonValue = true
            };

            usersGridView.Columns.Add(btn);

            usersGridView.ReadOnly = true;
            usersGridView.Columns["deleteButton"].ReadOnly = false;
            usersGridView.Columns["getStatistics"].ReadOnly = false;

            usersGridView.CellContentClick += dataGridView1_CellContentClick;
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Avoid header clicks
            if (e.RowIndex < 0)
                return;

            // Check if button column clicked
            if (usersGridView.Columns[e.ColumnIndex].Name == "deleteButton")
            {
                int userId = Convert.ToInt32(usersGridView.Rows[e.RowIndex].Cells["Id"].Value);

                // Ask for confirmation
                var confirm = MessageBox.Show(
                    $"Delete user with ID {userId}?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    bool success = await DeleteUserAsync(userId);

                    if (success)
                    {
                        usersGridView.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete user.");
                    }
                }
            }

            if (usersGridView.Columns[e.ColumnIndex].Name == "getStatistics")
            {
                int userId = Convert.ToInt32(usersGridView.Rows[e.RowIndex].Cells["Id"].Value);
                new StatisticsForm(false, userId).Show();
            }
        }

        private async Task<bool> DeleteUserAsync(int id)
        {
            await HttpHelper<dynamic>.DeleteWithoutResponseAsync($"https://localhost:7245/api/user/delete/{id}");

            return true;
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            new StatisticsForm(true).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isLoggingOut = true;
            Close();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            ThemeHelper.ApplyTheme(this, GlobalVariables.CurrentTheme);
        }
    }
}
