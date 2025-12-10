namespace Gui_Diplom.Forms
{
    partial class StatisticsForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            endDateTimePicker = new DateTimePicker();
            startDateTimePicker = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            searchButton = new Button();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel1 = new Panel();
            exportDataButton = new Button();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // endDateTimePicker
            // 
            endDateTimePicker.Location = new Point(618, 139);
            endDateTimePicker.Name = "endDateTimePicker";
            endDateTimePicker.Size = new Size(170, 27);
            endDateTimePicker.TabIndex = 0;
            // 
            // startDateTimePicker
            // 
            startDateTimePicker.Location = new Point(618, 91);
            startDateTimePicker.Name = "startDateTimePicker";
            startDateTimePicker.Size = new Size(170, 27);
            startDateTimePicker.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(482, 91);
            label1.Name = "label1";
            label1.Size = new Size(74, 20);
            label1.TabIndex = 2;
            label1.Text = "Start date";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(482, 139);
            label2.Name = "label2";
            label2.Size = new Size(68, 20);
            label2.TabIndex = 3;
            label2.Text = "End date";
            // 
            // searchButton
            // 
            searchButton.Location = new Point(618, 196);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(170, 36);
            searchButton.TabIndex = 4;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(3, 3);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(375, 375);
            chart1.TabIndex = 6;
            chart1.Text = "chart1";
            // 
            // panel1
            // 
            panel1.Controls.Add(chart1);
            panel1.Location = new Point(26, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(421, 393);
            panel1.TabIndex = 7;
            // 
            // exportDataButton
            // 
            exportDataButton.Location = new Point(618, 260);
            exportDataButton.Name = "exportDataButton";
            exportDataButton.Size = new Size(170, 34);
            exportDataButton.TabIndex = 8;
            exportDataButton.Text = "Export data";
            exportDataButton.UseVisualStyleBackColor = true;
            exportDataButton.Visible = false;
            exportDataButton.Click += exportDataButton_Click;
            // 
            // StatisticsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(exportDataButton);
            Controls.Add(panel1);
            Controls.Add(searchButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(startDateTimePicker);
            Controls.Add(endDateTimePicker);
            Name = "StatisticsForm";
            Text = "StatisticsForm";
            Load += StatisticsForm_Load;
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker endDateTimePicker;
        private DateTimePicker startDateTimePicker;
        private Label label1;
        private Label label2;
        private Button searchButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Panel panel1;
        private Button exportDataButton;
    }
}