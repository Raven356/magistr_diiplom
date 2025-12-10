using System.Threading.Tasks;

namespace Gui_Diplom
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            startDetectionButton = new Button();
            pictureBox1 = new PictureBox();
            selectFileButton = new Button();
            selectedFileTextBox = new TextBox();
            label1 = new Label();
            confidenceUpDown = new NumericUpDown();
            userSettingsButton = new Button();
            checkBox1 = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)confidenceUpDown).BeginInit();
            SuspendLayout();
            // 
            // startDetectionButton
            // 
            startDetectionButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            startDetectionButton.Enabled = false;
            startDetectionButton.Location = new Point(314, 295);
            startDetectionButton.Name = "startDetectionButton";
            startDetectionButton.Size = new Size(190, 62);
            startDetectionButton.TabIndex = 0;
            startDetectionButton.Text = "Start detection";
            startDetectionButton.UseVisualStyleBackColor = true;
            startDetectionButton.Click += startDetectionButton_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = SystemColors.ActiveCaption;
            pictureBox1.Location = new Point(32, 24);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(438, 213);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // selectFileButton
            // 
            selectFileButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            selectFileButton.Location = new Point(658, 131);
            selectFileButton.Name = "selectFileButton";
            selectFileButton.Size = new Size(129, 36);
            selectFileButton.TabIndex = 2;
            selectFileButton.Text = "Select local file";
            selectFileButton.UseVisualStyleBackColor = true;
            selectFileButton.Click += selectFileButton_Click;
            // 
            // selectedFileTextBox
            // 
            selectedFileTextBox.Enabled = false;
            selectedFileTextBox.Location = new Point(494, 89);
            selectedFileTextBox.Name = "selectedFileTextBox";
            selectedFileTextBox.Size = new Size(293, 27);
            selectedFileTextBox.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(494, 190);
            label1.Name = "label1";
            label1.Size = new Size(87, 20);
            label1.TabIndex = 4;
            label1.Text = "Confidence:";
            // 
            // confidenceUpDown
            // 
            confidenceUpDown.DecimalPlaces = 1;
            confidenceUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            confidenceUpDown.Location = new Point(637, 188);
            confidenceUpDown.Maximum = new decimal(new int[] { 9, 0, 0, 65536 });
            confidenceUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            confidenceUpDown.Name = "confidenceUpDown";
            confidenceUpDown.Size = new Size(150, 27);
            confidenceUpDown.TabIndex = 5;
            confidenceUpDown.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // userSettingsButton
            // 
            userSettingsButton.Location = new Point(693, 53);
            userSettingsButton.Name = "userSettingsButton";
            userSettingsButton.Size = new Size(94, 29);
            userSettingsButton.TabIndex = 6;
            userSettingsButton.Text = "Settings";
            userSettingsButton.UseVisualStyleBackColor = true;
            userSettingsButton.Click += userSettingsButton_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(504, 138);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(115, 24);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Use webcam";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button1
            // 
            button1.Location = new Point(494, 53);
            button1.Name = "button1";
            button1.Size = new Size(125, 29);
            button1.TabIndex = 8;
            button1.Text = "Show statistics";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(658, 12);
            button2.Name = "button2";
            button2.Size = new Size(130, 29);
            button2.TabIndex = 9;
            button2.Text = "Logout";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(checkBox1);
            Controls.Add(userSettingsButton);
            Controls.Add(confidenceUpDown);
            Controls.Add(label1);
            Controls.Add(selectedFileTextBox);
            Controls.Add(selectFileButton);
            Controls.Add(pictureBox1);
            Controls.Add(startDetectionButton);
            Name = "MainForm";
            Text = "Fire detection";
            FormClosed += Form1_FormClosed;
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)confidenceUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startDetectionButton;
        private PictureBox pictureBox1;
        private Button selectFileButton;
        private TextBox selectedFileTextBox;
        private Label label1;
        private NumericUpDown confidenceUpDown;
        private Button userSettingsButton;
        private CheckBox checkBox1;
        private Button button1;
        private Button button2;
    }
}
