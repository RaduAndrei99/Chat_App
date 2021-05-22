namespace Chat_App.Views
{
    partial class FriendRequestsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FriendRequestsView));
            this.listviewFriendRequests = new System.Windows.Forms.ListView();
            this.buttonAccept = new MaterialSkin.Controls.MaterialRaisedButton();
            this.columnFriendRequests = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listviewFriendRequests
            // 
            this.listviewFriendRequests.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFriendRequests});
            this.listviewFriendRequests.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewFriendRequests.HideSelection = false;
            this.listviewFriendRequests.Location = new System.Drawing.Point(12, 78);
            this.listviewFriendRequests.MultiSelect = false;
            this.listviewFriendRequests.Name = "listviewFriendRequests";
            this.listviewFriendRequests.Size = new System.Drawing.Size(226, 259);
            this.listviewFriendRequests.TabIndex = 0;
            this.listviewFriendRequests.UseCompatibleStateImageBehavior = false;
            this.listviewFriendRequests.View = System.Windows.Forms.View.List;
            // 
            // buttonAccept
            // 
            this.buttonAccept.Depth = 0;
            this.buttonAccept.Location = new System.Drawing.Point(113, 353);
            this.buttonAccept.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Primary = true;
            this.buttonAccept.Size = new System.Drawing.Size(125, 35);
            this.buttonAccept.TabIndex = 1;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // columnFriendRequests
            // 
            this.columnFriendRequests.Width = 259;
            // 
            // FriendRequestsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 400);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.listviewFriendRequests);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FriendRequestsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Friend Requests";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FriendRequestsView_FormClosing);
            this.Load += new System.EventHandler(this.FriendRequestsView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listviewFriendRequests;
        private MaterialSkin.Controls.MaterialRaisedButton buttonAccept;
        private System.Windows.Forms.ColumnHeader columnFriendRequests;
    }
}