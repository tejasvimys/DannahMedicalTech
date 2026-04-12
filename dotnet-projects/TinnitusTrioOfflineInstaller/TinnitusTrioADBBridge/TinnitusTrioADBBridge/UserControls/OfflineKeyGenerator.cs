using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace TinnitusTrioADBBridge.UserControls
{
    public partial class OfflineKeyGenerator : UserControl
    {
        private string _userId;
        public OfflineKeyGenerator(string userId)
        {
            _userId = userId;
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Equals(""))
            {
                XtraMessageBox.Show("Please Enter the Key from the Android Device Popup!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var myAlert =
                XtraMessageBox.Show(
                    "Have you verified that the Key entered here is correct? Key once saved cannot be reverted back !",
                    "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (myAlert != DialogResult.Yes) return;

            var myString = Hash(textEdit1.Text.Trim());
            var uniqueId = myString.Substring(0, 8);

            var balLicensceManager = new TinnitusTrioADB_BAL.LicenseManager();
            var objSaveLicenseDetails = balLicensceManager.SaveUniqueIdDetails(uniqueId, textEdit1.Text.Trim(), _userId);

            if (objSaveLicenseDetails == "1")
            {
                lblUniqueId.Text = uniqueId;
                simpleButton1.Enabled = false;
            }

            else
            {
                XtraMessageBox.Show(
                     "UniqueId not generated and saved correctly. Please retry!. If the problem persists, please contact Administrator of Tinnitus Trio",
                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } 
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

        private void OfflineKeyGenerator_Load(object sender, EventArgs e)
        {
            //check if the key exists for the user. if exists, disable the unique id button and 
            var licenseManager = new TinnitusTrioADB_BAL.LicenseManager();
            var objLicenseExists = licenseManager.CheckifUniqueIdExists(_userId);

            if (string.IsNullOrEmpty(objLicenseExists.UniqueId)) return;
            lblUniqueId.Text = objLicenseExists.UniqueId;
            textEdit1.Text = objLicenseExists.KeyFromPhone;
            simpleButton1.Enabled = false;
        }
    }
}
