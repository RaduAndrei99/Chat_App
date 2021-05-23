namespace Chat_App.Views
{
    partial class LogInControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureUser = new System.Windows.Forms.PictureBox();
            this.labelUsername = new MaterialSkin.Controls.MaterialLabel();
            this.labelPassword = new MaterialSkin.Controls.MaterialLabel();
            this.textboxUsername = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textboxPassword = new System.Windows.Forms.TextBox();
            this.buttonLogIn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.buttonRegister = new MaterialSkin.Controls.MaterialRaisedButton();
            this.buttonSettings = new MaterialSkin.Controls.MaterialRaisedButton();
            this.labelErrorMessage = new MaterialSkin.Controls.MaterialLabel();
            this.buttonHelp = new MaterialSkin.Controls.MaterialRaisedButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUser)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureUser
            // 
            this.pictureUser.Image = global::Chat_App.Properties.Resources.logo;
            this.pictureUser.Location = new System.Drawing.Point(13, 30);
            this.pictureUser.Name = "pictureUser";
            this.pictureUser.Size = new System.Drawing.Size(375, 193);
            this.pictureUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureUser.TabIndex = 0;
            this.pictureUser.TabStop = false;
            // 
            // labelUsername
            // 
            this.labelUsername.Depth = 0;
            this.labelUsername.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelUsername.Location = new System.Drawing.Point(120, 250);
            this.labelUsername.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(80, 20);
            this.labelUsername.TabIndex = 1;
            this.labelUsername.Text = "Username";
            this.labelUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPassword
            // 
            this.labelPassword.Depth = 0;
            this.labelPassword.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelPassword.Location = new System.Drawing.Point(120, 310);
            this.labelPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(80, 20);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Password";
            this.labelPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textboxUsername
            // 
            this.textboxUsername.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textboxUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxUsername.Location = new System.Drawing.Point(120, 275);
            this.textboxUsername.Name = "textboxUsername";
            this.textboxUsername.Size = new System.Drawing.Size(160, 22);
            this.textboxUsername.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // textboxPassword
            // 
            this.textboxPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textboxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxPassword.Location = new System.Drawing.Point(120, 335);
            this.textboxPassword.Name = "textboxPassword";
            this.textboxPassword.Size = new System.Drawing.Size(160, 22);
            this.textboxPassword.TabIndex = 4;
            this.textboxPassword.UseSystemPasswordChar = true;
            // 
            // buttonLogIn
            // 
            this.buttonLogIn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonLogIn.AutoSize = true;
            this.buttonLogIn.Depth = 0;
            this.buttonLogIn.Location = new System.Drawing.Point(150, 370);
            this.buttonLogIn.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonLogIn.Name = "buttonLogIn";
            this.buttonLogIn.Primary = true;
            this.buttonLogIn.Size = new System.Drawing.Size(95, 30);
            this.buttonLogIn.TabIndex = 5;
            this.buttonLogIn.Text = "Log In";
            this.buttonLogIn.UseVisualStyleBackColor = true;
            this.buttonLogIn.Click += new System.EventHandler(this.buttonLogIn_Click);
            // 
            // buttonRegister
            // 
            this.buttonRegister.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRegister.AutoSize = true;
            this.buttonRegister.Depth = 0;
            this.buttonRegister.Location = new System.Drawing.Point(150, 406);
            this.buttonRegister.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Primary = true;
            this.buttonRegister.Size = new System.Drawing.Size(95, 30);
            this.buttonRegister.TabIndex = 6;
            this.buttonRegister.Text = "Register";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSettings.AutoSize = true;
            this.buttonSettings.Depth = 0;
            this.buttonSettings.Location = new System.Drawing.Point(150, 442);
            this.buttonSettings.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Primary = true;
            this.buttonSettings.Size = new System.Drawing.Size(95, 30);
            this.buttonSettings.TabIndex = 7;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // labelErrorMessage
            // 
            this.labelErrorMessage.Depth = 0;
            this.labelErrorMessage.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelErrorMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelErrorMessage.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.labelErrorMessage.Location = new System.Drawing.Point(3, 225);
            this.labelErrorMessage.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelErrorMessage.Name = "labelErrorMessage";
            this.labelErrorMessage.Size = new System.Drawing.Size(394, 25);
            this.labelErrorMessage.TabIndex = 8;
            this.labelErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonHelp
            // 
            this.buttonHelp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonHelp.AutoSize = true;
            this.buttonHelp.Depth = 0;
            this.buttonHelp.Location = new System.Drawing.Point(293, 467);
            this.buttonHelp.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Primary = true;
            this.buttonHelp.Size = new System.Drawing.Size(95, 30);
            this.buttonHelp.TabIndex = 9;
            this.buttonHelp.Text = "Help";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // LogInControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.labelErrorMessage);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.buttonLogIn);
            this.Controls.Add(this.textboxPassword);
            this.Controls.Add(this.textboxUsername);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.pictureUser);
            this.Name = "LogInControl";
            this.Size = new System.Drawing.Size(400, 500);
            ((System.ComponentModel.ISupportInitialize)(this.pictureUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureUser;
        private MaterialSkin.Controls.MaterialLabel labelUsername;
        private MaterialSkin.Controls.MaterialLabel labelPassword;
        private System.Windows.Forms.TextBox textboxUsername;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox textboxPassword;
        private MaterialSkin.Controls.MaterialRaisedButton buttonLogIn;
        private MaterialSkin.Controls.MaterialRaisedButton buttonRegister;
        private MaterialSkin.Controls.MaterialRaisedButton buttonSettings;
        private MaterialSkin.Controls.MaterialLabel labelErrorMessage;
        private MaterialSkin.Controls.MaterialRaisedButton buttonHelp;
    }
}
