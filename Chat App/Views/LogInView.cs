﻿using Chat_App.Views;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_App
{
    public partial class LogInView : BasicView
    {
        private static LogInView _instance;
        private BasicView _chatForm;
        private BasicView _settingsForm;
        private BasicView _registerForm;
        private LogInControl _control;

        public static LogInView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LogInView();
                return _instance;
            }
        }

        public BasicView ChatForm
        {
            get
            {
                return _chatForm;
            }
        }

        public BasicView SettingsForm
        {
            get
            {
                return _settingsForm;
            }
        }

        public BasicView RegisterForm
        {
            get
            {
                return _registerForm;
            }
        }

        public LogInControl Control
        {
            get
            {
                return _control;
            }
        }

        private LogInView() : base()
        {
            InitializeComponent();
            _control = new LogInControl(this);
            _chatForm = ChatView.Instance;
            _settingsForm = SettingsView.Instance;
            _registerForm = RegisterView.Instance;
            _chatForm.StartPosition = FormStartPosition.CenterScreen;
            _settingsForm.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            _instance = this;
            panelUser.Controls.Add(_control);
        }

        private void LogInView_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChatApp.Instance.Close();
        }
    }
}
