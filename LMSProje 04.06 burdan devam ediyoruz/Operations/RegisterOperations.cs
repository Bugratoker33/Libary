using LMSProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LMSProje.Operations
{
    public static class RegisterOperations
    {
        public static void completeRegister(MainWindow main)
        {
            using (LMSContext context = new LMSContext())
            {
                TblUser myUser = new TblUser();

                myUser.FirstName = main.txtFirstName.Text;
                if (main.txtFirstName.Text.Length < 1)
                {
                    MessageBox.Show("Lütfen iSMİNİZİ GRİNİZ");
                    return;
                }

                myUser.LastName = main.txtLastName.Text;

                if(main.txtLastName.Text.Length < 1)
                {
                    MessageBox.Show("lÜTFEN SOY ADINIZI GRİNİZ");
                    return;
                }

                myUser.UserEmail = main.txtEmail.Text;
                if (main.txtEmail.Text.Length < 1)
                {
                    MessageBox.Show("Lütefen Emailinizi Griniz ");
                    return;

                }


                myUser.RegisterIp = GlobalMethods.returnUserIp();

                myUser.UserTypeId = (main.cmbBoxUserRank.SelectedItem as TblUserTypes).UserTypeId;
                
                if (main.cmbBoxUserRank.SelectedIndex < 1)
                {
                    MessageBox.Show("lütfen userRank seçiniz ");
                    return;

                }

                Guid obj = Guid.NewGuid();
                myUser.SaltOfPw = obj.ToString();

                myUser.UserPw = GlobalMethods.returnUserPw(main.txtPw1.Password.ToString(), myUser.SaltOfPw);

                if (myUser.UserTypeId == 1) //öğrenci
                {
                    myUser.RankId = 2;// öğretmen 
                }
                else 
                {
                    myUser.RankId = 1;
                    
                }

                if (main.txtPw1.Password.ToString() != main.txtPw2.Password.ToString())
                {
                    MessageBox.Show("lütfen şifrenizi kontrol ediniz");
                    return;
                }

                if (main.txtPw1.Password.Length < 1)
                {
                    MessageBox.Show("şire giriniz");
                    return;

                }
              //  AdminLogin.kitapayarı();

                myUser.RegisterDate = DateTimeHelper.ServerTime;

                try
                {
                    context.TblUser.Add(myUser);
                    context.SaveChanges();


                }
                catch
                {

                    MessageBox.Show("An error has occured while registering. Error is");
                    return;
                }
                MessageBox.Show("User has been succcesfully registered");
                main.txtEmail.Text = "";
                main.txtFirstName.Text = "";
                main.txtLastName.Text = "";





            }

        }
    }
}
