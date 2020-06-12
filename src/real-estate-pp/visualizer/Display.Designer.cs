namespace visualizer
{
	partial class Display
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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
			this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.btnNext = new System.Windows.Forms.Button();
			this.btnPrev = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
			this.SuspendLayout();
			// 
			// chart
			// 
			chartArea3.Name = "ChartArea1";
			this.chart.ChartAreas.Add(chartArea3);
			this.chart.Dock = System.Windows.Forms.DockStyle.Top;
			legend3.Name = "Legend1";
			this.chart.Legends.Add(legend3);
			this.chart.Location = new System.Drawing.Point(0, 0);
			this.chart.Name = "chart";
			series3.ChartArea = "ChartArea1";
			series3.Legend = "Legend1";
			series3.Name = "Series1";
			this.chart.Series.Add(series3);
			this.chart.Size = new System.Drawing.Size(635, 461);
			this.chart.TabIndex = 0;
			this.chart.Text = "Chart";
			title3.Name = "Title";
			title3.Text = "Data Display";
			this.chart.Titles.Add(title3);
			// 
			// btnNext
			// 
			this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNext.Location = new System.Drawing.Point(536, 461);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(100, 23);
			this.btnNext.TabIndex = 1;
			this.btnNext.Text = ">";
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// btnPrev
			// 
			this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnPrev.Location = new System.Drawing.Point(-1, 461);
			this.btnPrev.Name = "btnPrev";
			this.btnPrev.Size = new System.Drawing.Size(100, 23);
			this.btnPrev.TabIndex = 2;
			this.btnPrev.Text = "<";
			this.btnPrev.UseVisualStyleBackColor = true;
			this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
			// 
			// Display
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(635, 483);
			this.Controls.Add(this.btnPrev);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.chart);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Display";
			this.ShowIcon = false;
			this.Text = "Data Visualization";
			this.Load += new System.EventHandler(this.Display_Load);
			((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart chart;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnPrev;
	}
}

