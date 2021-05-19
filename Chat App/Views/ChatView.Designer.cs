namespace Chat_App.Views
{
    partial class ChatView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatView));
            this.listviewChat = new System.Windows.Forms.ListView();
            this.columnLeft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnRight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textboxMessage = new System.Windows.Forms.TextBox();
            this.buttonSend = new MaterialSkin.Controls.MaterialRaisedButton();
            this.buttonAddFriend = new MaterialSkin.Controls.MaterialRaisedButton();
            this.labelActiveFriend = new MaterialSkin.Controls.MaterialLabel();
            this.dividerChat = new MaterialSkin.Controls.MaterialDivider();
            this.listviewFriends = new System.Windows.Forms.ListView();
            this.buttonSettings = new MaterialSkin.Controls.MaterialRaisedButton();
            this.buttonLogout = new MaterialSkin.Controls.MaterialRaisedButton();
            this.buttonFriendRequests = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // listviewChat
            // 
            this.listviewChat.BackColor = System.Drawing.Color.White;
            this.listviewChat.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnLeft,
            this.columnRight});
            this.listviewChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewChat.GridLines = true;
            this.listviewChat.HideSelection = false;
            this.listviewChat.Location = new System.Drawing.Point(190, 120);
            this.listviewChat.Name = "listviewChat";
            this.listviewChat.Size = new System.Drawing.Size(594, 264);
            this.listviewChat.TabIndex = 0;
            this.listviewChat.UseCompatibleStateImageBehavior = false;
            this.listviewChat.View = System.Windows.Forms.View.List;
            // 
            // columnLeft
            // 
            this.columnLeft.Text = "";
            this.columnLeft.Width = 200;
            // 
            // columnRight
            // 
            this.columnRight.Text = "";
            this.columnRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnRight.Width = 200;
            // 
            // textboxMessage
            // 
            this.textboxMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxMessage.Location = new System.Drawing.Point(190, 390);
            this.textboxMessage.Multiline = true;
            this.textboxMessage.Name = "textboxMessage";
            this.textboxMessage.Size = new System.Drawing.Size(504, 30);
            this.textboxMessage.TabIndex = 1;
            // 
            // buttonSend
            // 
            this.buttonSend.AutoSize = true;
            this.buttonSend.Depth = 0;
            this.buttonSend.Location = new System.Drawing.Point(700, 390);
            this.buttonSend.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Primary = true;
            this.buttonSend.Size = new System.Drawing.Size(84, 30);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonAddFriend
            // 
            this.buttonAddFriend.AutoSize = true;
            this.buttonAddFriend.Depth = 0;
            this.buttonAddFriend.Location = new System.Drawing.Point(13, 134);
            this.buttonAddFriend.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonAddFriend.Name = "buttonAddFriend";
            this.buttonAddFriend.Primary = true;
            this.buttonAddFriend.Size = new System.Drawing.Size(137, 37);
            this.buttonAddFriend.TabIndex = 4;
            this.buttonAddFriend.Text = "Add Friend";
            this.buttonAddFriend.UseVisualStyleBackColor = true;
            this.buttonAddFriend.Click += new System.EventHandler(this.buttonAddFriend_Click);
            // 
            // labelActiveFriend
            // 
            this.labelActiveFriend.Depth = 0;
            this.labelActiveFriend.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelActiveFriend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.labelActiveFriend.Location = new System.Drawing.Point(187, 84);
            this.labelActiveFriend.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelActiveFriend.Name = "labelActiveFriend";
            this.labelActiveFriend.Size = new System.Drawing.Size(150, 30);
            this.labelActiveFriend.TabIndex = 5;
            // 
            // dividerChat
            // 
            this.dividerChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dividerChat.Depth = 0;
            this.dividerChat.Location = new System.Drawing.Point(155, 84);
            this.dividerChat.MouseState = MaterialSkin.MouseState.HOVER;
            this.dividerChat.Name = "dividerChat";
            this.dividerChat.Size = new System.Drawing.Size(19, 336);
            this.dividerChat.TabIndex = 6;
            this.dividerChat.Text = "materialDivider1";
            // 
            // listviewFriends
            // 
            this.listviewFriends.HideSelection = false;
            this.listviewFriends.Location = new System.Drawing.Point(13, 177);
            this.listviewFriends.Name = "listviewFriends";
            this.listviewFriends.Size = new System.Drawing.Size(136, 243);
            this.listviewFriends.TabIndex = 7;
            this.listviewFriends.UseCompatibleStateImageBehavior = false;
            this.listviewFriends.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listviewFriends_MouseDoubleClick);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSettings.AutoSize = true;
            this.buttonSettings.Depth = 0;
            this.buttonSettings.Location = new System.Drawing.Point(610, 79);
            this.buttonSettings.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Primary = true;
            this.buttonSettings.Size = new System.Drawing.Size(84, 30);
            this.buttonSettings.TabIndex = 8;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonLogout
            // 
            this.buttonLogout.AutoSize = true;
            this.buttonLogout.Depth = 0;
            this.buttonLogout.Location = new System.Drawing.Point(700, 79);
            this.buttonLogout.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Primary = true;
            this.buttonLogout.Size = new System.Drawing.Size(84, 30);
            this.buttonLogout.TabIndex = 9;
            this.buttonLogout.Text = "Log out";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // buttonFriendRequests
            // 
            this.buttonFriendRequests.AutoSize = true;
            this.buttonFriendRequests.Depth = 0;
            this.buttonFriendRequests.Location = new System.Drawing.Point(12, 91);
            this.buttonFriendRequests.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonFriendRequests.Name = "buttonFriendRequests";
            this.buttonFriendRequests.Primary = true;
            this.buttonFriendRequests.Size = new System.Drawing.Size(137, 37);
            this.buttonFriendRequests.TabIndex = 10;
            this.buttonFriendRequests.Text = "Friend Requests";
            this.buttonFriendRequests.UseVisualStyleBackColor = true;
            this.buttonFriendRequests.Click += new System.EventHandler(this.buttonFriendRequests_Click);
            // 
            // ChatView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonFriendRequests);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.listviewFriends);
            this.Controls.Add(this.dividerChat);
            this.Controls.Add(this.labelActiveFriend);
            this.Controls.Add(this.buttonAddFriend);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textboxMessage);
            this.Controls.Add(this.listviewChat);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ChatView";
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatView_FormClosing);
            this.Load += new System.EventHandler(this.ChatView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listviewChat;
        private System.Windows.Forms.TextBox textboxMessage;
        private MaterialSkin.Controls.MaterialRaisedButton buttonSend;
        private System.Windows.Forms.ColumnHeader columnLeft;
        private System.Windows.Forms.ColumnHeader columnRight;
        private MaterialSkin.Controls.MaterialRaisedButton buttonAddFriend;
        private MaterialSkin.Controls.MaterialLabel labelActiveFriend;
        private MaterialSkin.Controls.MaterialDivider dividerChat;
        private System.Windows.Forms.ListView listviewFriends;
        private MaterialSkin.Controls.MaterialRaisedButton buttonSettings;
        private MaterialSkin.Controls.MaterialRaisedButton buttonLogout;
        private MaterialSkin.Controls.MaterialRaisedButton buttonFriendRequests;
    }
}