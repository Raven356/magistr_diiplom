namespace Gui_Diplom.Forms
{
    partial class UserSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            authenticatedToTelegramLabel = new Label();
            authenticateToTelegramButton = new Button();
            userNameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            notificationDropDown = new ComboBox();
            saveButton = new Button();
            cancelButton = new Button();
            label5 = new Label();
            userEmailTextBox = new TextBox();
            themeComboBox = new ComboBox();
            label6 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 52);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 0;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 135);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 1;
            label2.Text = "Password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 183);
            label3.Name = "label3";
            label3.Size = new Size(121, 20);
            label3.TabIndex = 2;
            label3.Text = "Notification type";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 228);
            label4.Name = "label4";
            label4.Size = new Size(196, 20);
            label4.TabIndex = 3;
            label4.Text = "Is authenticated to telegram";
            // 
            // authenticatedToTelegramLabel
            // 
            authenticatedToTelegramLabel.AutoSize = true;
            authenticatedToTelegramLabel.Location = new Point(268, 228);
            authenticatedToTelegramLabel.Name = "authenticatedToTelegramLabel";
            authenticatedToTelegramLabel.Size = new Size(29, 20);
            authenticatedToTelegramLabel.TabIndex = 4;
            authenticatedToTelegramLabel.Text = "No";
            // 
            // authenticateToTelegramButton
            // 
            authenticateToTelegramButton.Location = new Point(340, 224);
            authenticateToTelegramButton.Name = "authenticateToTelegramButton";
            authenticateToTelegramButton.Size = new Size(107, 29);
            authenticateToTelegramButton.TabIndex = 5;
            authenticateToTelegramButton.Text = "Authenticate";
            authenticateToTelegramButton.UseVisualStyleBackColor = true;
            authenticateToTelegramButton.Visible = false;
            authenticateToTelegramButton.Click += authenticateToTelegramButton_Click;
            // 
            // userNameTextBox
            // 
            userNameTextBox.Location = new Point(268, 52);
            userNameTextBox.Name = "userNameTextBox";
            userNameTextBox.Size = new Size(231, 27);
            userNameTextBox.TabIndex = 6;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(268, 132);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(231, 27);
            passwordTextBox.TabIndex = 7;
            passwordTextBox.UseSystemPasswordChar = true;
            // 
            // notificationDropDown
            // 
            notificationDropDown.FormattingEnabled = true;
            notificationDropDown.Location = new Point(268, 180);
            notificationDropDown.Name = "notificationDropDown";
            notificationDropDown.Size = new Size(231, 28);
            notificationDropDown.TabIndex = 8;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(337, 311);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(162, 57);
            saveButton.TabIndex = 9;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(30, 311);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(162, 57);
            cancelButton.TabIndex = 10;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(30, 93);
            label5.Name = "label5";
            label5.Size = new Size(46, 20);
            label5.TabIndex = 11;
            label5.Text = "Email";
            // 
            // userEmailTextBox
            // 
            userEmailTextBox.Location = new Point(268, 90);
            userEmailTextBox.Name = "userEmailTextBox";
            userEmailTextBox.Size = new Size(231, 27);
            userEmailTextBox.TabIndex = 12;
            // 
            // themeComboBox
            // 
            themeComboBox.FormattingEnabled = true;
            themeComboBox.Items.AddRange(new object[] { "Light", "Dark" });
            themeComboBox.Location = new Point(268, 268);
            themeComboBox.Name = "themeComboBox";
            themeComboBox.Size = new Size(151, 28);
            themeComboBox.TabIndex = 13;
            themeComboBox.SelectedIndexChanged += themeComboBox_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(30, 268);
            label6.Name = "label6";
            label6.Size = new Size(54, 20);
            label6.TabIndex = 14;
            label6.Text = "Theme";
            // 
            // UserSettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(532, 388);
            Controls.Add(label6);
            Controls.Add(themeComboBox);
            Controls.Add(userEmailTextBox);
            Controls.Add(label5);
            Controls.Add(cancelButton);
            Controls.Add(saveButton);
            Controls.Add(notificationDropDown);
            Controls.Add(passwordTextBox);
            Controls.Add(userNameTextBox);
            Controls.Add(authenticateToTelegramButton);
            Controls.Add(authenticatedToTelegramLabel);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "UserSettingsForm";
            Text = "UserSettingsForm";
            Load += UserSettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label authenticatedToTelegramLabel;
        private Button authenticateToTelegramButton;
        private TextBox userNameTextBox;
        private TextBox passwordTextBox;
        private ComboBox notificationDropDown;
        private Button saveButton;
        private Button cancelButton;
        private Label label5;
        private TextBox userEmailTextBox;
        private ComboBox themeComboBox;
        private Label label6;
    }
}