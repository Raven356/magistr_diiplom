namespace Gui_Diplom
{
    public static class GlobalVariables
    {
        public static Guid CurrentSession { get; set; }

        public static string TelegramId { get; set; }

        public static Theme CurrentTheme { get; set; } = AppThemes.Light;
    }
}
