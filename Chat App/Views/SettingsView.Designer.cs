namespace Chat_App.Views
{
    partial class SettingsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsView));
            this.radioButtonBlue = new MaterialSkin.Controls.MaterialRadioButton();
            this.radioButtonLight = new MaterialSkin.Controls.MaterialRadioButton();
            this.radioButtonDark = new MaterialSkin.Controls.MaterialRadioButton();
            this.radioButtonOrange = new MaterialSkin.Controls.MaterialRadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelColor = new MaterialSkin.Controls.MaterialLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelTheme = new MaterialSkin.Controls.MaterialLabel();
            this.buttonApply = new MaterialSkin.Controls.MaterialRaisedButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonBlue
            // 
            this.radioButtonBlue.AutoSize = true;
            this.radioButtonBlue.Depth = 0;
            this.radioButtonBlue.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioButtonBlue.Location = new System.Drawing.Point(24, 103);
            this.radioButtonBlue.Margin = new System.Windows.Forms.Padding(0);
            this.radioButtonBlue.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioButtonBlue.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioButtonBlue.Name = "radioButtonBlue";
            this.radioButtonBlue.Ripple = true;
            this.radioButtonBlue.Size = new System.Drawing.Size(56, 30);
            this.radioButtonBlue.TabIndex = 0;
            this.radioButtonBlue.Text = "Blue";
            this.radioButtonBlue.UseVisualStyleBackColor = true;
            this.radioButtonBlue.CheckedChanged += new System.EventHandler(this.radioButtonBlue_CheckedChanged);
            // 
            // radioButtonLight
            // 
            this.radioButtonLight.AutoSize = true;
            this.radioButtonLight.Depth = 0;
            this.radioButtonLight.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioButtonLight.Location = new System.Drawing.Point(13, 103);
            this.radioButtonLight.Margin = new System.Windows.Forms.Padding(0);
            this.radioButtonLight.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioButtonLight.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioButtonLight.Name = "radioButtonLight";
            this.radioButtonLight.Ripple = true;
            this.radioButtonLight.Size = new System.Drawing.Size(60, 30);
            this.radioButtonLight.TabIndex = 3;
            this.radioButtonLight.Text = "Light";
            this.radioButtonLight.UseVisualStyleBackColor = true;
            this.radioButtonLight.Click += new System.EventHandler(this.radioButtonWhite_Click);
            // 
            // radioButtonDark
            // 
            this.radioButtonDark.AutoSize = true;
            this.radioButtonDark.Checked = true;
            this.radioButtonDark.Depth = 0;
            this.radioButtonDark.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioButtonDark.Location = new System.Drawing.Point(13, 61);
            this.radioButtonDark.Margin = new System.Windows.Forms.Padding(0);
            this.radioButtonDark.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioButtonDark.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioButtonDark.Name = "radioButtonDark";
            this.radioButtonDark.Ripple = true;
            this.radioButtonDark.Size = new System.Drawing.Size(57, 30);
            this.radioButtonDark.TabIndex = 2;
            this.radioButtonDark.TabStop = true;
            this.radioButtonDark.Text = "Dark";
            this.radioButtonDark.UseVisualStyleBackColor = true;
            this.radioButtonDark.Click += new System.EventHandler(this.radioButtonDark_Click);
            // 
            // radioButtonOrange
            // 
            this.radioButtonOrange.AutoSize = true;
            this.radioButtonOrange.Checked = true;
            this.radioButtonOrange.Depth = 0;
            this.radioButtonOrange.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioButtonOrange.Location = new System.Drawing.Point(24, 61);
            this.radioButtonOrange.Margin = new System.Windows.Forms.Padding(0);
            this.radioButtonOrange.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioButtonOrange.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioButtonOrange.Name = "radioButtonOrange";
            this.radioButtonOrange.Ripple = true;
            this.radioButtonOrange.Size = new System.Drawing.Size(73, 30);
            this.radioButtonOrange.TabIndex = 1;
            this.radioButtonOrange.TabStop = true;
            this.radioButtonOrange.Text = "Orange";
            this.radioButtonOrange.UseVisualStyleBackColor = true;
            this.radioButtonOrange.Click += new System.EventHandler(this.radioButtonOrange_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelColor);
            this.panel1.Controls.Add(this.radioButtonOrange);
            this.panel1.Controls.Add(this.radioButtonBlue);
            this.panel1.Location = new System.Drawing.Point(26, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 157);
            this.panel1.TabIndex = 2;
            // 
            // labelColor
            // 
            this.labelColor.AutoSize = true;
            this.labelColor.Depth = 0;
            this.labelColor.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.labelColor.Location = new System.Drawing.Point(34, 16);
            this.labelColor.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelColor.Name = "labelColor";
            this.labelColor.Size = new System.Drawing.Size(46, 19);
            this.labelColor.TabIndex = 2;
            this.labelColor.Text = "Color";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelTheme);
            this.panel2.Controls.Add(this.radioButtonDark);
            this.panel2.Controls.Add(this.radioButtonLight);
            this.panel2.Location = new System.Drawing.Point(167, 77);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(120, 157);
            this.panel2.TabIndex = 3;
            // 
            // labelTheme
            // 
            this.labelTheme.AutoSize = true;
            this.labelTheme.Depth = 0;
            this.labelTheme.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelTheme.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.labelTheme.Location = new System.Drawing.Point(22, 16);
            this.labelTheme.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelTheme.Name = "labelTheme";
            this.labelTheme.Size = new System.Drawing.Size(55, 19);
            this.labelTheme.TabIndex = 3;
            this.labelTheme.Text = "Theme";
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonApply.AutoSize = true;
            this.buttonApply.Depth = 0;
            this.buttonApply.Location = new System.Drawing.Point(230, 249);
            this.buttonApply.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Primary = true;
            this.buttonApply.Size = new System.Drawing.Size(95, 30);
            this.buttonApply.TabIndex = 6;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 300);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsView";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsView_FormClosing);
            this.Load += new System.EventHandler(this.SettingsView_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialRadioButton radioButtonBlue;
        private MaterialSkin.Controls.MaterialRadioButton radioButtonLight;
        private MaterialSkin.Controls.MaterialRadioButton radioButtonDark;
        private MaterialSkin.Controls.MaterialRadioButton radioButtonOrange;
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialLabel labelColor;
        private System.Windows.Forms.Panel panel2;
        private MaterialSkin.Controls.MaterialLabel labelTheme;
        private MaterialSkin.Controls.MaterialRaisedButton buttonApply;
    }
}