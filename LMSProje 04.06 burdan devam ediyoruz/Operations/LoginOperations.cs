using LMSProje.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LMSProje.Operations
{
    public static class LoginOperations
    {
        public static TblUser loggerduser = null;
       public static DateTime dtLoginTime;
        public static void loginIntry(MainWindow main)
        {


            using (LMSContext context = new LMSContext())
            {
              
                var vrUserEmail = context.TblUser.FirstOrDefault(pr => pr.UserEmail == main.txtLoginEmail.Text);

                if (vrUserEmail == null)
                {

                    MessageBox.Show("Lütfen Emaliniz kontrol ediniz ");
                    return;


                }


                if (vrUserEmail.UserPw != GlobalMethods.returnUserPw(main.txtLoginPw.Password.ToString(), vrUserEmail.SaltOfPw))
                {

                    MessageBox.Show("Lütfen Şifrenizi kontrol ediniz");
                    return;

                }


                loggerduser = vrUserEmail;
               
               dtLoginTime = DateTimeHelper.ServerTime;


                GlobalMethods.ChangeLoginStatu();
                GlobalMethods.ChangeLoginStatu2();


                if (vrUserEmail.UserTypeId == 2)
                {
                    MessageBox.Show("HOŞGELDİNİZ ÖĞRETMEN");

                }
                else
                {

                    MessageBox.Show("HOŞGELDİNİZ ÖĞRENCİ");
                }

                main.txtLoginEmail.Text = "";
                main.txtLoginPw.Password = "";



                MessageBox.Show("Başarılı bir şekilde griş yaptınız");
               GlobalMethods.changeBOOKS();

            }


        }

        public static void trylogOut()
        {
             loggerduser = null;
            GlobalMethods.ChangeLoginStatu();
            GlobalMethods.ChangeLoginStatu2();
            GlobalMethods.main.tabLogin.IsSelected = true; // log outo bastığımızda tablogin seçili hale geliyor;

            MessageBox.Show("başarılı şekilde çıkış yaptınız");

        }

    }
}
