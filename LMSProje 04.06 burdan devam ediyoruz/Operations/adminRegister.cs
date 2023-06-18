using LMSProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LMSProje.Operations
{
    public static class adminRegister
    {
        public static void registerOperations(MainWindow main)
        {
            GlobalMethods.AdminRegisterChange(); /// admin register tab ıtem panelini görünmez yaptık ;


            using (LMSContext context = new LMSContext())
            {

                TblAdmin admin = new TblAdmin();


                admin.FirstName = main.txtFirstNameadmin.Text;
                admin.LastName = main.txtLastNameadmin.Text;
                admin.Email = main.txtEmailAdmin.Text;

                Guid obj = Guid.NewGuid();
                admin.SaltOfPw = obj.ToString();

                admin.Password = GlobalMethods.returnUserPw(GlobalMethods.main.txtPW1aDMİN.Password.ToString(), admin.SaltOfPw);

                try
                {
                    context.TblAdmin.Add(admin);
                    context.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("kAYIT OLMAD");
                    return;
                }
            }
            MessageBox.Show("BAŞARILI ŞEKİLDE KAYIT OLDU ");
        }
    }
}
