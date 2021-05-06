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
            ((System.ComponentModel.ISupportInitialize)(this.pictureUser)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureUser
            // 
            this.pictureUser.Image = global::Chat_App.Properties.Resources.outline_account_circle_white_48dp;
            this.pictureUser.Location = new System.Drawing.Point(125, 60);
            this.pictureUser.Name = "pictureUser";
            this.pictureUser.Size = new System.Drawing.Size(150, 150);
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
            this.buttonLogIn.Depth = 0;
            this.buttonLogIn.Location = new System.Drawing.Point(150, 381);
            this.buttonLogIn.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonLogIn.Name = "buttonLogIn";
            this.buttonLogIn.Primary = true;
            this.buttonLogIn.Size = new System.Drawing.Size(100, 30);
            this.buttonLogIn.TabIndex = 5;
            this.buttonLogIn.Text = "Log In";
            this.buttonLogIn.UseVisualStyleBackColor = true;
            this.buttonLogIn.Click += new System.EventHandler(this.buttonLogIn_Click);
            // 
            // buttonRegister
            // 
            this.buttonRegister.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRegister.Depth = 0;
            this.buttonRegister.Location = new System.Drawing.Point(150, 427);
            this.buttonRegister.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Primary = true;
            this.buttonRegister.Size = new System.Drawing.Size(100, 30);
            this.buttonRegister.TabIndex = 6;
            this.buttonRegister.Text = "Register";
            this.buttonRegister.UseVisualStyleBackColor = true;
            // 
            // LogInControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
    }
}
