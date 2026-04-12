using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Data.Helpers;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using TinnitusTrioADB_BAL;

namespace TinnitusTrioADBBridge
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        /// 


        static readonly GetLicense MyLicense = new GetLicense();
        static readonly Login Login = new Login();
        [STAThread]
        static void Main()
        {
            try
            {
                //Application.Run(new Form1("", "", ""));
                //Application.Run(new LicenseManager());

                SplashForm.ShowSplashScreen();
                var isinternetavailable = CheckforInternet();

                if (isinternetavailable)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    DevExpress.Skins.SkinManager.EnableFormSkins();
                    DevExpress.UserSkins.BonusSkins.Register();
                    UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

                    var isActive = MyLicense.GetLicenseKey().IsActive;

                    switch (isActive)
                    {
                        case "True":
                            SplashForm.CloseForm();
                            //Application.Run(new QueryScreen(MyLicense.GetLicenseKey().RegistrarName, MyLicense.GetLicenseKey().RegistrarId));

                            var isFirstLogin = Login.CheckFirstLogin(MyLicense.GetLicenseKey().RegistrarId);

                            if (isFirstLogin == "FIRSTLOGIN")
                            {
                                Application.Run(new ResetPassword(MyLicense.GetLicenseKey().RegistrarId,
                                    MyLicense.GetLicenseKey().RegistrarName));
                            }

                            else
                            {
                                Application.Run(new LoginForm(MyLicense.GetLicenseKey().RegistrarId,
                                    MyLicense.GetLicenseKey().RegistrarName));
                            }


                            break;
                        case "0":
                            SplashForm.CloseForm();
                            XtraMessageBox.Show("Error in Application!", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                            break;
                        case "INACTIVE":
                            SplashForm.CloseForm();
                            XtraMessageBox.Show(
                                "DOCTOR ID INACTIVE, PLEASE CONTACT THE ADMINISTRATOR OF TINNITUS TRIO!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        default:
                            Application.Run(new LicenseManager());
                            break;
                    }
                }

                else
                {
                    //SplashForm.CloseForm();
                    XtraMessageBox.Show("PLEASE CONNECT TO THE INTERNET TO USE TINNITUS TRIO!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception exception)
            {
                TinnitusTrioADB_BO.TinnitusTrioLogger.SendErrorReport(exception.ToString());
                XtraMessageBox.Show(
                    "Error in Application! Support team has got the error and are working on it. The error will be resolved Shortly! Thanks for your Patience!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private static bool CheckforInternet()
        {
            try
            {
                var myPing = new Ping();
                const string host = "google.com";
                var buffer = new byte[32];
                const int timeout = 2000;
                var pingOptions = new PingOptions();
                var reply = myPing.Send(host, timeout, buffer, pingOptions);
                return reply != null && reply.Status == IPStatus.Success;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class GetLicense
    {
        public TinnitusTrioADB_BO.LicenseManager GetLicenseKey()
        {
            var licenseVal = new TinnitusTrioADB_BO.LicenseManager();

            try
            {
                var objLicensemanager = new TinnitusTrioADB_BAL.LicenseManager();

                 licenseVal = objLicensemanager.CheckActiveLicense();

                
            }
            catch (Exception exception)
            {
                TinnitusTrioADB_BO.TinnitusTrioLogger.SendErrorReport(exception.ToString());
                XtraMessageBox.Show(
                    "Error in Application! Support team has got the error and are working on it. The error will be resolved Shortly! Thanks for your Patience!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return licenseVal;
        }

        public string IsFirstLogin(string doctorid)
        {

            var licenseVal = "";
            try
            {
                var objLicensemanager = new TinnitusTrioADB_BAL.Login();

                 licenseVal = objLicensemanager.CheckFirstLogin(doctorid);

               

            }
            catch (Exception exception)
            {
                TinnitusTrioADB_BO.TinnitusTrioLogger.SendErrorReport(exception.ToString());
                XtraMessageBox.Show(
                    "Error in Application! Support team has got the error and are working on it. The error will be resolved Shortly! Thanks for your Patience!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return licenseVal;
        }
    }

    internal class SplashForm
    {
        //Delegate for cross thread call to close
        private delegate void CloseDelegate();

        //The type of form to be displayed as the splash screen.
        private static SplashScreen _splashForm;

        public static void ShowSplashScreen()
        {
            // Make sure it is only launched once.
            if (_splashForm != null)
                return;
            var thread = new Thread(new ThreadStart(SplashForm.ShowForm)) {IsBackground = true};
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private static void ShowForm()
        {
            _splashForm = new TinnitusTrioADBBridge.SplashScreen();
            Application.Run(_splashForm);
        }

        public static void CloseForm()
        {
            if (!_splashForm.IsHandleCreated) return;
            if (_splashForm != null)
            {
                _splashForm.Invoke(new CloseDelegate(SplashForm.CloseFormInternal));
            }

        }

        private static void CloseFormInternal()
        {
            _splashForm.Close();
        }
    }

    internal class WaitForm
    {
        //Delegate for cross thread call to close
        private delegate void CloseDelegate();

        //The type of form to be displayed as the splash screen.
        private static PleaseWait _splashForm;

        public static void ShowSplashScreen()
        {
            // Make sure it is only launched once.

            if (_splashForm != null)
                return;
            var thread = new Thread(new ThreadStart(WaitForm.ShowForm)) { IsBackground = true };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private static void ShowForm()
        {
            _splashForm = new TinnitusTrioADBBridge.PleaseWait();
            Application.Run(_splashForm);
        }

        public static void CloseForm()
        {
            if (!_splashForm.IsHandleCreated) return;
            if (_splashForm != null) _splashForm.Invoke(new CloseDelegate(WaitForm.CloseFormInternal));
        }

        private static void CloseFormInternal()
        {
            _splashForm.Close();
        }
    }
}   