using LMSProje.Models;
using LMSProje.Operations;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LMSProje.Operations
{
    public static class GlobalMethods
    {
        public static MainWindow main;
        private static DateTime _appRunDate = DateTimeHelper.ServerTime;

//        startInfoOperations() metodu, DispatcherTimer adlı bir zamanlayıcı oluşturur ve bu zamanlayıcının Tick olayına updateInfoLabel adlı bir olay işleyici(event handler) atar. Zamanlayıcı, 1 saniyelik bir süreyle ayarlanır ve başlatılır.

//updateInfoLabel metodu, zamanlayıcı her tetiklendiğinde (tick) çalışır.İlk olarak, LoginOperations.loggerduser null ise, yani kullanıcı giriş yapmamışsa, main adlı bir nesnenin lblLoginStatus adlı etiketinin içeriğini "Not Logged In. Application Run Time: <uygulama çalışma süresi> Seconds" olarak günceller. Bu süre, DateTimeHelper.ServerTime - _appRunDate ifadesiyle hesaplanır.

//Eğer LoginOperations.loggerduser null değilse, yani bir kullanıcı giriş yapmışsa, main nesnesinin lblLoginStatus adlı etiketinin içeriğini "Logged User: <kullanıcının adı ve soyadı>, Session Run Time: <oturum çalışma süresi> Seconds" olarak günceller. Kullanıcının adı ve soyadı LoginOperations.loggerduser.FirstName ve LoginOperations.loggerduser.LastName özelliklerinden alınır.Oturum çalışma süresi, DateTimeHelper.ServerTime - LoginOperations.dtLoginTime ifadesiyle hesaplanır.
//        Dispatcher.BeginInvoke() metodu, ana iş parçacığı dışında çalışan bir zamanlayıcı olayı tarafından erişilen kontrol elemanlarına (UI elemanları gibi) güvenli bir şekilde erişim sağlar.

////Bu kodlar, bir kullanıcının oturum durumunu ve oturumun ne kadar süredir devam ettiğini göstermek için bir arayüz üzerinde bir etiketi güncellemek amacıyla kullanılabilir.


        public static void startInfoOperations()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(updateInfoLabel);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private static void updateInfoLabel(object sender, EventArgs e)
        {
            if (LoginOperations.loggerduser == null)
            {
                main.lblLoginStatus.Dispatcher.BeginInvoke(() =>
                {
                    main.lblLoginStatus.Content = "Giriş Yapılmadı. Uygulama Çalışma Süresi: " + (DateTimeHelper.ServerTime - _appRunDate).TotalSeconds.ToString("N0") + " Seconds";
                });
                return;
            }

            main.lblLoginStatus.Dispatcher.BeginInvoke(() =>
            {
                main.lblLoginStatus.Content = $"Kayıtlı Kullanıcı: {LoginOperations.loggerduser.FirstName} {LoginOperations.loggerduser.LastName}, Session Run Time: " + (DateTimeHelper.ServerTime - LoginOperations.dtLoginTime).TotalSeconds.ToString("N0") + " Seconds";
            });
        }


        private static string ComputeSha256Hash(this string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string returnUserPw(string srUserPw, string srRandomString)
        {
            //no rainbow tables will be useful with salted encyrpted password
            return (srRandomString + srUserPw).ComputeSha256Hash();
        }

        public static string returnUserIp()
        {
            return "152.34.64.123";


        }
       

       //public static bool booklistopen()
       // {
       //     if (LoggingOptions.loggerduser?.UserTypeId == 1)
       //     {

       //         return true;
       //     return false;
       //     }

            

       // }

        public static void ChangeLoginStatu()
        {
            main.tabLogin.IsEnabled = !main.tabLogin.IsEnabled;
            main.tabRegister.IsEnabled = !main.tabRegister.IsEnabled;
            //main.tabBooks.IsEnabled = !main.tabBooks.IsEnabled;
           // main.tabrecordsBook.IsEnabled = !main.tabrecordsBook.IsEnabled;
          
        }
        
        public static void ChangeLoginStatu2()
        {
           

            main.tabBooks.IsEnabled = ! main.tabBooks.IsEnabled;
            main.tabrecordsBook.IsEnabled = !main.tabrecordsBook.IsEnabled;
            main.dataGridListRecords.IsEnabled =!main.dataGridListRecords.IsEnabled;

            switch (main.btnLogout.Visibility)
            {
                case System.Windows.Visibility.Visible:
                    main.btnLogout.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case System.Windows.Visibility.Hidden:
                    main.btnLogout.Visibility = System.Windows.Visibility.Visible;
                    break;
                case System.Windows.Visibility.Collapsed:
                    break;
                default:
                    break;
            }

        }

        public static void changeBOOKS()
        {
            switch (main.tabBooks.Visibility)
            {
                case System.Windows.Visibility.Visible:
                    main.tabBooks.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case System.Windows.Visibility.Hidden:
                    main.tabBooks.Visibility = System.Windows.Visibility.Visible;
                    break;
                case System.Windows.Visibility.Collapsed:
                    break;
                default:
                    break;
            }

        }
        public static void recordlistChange()
        {
            switch (main.tabrecordsBook.Visibility)
            {
                case System.Windows.Visibility.Visible:
                    main.tabrecordsBook.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case System.Windows.Visibility.Hidden:
                    main.tabrecordsBook.Visibility= System.Windows.Visibility.Visible;
                    break;
                case System.Windows.Visibility.Collapsed:
                    break;
                default:
                    break;
            }
        }
        public static void adminOperatins()
        {
            switch (main.tamAdminOparetin.Visibility)
            {
                case System.Windows.Visibility.Visible:
                    main.tamAdminOparetin.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case System.Windows.Visibility.Hidden:
                    main.tamAdminOparetin.Visibility= System.Windows.Visibility.Visible;
                    break;
                case System.Windows.Visibility.Collapsed:
                    break;
                default:
                    break;
            }
        }

      

        public static void AdminRegisterChange()
        {
            main.tabADminregister.Visibility = System.Windows.Visibility.Hidden; // kayıt yerini tabitede görünmez olarak yaptım:::::::::::::
        }

      
    }

    static public class DateTimeHelper
    {
        public static DateTime ServerTime
        {
            get { return DateTime.UtcNow; }
        }

        public static DateTime Servertime7
        {

            get { return DateTime.Today.AddDays(7); }   // gerek kalmadı adminden ayarlama yapabiliyoruz ::
            
        }
    }
    
}


//public static void setDrugsPanelVisibility()
//{
//    if (isA_DoctorLoggedIn() == false)
//    {
//        GlobalMethods.main.tabDrugs.Visibility = System.Windows.Visibility.Hidden;
//    }
//    else
//        GlobalMethods.main.tabDrugs.Visibility = System.Windows.Visibility.Visible;
//}

//public static bool isA_DoctorLoggedIn()
//{
//    if (LoginOperations.loggedUser?.UserType == 2)
//        return true;
//    return false;
//}