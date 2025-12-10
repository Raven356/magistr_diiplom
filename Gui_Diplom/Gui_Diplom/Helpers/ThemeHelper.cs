namespace Gui_Diplom.Helpers
{
    internal static class ThemeHelper
    {
        public static void ApplyTheme(Form form, Theme theme)
        {
            form.BackColor = theme.BackColor;
            form.ForeColor = theme.ForeColor;

            foreach (Control control in form.Controls)
            {
                ApplyThemeToControl(control, theme);
            }
        }

        private static void ApplyThemeToControl(Control ctrl, Theme theme)
        {
            switch (ctrl)
            {
                case Button btn:
                    btn.BackColor = theme.ButtonBackColor;
                    btn.ForeColor = theme.ButtonForeColor;
                    btn.FlatStyle = FlatStyle.Flat;
                    break;

                case DataGridView dgv:
                    dgv.BackgroundColor = theme.DataGridBackColor;
                    dgv.ForeColor = theme.DataGridForeColor;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = theme.ButtonBackColor;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = theme.ButtonForeColor;
                    break;

                case TextBox txt:
                    txt.BackColor = theme.TextBoxBackColor;
                    txt.ForeColor = theme.TextBoxForeColor;
                    break;

                case PictureBox pic:
                    pic.BackColor = theme.PictureBoxBackColor;
                    break;

                case Panel pnl:
                    pnl.BackColor = theme.BackColor;
                    break;

                case NumericUpDown numericUpDown:
                    numericUpDown.BackColor = theme.NumericUpDownBackColor;
                    numericUpDown.ForeColor = theme.NumericUpDownForeColor;
                    break;
                case ComboBox comboBox:
                    comboBox.ForeColor = theme.ComboBoxForeColor;
                    comboBox.BackColor = theme.ComboBoxBackColor;
                    break;

                default:
                    ctrl.BackColor = theme.BackColor;
                    ctrl.ForeColor = theme.ForeColor;
                    break;
            }

            // Apply recursively
            foreach (Control child in ctrl.Controls)
            {
                ApplyThemeToControl(child, theme);
            }
        }
    }
}
