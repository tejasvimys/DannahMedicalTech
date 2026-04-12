using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace TinnitusTrioOfflineKeyGenerator
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Equals(""))
            {
                XtraMessageBox.Show("Please Enter the Key from the Android Device Popup!","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var myString = Hash(textEdit1.Text.Trim());
            var uniqueId = myString.Substring(0, 8);

            lblUniqueId.Text = uniqueId;

        }

        public static string Hash(string input)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (var b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
