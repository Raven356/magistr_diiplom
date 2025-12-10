namespace Gui_Diplom
{
    public class AppThemes
    {
        public static readonly Theme Light = new Theme
        {
            BackColor = Color.FromArgb(225, 240, 255),       
            ForeColor = Color.FromArgb(20, 20, 20),          
            ButtonBackColor = Color.White, 
            ButtonForeColor = Color.Black,
            DataGridBackColor = Color.FromArgb(240, 250, 255), 
            DataGridForeColor = Color.Black,
            TextBoxBackColor = Color.White,                  
            TextBoxForeColor = Color.Black,
            PictureBoxBackColor = Color.White,
            NumericUpDownForeColor = Color.Black,
            NumericUpDownBackColor = Color.White,
            ComboBoxBackColor = Color.White,
            ComboBoxForeColor = Color.Black,
        };

        public static readonly Theme Dark = new Theme
        {
            BackColor = Color.FromArgb(10, 25, 50),
            ForeColor = Color.White,

            ButtonBackColor = Color.FromArgb(200, 200, 200),
            ButtonForeColor = Color.Black,

            DataGridBackColor = Color.FromArgb(200, 200, 200),
            DataGridForeColor = Color.Black,

            TextBoxBackColor = Color.FromArgb(200, 200, 200),
            TextBoxForeColor = Color.Black,

            PictureBoxBackColor = Color.FromArgb(200, 200, 200),

            NumericUpDownBackColor = Color.FromArgb(200, 200, 200),
            NumericUpDownForeColor = Color.Black,

            ComboBoxBackColor = Color.FromArgb(200, 200, 200),
            ComboBoxForeColor = Color.Black,
        };
    }
}
