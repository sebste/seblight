namespace seblight
{
    partial class SeblightForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeblightForm));
            this.testLedButton = new System.Windows.Forms.Button();
            this.close_button = new System.Windows.Forms.Button();
            this.set_color_button = new System.Windows.Forms.Button();
            this.capture_button = new System.Windows.Forms.Button();
            this.brightness_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.status_label = new System.Windows.Forms.Label();
            this.status_label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // testLedButton
            // 
            this.testLedButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.testLedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.testLedButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.testLedButton.Location = new System.Drawing.Point(35, 231);
            this.testLedButton.Name = "testLedButton";
            this.testLedButton.Size = new System.Drawing.Size(213, 23);
            this.testLedButton.TabIndex = 0;
            this.testLedButton.Text = "Test LED";
            this.testLedButton.UseVisualStyleBackColor = false;
            this.testLedButton.Click += new System.EventHandler(this.testLedButton_Click);
            // 
            // close_button
            // 
            this.close_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.close_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close_button.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.close_button.Location = new System.Drawing.Point(35, 260);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(213, 23);
            this.close_button.TabIndex = 3;
            this.close_button.Text = "Close";
            this.close_button.UseVisualStyleBackColor = false;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // set_color_button
            // 
            this.set_color_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.set_color_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.set_color_button.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.set_color_button.Location = new System.Drawing.Point(35, 173);
            this.set_color_button.Name = "set_color_button";
            this.set_color_button.Size = new System.Drawing.Size(213, 23);
            this.set_color_button.TabIndex = 4;
            this.set_color_button.Text = "Select color";
            this.set_color_button.UseVisualStyleBackColor = false;
            this.set_color_button.Click += new System.EventHandler(this.set_color_button_Click);
            // 
            // capture_button
            // 
            this.capture_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.capture_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.capture_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.capture_button.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.capture_button.Location = new System.Drawing.Point(35, 144);
            this.capture_button.Name = "capture_button";
            this.capture_button.Size = new System.Drawing.Size(213, 23);
            this.capture_button.TabIndex = 5;
            this.capture_button.Text = "Start screen capture";
            this.capture_button.UseVisualStyleBackColor = false;
            this.capture_button.Click += new System.EventHandler(this.capture_button_Click);
            // 
            // brightness_button
            // 
            this.brightness_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.brightness_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.brightness_button.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.brightness_button.Location = new System.Drawing.Point(35, 202);
            this.brightness_button.Name = "brightness_button";
            this.brightness_button.Size = new System.Drawing.Size(213, 23);
            this.brightness_button.TabIndex = 6;
            this.brightness_button.Text = "Set brightness";
            this.brightness_button.UseVisualStyleBackColor = false;
            this.brightness_button.Click += new System.EventHandler(this.brightness_button_Click_1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::seblight.Properties.Resources.television_icon;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(96, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(85, 79);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(200, 294);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "SebLight";
            // 
            // status_label
            // 
            this.status_label.AutoSize = true;
            this.status_label.BackColor = System.Drawing.Color.Transparent;
            this.status_label.ForeColor = System.Drawing.Color.Silver;
            this.status_label.Location = new System.Drawing.Point(13, 303);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(0, 13);
            this.status_label.TabIndex = 9;
            // 
            // status_label2
            // 
            this.status_label2.AutoSize = true;
            this.status_label2.BackColor = System.Drawing.Color.Transparent;
            this.status_label2.ForeColor = System.Drawing.Color.Silver;
            this.status_label2.Location = new System.Drawing.Point(96, 303);
            this.status_label2.Name = "status_label2";
            this.status_label2.Size = new System.Drawing.Size(0, 13);
            this.status_label2.TabIndex = 10;
            // 
            // SeblightForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.BackgroundImage = global::seblight.Properties.Resources.Background_backgrounds_desktop_reloaded_windows_logon_wallpaper;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(278, 326);
            this.Controls.Add(this.status_label2);
            this.Controls.Add(this.status_label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.brightness_button);
            this.Controls.Add(this.capture_button);
            this.Controls.Add(this.set_color_button);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.testLedButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SeblightForm";
            this.Text = "Seblight";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SeblightForm_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SeblightForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SeblightForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SeblightForm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button testLedButton;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.Button set_color_button;
        private System.Windows.Forms.Button capture_button;
        private System.Windows.Forms.Button brightness_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label status_label;
        private System.Windows.Forms.Label status_label2;
    }
}

