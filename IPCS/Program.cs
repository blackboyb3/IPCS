﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using IPCS.Forms;
using IPCS.DatabaseManager;
using MetroFramework;
using MetroFramework.Components;
using IPCS.Data;

namespace IPCS
{
    public static class Program
    {
        #region Main Entury

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        #endregion

        #region Fields

        public static MetroStyleManager MainStyleManager { get; set; }

        public static TestDatabase Database { get; set; }
        
        public static User User { get; set; }

        #endregion

        #region Threads

        public static void InitializeServer()
        {
            Database = new TestDatabase();
        }

        public static void UpdateUser()
        {
            User.StyleManager = MainStyleManager;
            Database.UpdateUser(User);
        }

        public static bool Signup(string username, string password, string recoveryKey, Image profilePic)
        {
            User user = new User(username, password, recoveryKey, profilePic);
            return Database.CreateUser(user);
        }

        public static bool Login(string username, string password)
        {
            User = Database.GetUser(username, password);
            if (!UserReady()) return false;
            MainStyleManager = User.StyleManager;
            return true;
        }

        public static void Logout()
        {
            UpdateUser();
            User = null;
        }

        public static bool UserReady()
        {
            if (User == null) return false;
            return true;
        }

        #endregion

        #region Debug Methods

        public static void PrintDebug(string[] data)
        {
            string temp = "";
            for (int i = 0; data.Length > i; i++)
            {
                temp += data[i] + Environment.NewLine;
            }
            MessageBox.Show(temp);
        }

        public static void PrintDebug(string data)
        {
            MessageBox.Show(data);
        }

        #endregion
    }
}
