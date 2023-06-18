using LMSProje.Models;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LMSProje.Operations
{
    public static  class AdminLogin
    {
        public static void LoginAdmin(MainWindow main)
        {
           

            using (LMSContext context = new LMSContext())
            {
                var vrUserEmail = context.TblAdmin.FirstOrDefault(pr => pr.Email == main.txtLoginEmailAdmin.Text);

                if (vrUserEmail == null)
                {
                    MessageBox.Show("Lütfen Admin Emailnizi Kontrol edniz");
                    return;

                }
                GlobalMethods.adminOperatins();

                if (vrUserEmail.Password != GlobalMethods.returnUserPw(main.txtLoginPWAdmin.Password.ToString(), vrUserEmail.SaltOfPw))
                {

                    MessageBox.Show("lütfen şifrenizi kontrol ediniz");
                    return;

                }
                MessageBox.Show("Başarılı bir şekilde griş yaptınız");

            }
           
        }




      

    }
}
