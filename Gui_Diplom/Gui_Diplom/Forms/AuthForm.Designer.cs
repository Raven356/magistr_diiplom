namespace Gui_Diplom.Forms
{
    partial class AuthForm
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
            googleAuthButton = new Button();
            telegramAuthButton = new Button();
            authorizeButton = new Button();
            registerButton = new Button();
            userNameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            confirmPasswordLabel = new Label();
            confirmPasswordTextBox = new TextBox();
            confirmRegistrationButton = new Button();
            cancelRegistrationButton = new Button();
            SuspendLayout();
            // 
            // googleAuthButton
            // 
            googleAuthButton.Location = new Point(151, 210);
            googleAuthButton.Name = "googleAuthButton";
            googleAuthButton.Size = new Size(196, 47);
            googleAuthButton.TabIndex = 0;
            googleAuthButton.Text = "Google auth";
            googleAuthButton.UseVisualStyleBackColor = true;
            googleAuthButton.Click += googleAuthButton_Click;
            // 
            // telegramAuthButton
            // 
            telegramAuthButton.Location = new Point(151, 274);
            telegramAuthButton.Name = "telegramAuthButton";
            telegramAuthButton.Size = new Size(196, 47);
            telegramAuthButton.TabIndex = 1;
            telegramAuthButton.Text = "Telegram auth";
            telegramAuthButton.UseVisualStyleBackColor = true;
            telegramAuthButton.Click += telegramAuthButton_Click;
            // 
            // authorizeButton
            // 
            authorizeButton.Location = new Point(86, 140);
            authorizeButton.Name = "authorizeButton";
            authorizeButton.Size = new Size(113, 47);
            authorizeButton.TabIndex = 2;
            authorizeButton.Text = "Authorize";
            authorizeButton.UseVisualStyleBackColor = true;
            authorizeButton.Click += authorizeButton_Click;
            // 
            // registerButton
            // 
            registerButton.Location = new Point(293, 140);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(113, 47);
            registerButton.TabIndex = 3;
            registerButton.Text = "Register";
            registerButton.UseVisualStyleBackColor = true;
            registerButton.Click += registerButton_Click;
            // 
            // userNameTextBox
            // 
            userNameTextBox.Location = new Point(205, 26);
            userNameTextBox.Name = "userNameTextBox";
            userNameTextBox.Size = new Size(249, 27);
            userNameTextBox.TabIndex = 4;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(205, 76);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(249, 27);
            passwordTextBox.TabIndex = 5;
            passwordTextBox.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 33);
            label1.Name = "label1";
            label1.Size = new Size(79, 20);
            label1.TabIndex = 6;
            label1.Text = "User name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 83);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 7;
            label2.Text = "Password";
            // 
            // confirmPasswordLabel
            // 
            confirmPasswordLabel.AutoSize = true;
            confirmPasswordLabel.Location = new Point(45, 127);
            confirmPasswordLabel.Name = "confirmPasswordLabel";
            confirmPasswordLabel.Size = new Size(129, 20);
            confirmPasswordLabel.TabIndex = 8;
            confirmPasswordLabel.Text = "Confirm password";
            confirmPasswordLabel.Visible = false;
            // 
            // confirmPasswordTextBox
            // 
            confirmPasswordTextBox.Location = new Point(205, 124);
            confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            confirmPasswordTextBox.Size = new Size(249, 27);
            confirmPasswordTextBox.TabIndex = 9;
            confirmPasswordTextBox.UseSystemPasswordChar = true;
            confirmPasswordTextBox.Visible = false;
            // 
            // confirmRegistrationButton
            // 
            confirmRegistrationButton.Location = new Point(163, 166);
            confirmRegistrationButton.Name = "confirmRegistrationButton";
            confirmRegistrationButton.Size = new Size(164, 38);
            confirmRegistrationButton.TabIndex = 10;
            confirmRegistrationButton.Text = "Confirm registration";
            confirmRegistrationButton.UseVisualStyleBackColor = true;
            confirmRegistrationButton.Visible = false;
            confirmRegistrationButton.Click += confirmRegistrationButton_Click;
            // 
            // cancelRegistrationButton
            // 
            cancelRegistrationButton.Location = new Point(163, 219);
            cancelRegistrationButton.Name = "cancelRegistrationButton";
            cancelRegistrationButton.Size = new Size(164, 38);
            cancelRegistrationButton.TabIndex = 11;
            cancelRegistrationButton.Text = "Cancel registration";
            cancelRegistrationButton.UseVisualStyleBackColor = true;
            cancelRegistrationButton.Visible = false;
            cancelRegistrationButton.Click += cancelRegistrationButton_Click;
            // 
            // AuthForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(523, 361);
            Controls.Add(cancelRegistrationButton);
            Controls.Add(confirmRegistrationButton);
            Controls.Add(confirmPasswordTextBox);
            Controls.Add(confirmPasswordLabel);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(passwordTextBox);
            Controls.Add(userNameTextBox);
            Controls.Add(registerButton);
            Controls.Add(authorizeButton);
            Controls.Add(telegramAuthButton);
            Controls.Add(googleAuthButton);
            Name = "AuthForm";
            Text = "AuthForm";
            Load += AuthForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button googleAuthButton;
        private Button telegramAuthButton;
        private Button authorizeButton;
        private Button registerButton;
        private TextBox userNameTextBox;
        private TextBox passwordTextBox;
        private Label label1;
        private Label label2;
        private Label confirmPasswordLabel;
        private TextBox confirmPasswordTextBox;
        private Button confirmRegistrationButton;
        private Button cancelRegistrationButton;
    }
}