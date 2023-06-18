using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LMSProje.Operations;

namespace LMSProje
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            GlobalMethods.main = this;
            Appint.initApp(this);
            GlobalMethods.startInfoOperations();
            GlobalMethods.ChangeLoginStatu2();
            GlobalMethods.changeBOOKS();
           GlobalMethods.recordlistChange();
            GlobalMethods.AdminRegisterChange();
            GlobalMethods.adminOperatins();




        }

       

        private void btnRegister_Click(object sender, RoutedEventArgs e) // kulanucı kayıt yeri 
        {
            RegisterOperations.completeRegister(this);
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)// kulanıcı giriş yeri
        {
            LoginOperations.loginIntry(this);
          
            //AdminOperationsRecord.Denenme();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)// log out butonu 
        {
            LoginOperations.trylogOut();
        }

        private void btnRefreshBook_Click(object sender, RoutedEventArgs e)// burası listeleme yeri  kitap listeleme 
        {
            BookOperations.ListBook();
            AdminOperationsRecord.LimistOfBooks();  // kitap listesinine ekledik çünkü daha fazla kitap alamaayacağını göstermek için 
            AdminOperationsRecord.Denenme();// KİTAP ALMAK İÇİN HEM REFREH BUTONUN BASTIĞINDA HEMDE RECORDS ADD BASTIĞINDA UYARI ALACAK : 

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) // burası txt aram yeri 
        {
            BookOperations.ListBook();
        }

        private void btnRecordsAdd_Click(object sender, RoutedEventArgs e)// record verisine kaydetme //
        {
            BooksRecors.emantVerilenKitap();
            AdminOperationsRecord.LimistOfBooks(); // daha fazla kitap alamaz eğer öğrenci ise (5)
            AdminOperationsRecord.Denenme();//tarih süresi 
        }

        private void btnRecordsList_Click(object sender, RoutedEventArgs e) //recorda kaydedilen verileri görme 
        {
           BooksRecors.ListOfRecordBooks();
        }

        //private void txtBooksOnMe_TextChanged(object sender, TextChangedEventArgs e)// recorda kaydedilen verileri arama 
        //{
        //    BooksRecors.ListOfRecordBooks();// buna gerek kalmadı çünkü userın grişine göre listeleniyor 
        //}

        private void btnAdminRehister_Click(object sender, RoutedEventArgs e)// admin kayıt yeri 
        {
            adminRegister.registerOperations(this);
        }

        private void btnAdminLogin_Click(object sender, RoutedEventArgs e) // admin kayıt griş yeri 
        {
            AdminLogin.LoginAdmin(this);
        }

       
        private void btnRecordListBookAdmin_Click(object sender, RoutedEventArgs e) // admin kaydedilen kitapları görme ::
        {
            AdminOperationsRecord.ListRecordsAdmin();
        }

        private void btnListAllBooks_Click(object sender, RoutedEventArgs e) // admin tüm kitapları görme
        {
            AdminOperationsRecord.AllBooks();
        }

        private void btnApproval_Click(object sender, RoutedEventArgs e) // öğretmen onayı admin //
        {
            AdminOperationsRecord.StudentsOrTeacher();
        }

       private void btnUpdateTeacher_Click(object sender, RoutedEventArgs e)
        {
          //  AdminOperationsRecord.AprovalTeacher();
        }

        private void btnRunBackTo_Click(object sender, RoutedEventArgs e)
        {
            BooksRecors.DeleteSelectedBookRecord(); // seçili kitabı silme kulanıcıda kayıtlı olan kitabı silme
            BooksRecors.addAProvalAdmin();
        }

        private void btnLimits_Click(object sender, RoutedEventArgs e)
        {

            //bunu aç sonra
             AdminOperationsRecord.limitSeting(); // kitap limiti admin tarafından değiltirilebili hale getirildi :
           
        }

        private void btnReturnDateSeting_Click(object sender, RoutedEventArgs e)
        {
            AdminOperationsRecord.changeReturnDate();// admin tarafından returnDate tarihi değiştirilebilir hale getirildi 
        }

        private void cmbSortignBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookOperations.enumList();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminOperationsRecord.ListApproval();
        }
    }
}
