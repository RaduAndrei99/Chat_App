namespace Chat_App.Views
{
    partial class AddFriendView
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
            this.textfieldFriendName = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.buttonAdd = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // textfieldFriendName
            // 
            this.textfieldFriendName.Depth = 0;
            this.textfieldFriendName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textfieldFriendName.Hint = "";
            this.textfieldFriendName.Location = new System.Drawing.Point(40, 95);
            this.textfieldFriendName.MouseState = MaterialSkin.MouseState.HOVER;
            this.textfieldFriendName.Name = "textfieldFriendName";
            this.textfieldFriendName.PasswordChar = '\0';
            this.textfieldFriendName.SelectedText = "";
            this.textfieldFriendName.SelectionLength = 0;
            this.textfieldFriendName.SelectionStart = 0;
            this.textfieldFriendName.Size = new System.Drawing.Size(220, 23);
            this.textfieldFriendName.TabIndex = 0;
            this.textfieldFriendName.UseSystemPasswordChar = false;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Depth = 0;
            this.buttonAdd.Location = new System.Drawing.Point(185, 140);
            this.buttonAdd.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Primary = true;
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // AddFriendView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textfieldFriendName);
            this.MinimizeBox = false;
            this.Name = "AddFriendView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Friend";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddFriendView_FormClosing);
            this.Load += new System.EventHandler(this.AddFriendView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField textfieldFriendName;
        private MaterialSkin.Controls.MaterialRaisedButton buttonAdd;
    }
}