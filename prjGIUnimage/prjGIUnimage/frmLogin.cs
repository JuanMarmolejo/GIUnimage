﻿using prjGIUnimage.bus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGIUnimage
{
    public partial class frmLogin : Form
    {
        clsListUser lstUsers = new clsListUser();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            lstUsers.AllUsers();
            cboUser.DisplayMember = "Username";
            cboUser.ValueMember = "UserID";
            cboUser.DataSource = lstUsers.Elements;

            clsGlobals.GIPar = new clsGIParameter();
            clsGlobals.GIPar.GetIDVariables();
            clsGlobals.GIPar.GetSurplusParameters();
            clsGlobals.GIPar.GetUserID();

            cboUser.SelectedValue = clsGlobals.GIPar.UserID;
            clsGlobals.ActiveRatio = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsUser ActiveUser = lstUsers.UserByID(Convert.ToInt32(cboUser.SelectedValue));
            //string source = txtPassword.Text.Trim();
            string source = "JCmm2587";
            using (MD5 md5Hash = MD5.Create())
            {
                //clsGlobals.GIPar.UserID = ActiveUser.UserID;
                //this.DialogResult = DialogResult.OK;
                if (VerifyMd5Hash(md5Hash, source, ActiveUser.Password))
                {
                    clsGlobals.GIPar.UserID = ActiveUser.UserID;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.No;
                }
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        //private void cboUser_Validating(object sender, CancelEventArgs e)
        //{
        //    if (cboUser.SelectedIndex < 0)
        //    {
        //        e.Cancel = true;
        //        cboUser.Focus();
        //        errorProvider.SetError(cboUser, "Sélectionnez un utilisateur");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider.SetError(cboUser, null);
        //    }
        //}

        //private void txtPassword_Validating(object sender, CancelEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtPassword.Text))
        //    {
        //        e.Cancel = true;
        //        cboUser.Focus();
        //        errorProvider.SetError(txtPassword, "Entrez un mot de passe");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider.SetError(cboUser, null);
        //    }
        //}
    }
}