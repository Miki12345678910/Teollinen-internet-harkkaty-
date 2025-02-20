namespace ListViewExample
{
    partial class ListViewFormExample
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
            listView1 = new ListView();
            columnHeaderTime = new ColumnHeader();
            columnHeaderPressure = new ColumnHeader();
            columnHeaderTemperature = new ColumnHeader();
            buttonTest = new Button();
            buttonStart = new Button();
            buttonStop = new Button();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeaderTime, columnHeaderPressure, columnHeaderTemperature });
            listView1.Location = new Point(12, 84);
            listView1.Name = "listView1";
            listView1.Size = new Size(534, 274);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // columnHeaderTime
            // 
            columnHeaderTime.Text = "Time";
            columnHeaderTime.Width = 100;
            // 
            // columnHeaderPressure
            // 
            columnHeaderPressure.Text = "Pressure";
            columnHeaderPressure.Width = 200;
            // 
            // columnHeaderTemperature
            // 
            columnHeaderTemperature.Text = "Temperature";
            columnHeaderTemperature.Width = 200;
            // 
            // buttonTest
            // 
            buttonTest.Location = new Point(12, 397);
            buttonTest.Name = "buttonTest";
            buttonTest.Size = new Size(112, 34);
            buttonTest.TabIndex = 1;
            buttonTest.Text = "Test";
            buttonTest.UseVisualStyleBackColor = true;
            // buttonTest.Click += buttonTest_Click;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(200, 397);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(112, 34);
            buttonStart.TabIndex = 2;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(374, 397);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(112, 34);
            buttonStop.TabIndex = 3;
            buttonStop.Text = "Stop";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += buttonStop_Click;
            // 
            // ListViewFormExample
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(567, 450);
            Controls.Add(buttonStop);
            Controls.Add(buttonStart);
            Controls.Add(buttonTest);
            Controls.Add(listView1);
            Name = "ListViewFormExample";
            Text = "ListView Esimerkki";
            FormClosing += ListViewFormExample_FormClosing;
            Load += ListViewFormExample_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListView listView1;
        private ColumnHeader columnHeaderTime;
        private ColumnHeader columnHeaderPressure;
        private ColumnHeader columnHeaderTemperature;
        private Button buttonTest;
        private Button buttonStart;
        private Button buttonStop;
    }
}
