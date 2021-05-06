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
            this.listviewFriends = new System.Windows.Forms.ListView();
            this.buttonAddFriend = new MaterialSkin.Controls.MaterialRaisedButton();
            this.labelActiveFriend = new MaterialSkin.Controls.MaterialLabel();
            this.dividerChat = new MaterialSkin.Controls.MaterialDivider();
            this.columnFriendList = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.textboxMessage.Size = new System.Drawing.Size(494, 30);
            this.textboxMessage.TabIndex = 1;
            // 
            // buttonSend
            // 
            this.buttonSend.Depth = 0;
            this.buttonSend.Location = new System.Drawing.Point(700, 390);
            this.buttonSend.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Primary = true;
            this.buttonSend.Size = new System.Drawing.Size(85, 30);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // listviewFriends
            // 
            this.listviewFriends.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFriendList});
            this.listviewFriends.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewFriends.GridLines = true;
            this.listviewFriends.HideSelection = false;
            this.listviewFriends.Location = new System.Drawing.Point(12, 120);
            this.listviewFriends.MultiSelect = false;
            this.listviewFriends.Name = "listviewFriends";
            this.listviewFriends.Size = new System.Drawing.Size(125, 300);
            this.listviewFriends.TabIndex = 3;
            this.listviewFriends.UseCompatibleStateImageBehavior = false;
            this.listviewFriends.View = System.Windows.Forms.View.List;
            this.listviewFriends.DoubleClick += new System.EventHandler(this.listviewFriends_DoubleClick);
            // 
            // buttonAddFriend
            // 
            this.buttonAddFriend.Depth = 0;
            this.buttonAddFriend.Location = new System.Drawing.Point(12, 84);
            this.buttonAddFriend.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonAddFriend.Name = "buttonAddFriend";
            this.buttonAddFriend.Primary = true;
            this.buttonAddFriend.Size = new System.Drawing.Size(125, 30);
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
            // columnFriendList
            // 
            this.columnFriendList.Text = "";
            this.columnFriendList.Width = 125;
            // 
            // ChatView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dividerChat);
            this.Controls.Add(this.labelActiveFriend);
            this.Controls.Add(this.buttonAddFriend);
            this.Controls.Add(this.listviewFriends);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textboxMessage);
            this.Controls.Add(this.listviewChat);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ChatView";
            this.Text = "ChatView";
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
        private System.Windows.Forms.ListView listviewFriends;
        private MaterialSkin.Controls.MaterialRaisedButton buttonAddFriend;
        private MaterialSkin.Controls.MaterialLabel labelActiveFriend;
        private MaterialSkin.Controls.MaterialDivider dividerChat;
        private System.Windows.Forms.ColumnHeader columnFriendList;
    }
}