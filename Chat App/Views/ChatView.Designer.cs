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
            this.listviewChat = new System.Windows.Forms.ListView();
            this.textboxMessage = new System.Windows.Forms.TextBox();
            this.buttonSend = new MaterialSkin.Controls.MaterialRaisedButton();
            this.columnLeft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnRight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listviewChat
            // 
            this.listviewChat.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listviewChat.BackColor = System.Drawing.Color.White;
            this.listviewChat.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnLeft,
            this.columnRight});
            this.listviewChat.GridLines = true;
            this.listviewChat.HideSelection = false;
            this.listviewChat.Location = new System.Drawing.Point(135, 75);
            this.listviewChat.Name = "listviewChat";
            this.listviewChat.Size = new System.Drawing.Size(650, 300);
            this.listviewChat.TabIndex = 0;
            this.listviewChat.UseCompatibleStateImageBehavior = false;
            this.listviewChat.View = System.Windows.Forms.View.SmallIcon;
            // 
            // textboxMessage
            // 
            this.textboxMessage.Location = new System.Drawing.Point(135, 390);
            this.textboxMessage.Multiline = true;
            this.textboxMessage.Name = "textboxMessage";
            this.textboxMessage.Size = new System.Drawing.Size(550, 30);
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
            this.buttonSend.Text = "SEND";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
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
            // ChatView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textboxMessage);
            this.Controls.Add(this.listviewChat);
            this.Name = "ChatView";
            this.Text = "ChatView";
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
    }
}