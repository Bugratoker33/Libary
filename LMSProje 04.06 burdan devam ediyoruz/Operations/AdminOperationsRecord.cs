using LMSProje.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LMSProje.Operations
{
    public static class AdminOperationsRecord
    {
        public static void ListApproval() // admin onaylamadan iade edilmiş sayılmıyor 
        {
            using (LMSContext contex9 = new LMSContext())
            { TblAdminApporoval appproval = new TblAdminApporoval();
                TblAdmin admin = contex9.TblAdmin.FirstOrDefault();

                var selectedUser = GlobalMethods.main.dataGridRecordsListBookAdmin.SelectedItem;
                var selectedItem = selectedUser;                                                        

                if (selectedItem != null)
                {


                    var RecordId = (int)selectedItem.GetType().GetProperty("RecordId").GetValue(selectedItem, null);
                    appproval.RecordId = RecordId;
                    if (appproval.BookId != null)
                    {
                        var BookId = (int)selectedItem.GetType().GetProperty("BookId").GetValue(selectedItem, null);
                        appproval.BookId = BookId;
                    }
                    var ID = (int)selectedItem.GetType().GetProperty("Id").GetValue(selectedItem, null);
                    appproval.Id = ID;

                    var AcquisitionDate = (DateTime)selectedItem.GetType().GetProperty("AcquisitionDate").GetValue(selectedItem, null);
                    appproval.AcquisitionDate = AcquisitionDate;
                    var ReturnDate = (DateTime)selectedItem.GetType().GetProperty("ReturnDate").GetValue(selectedItem, null);
                    appproval.ReturnDate = ReturnDate;

                    contex9.Remove(appproval);
                    MessageBox.Show($"{admin.FirstName}  {admin.LastName} Tarafından Başarılı şekilde teslim alındı");
                }
                var query9 = from a in contex9.TblAdminApporoval
                             join b in contex9.TblRecords on a.RecordId equals b.RecordId
                             join c in contex9.TblBooks on a.BookId equals c.BookId
                             join d in contex9.TblUser on a.UserId equals d.UserId
                             select new
                             {
                                 a.Id,d.UserTypeId,
                                 a.ReturnDate,a.AcquisitionDate,
                                 d.UserId, d.FirstName, d.LastName, d.UserEmail,
                                 c.BookId, c.BookName, c.Category.CategoryName, c.Author.AuthorName, c.Author.AuthorLastName,
                                 a.RecordId,



                             };
                
                GlobalMethods.main.dataGridRecordsListBookAdmin.ItemsSource = query9.Take(100).ToList();
                for (int i = 0; i < GlobalMethods.main.dataGridRecordsListBookAdmin.Columns.Count; i++)
                {

                    GlobalMethods.main.dataGridRecordsListBookAdmin.Columns[i].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }
                GlobalMethods.main.dataGridRecordsListBookAdmin.Items.Refresh();

                contex9.SaveChanges();

            }


        }
        public static void Denenme()
        {


            using (LMSContext context7 = new LMSContext()) // kitap alma süresi geçenlere mesaj gönderme
            {
               
                
                var user = context7.TblRecords.Where(pr => pr.UserId == LoginOperations.loggerduser.UserId);
              

                foreach (var recor3 in user)
                {
                   

                    var second = DateTime.UtcNow.Date - recor3.ReturnDate.Date;

                    if (LoginOperations.loggerduser.UserTypeId == 2)
                    {
                        //   MessageBox.Show("HOŞGELDİNİZ ÖĞRETMEN");
                    }
                    else if (second.TotalDays >= 0)
                    {
                        MessageBox.Show($"BookId: {recor3.BookId.ToString()}  iade süresi gelmiştir. En kısa zamanda iade ediniz.");
                    }
                }
            }
        }
        public static void changeReturnDate() // kitap alma süresini ayarlama 
        {
            using (LMSContext context8= new LMSContext())
            {
                TblDateSeting dateSeting = new TblDateSeting();
                string textboxReturDateValue = GlobalMethods.main.txtReturDateSeting.Text;

                DateTime tarih = DateTime.ParseExact(textboxReturDateValue, "yyyy-MM-dd", CultureInfo.InvariantCulture); // textboxa girilen string veriyi datetime dönüştürme 

                dateSeting.ReturnDateSeting = tarih;
                dateSeting.Id = 1;
                context8.TblDateSeting.Update(dateSeting);
                context8.SaveChanges();
               
                MessageBox.Show("başarılı şekilde tarihi eklediniz ");



            }


        }
        public static void limitSeting() // adminin ayarlar panelinden limit verisin değiştirme uygulandı 
        {
            using (LMSContext context7  = new LMSContext()) //   parse etme gpt4 den aldım 
            {
               TblSeting setimg= new TblSeting();

                string textboxValue = GlobalMethods.main.txtLimitBook.Text; 
              
                    int integerValue = int.Parse(textboxValue);  // strig verisini parse yöntemi ile int çevirdik :
                    // Dönüştürülen integer değerini kullanma
               
                setimg.BookLimits = integerValue;
                setimg.Id = 1;
                context7.TblSeting.Update(setimg);
                context7.SaveChanges();

                MessageBox.Show("Başarılı bir şekilde Değişikliği yaptınız");


               
            }
            
        }
        public static void LimistOfBooks() // öğrenci 5ten fazla alamaz 
        {
            using (LMSContext context1 = new LMSContext())
            {


                TblSeting limit = context1.TblSeting.FirstOrDefault();
                if (limit != null)
                {
                    var resul = context1.TblRecords.Where(pr => pr.UserId == LoginOperations.loggerduser.UserId).Count();
                    if (resul >= limit.BookLimits && LoginOperations.loggerduser.UserTypeId == 1)
                    {
                        MessageBox.Show("daha fazla kitap alamazsınız");
                        //   GlobalMethods.main.dataGridBooks.Visibility = Visibility.Hidden; kople datagridbooksu görünmez yaptı
                        GlobalMethods.main.dataGridBooks.IsEnabled = false; //etkinliğini kapatık 
                    }
                }

                
            }
        }
        public static void StudentsOrTeacher() // burada onay bekliyenleri görüyoruz // ve günceliyoruz 
        {

            using (LMSContext context5 = new LMSContext())
            {
               

             //   var selectedUser = GlobalMethods.main.dataGridRecordsListBookAdmin.SelectedItem;
               // var selectedItem = selectedUser;                                                         // bu üç satır hocann satırı dikat et 
                //int userId = (int)selectedItem.GetType().GetProperty("UserId").GetValue(selectedItem, null);
               
              //  var usersToUpdate = context5.TblUser.Where(pr => pr.UserTypeId == 1);

              
                
                    var selectedUser = GlobalMethods.main.dataGridRecordsListBookAdmin.SelectedItem;
                    var selectedItem = selectedUser;


                    if (selectedItem != null)
                    {
                     TblUser user = new TblUser();       
                    
                     int userId = (int)selectedItem.GetType().GetProperty("UserId").GetValue(selectedItem, null);
                     user.UserId = userId;
                    string email = (string)selectedItem.GetType().GetProperty("UserEmail").GetValue(selectedItem, null);//
                    user.UserEmail = email;
                    string registerIp = (string)selectedItem.GetType().GetProperty("RegisterIp").GetValue(selectedItem, null);//
                    user.RegisterIp = registerIp;
                    var RegisterDate = (DateTime)selectedItem.GetType().GetProperty("RegisterDate").GetValue(selectedItem, null);//
                    user.RegisterDate = RegisterDate;
                    string FirstName = (string)selectedItem.GetType().GetProperty("FirstName").GetValue(selectedItem, null);//
                    user.FirstName = FirstName;
                    string LastName = (string)selectedItem.GetType().GetProperty("LastName").GetValue(selectedItem, null);//
                    user.LastName = LastName;
                    string SaltOfPw = (string)selectedItem.GetType().GetProperty("SaltOfPw").GetValue(selectedItem, null);
                    user.SaltOfPw = SaltOfPw;
                    string UserPw = (string)selectedItem.GetType().GetProperty("UserPw").GetValue(selectedItem, null);//
                    user.UserPw = UserPw;

                    // string SaltOfPw = (string)selectedItem.GetType().GetProperty("SaltOfPw").GetValue(selectedItem, null);




                    if (user.UserId != null)
                       {
                        user.UserTypeId = 2;
                        user.RankId = 2;
                       }
                    context5.TblUser.Update(user);
                    }
                var query6 = from a in context5.TblUser // tbl usere kayıt olan dategride gösterme 
                             join b in context5.TblUserTypes on a.UserTypeId equals b.UserTypeId                         
                             

                             where a.RankId == 2 && a.UserTypeId == 1

                             select new
                             {   
                                 a.UserId,//                               
                                 UserTypeId=b.UserTypeId,
                                 a.FirstName,//
                                 a.LastName,//
                                 a.UserEmail,//
                                 a.RankId,
                                 a.RegisterDate,//
                                 a.UserPw,//
                                 a.RegisterIp,//
                                 a.SaltOfPw,//
                                 
                               


                             };

                GlobalMethods.main.dataGridRecordsListBookAdmin.ItemsSource = query6.Take(100).ToList();
                for (int i = 0; i < GlobalMethods.main.dataGridRecordsListBookAdmin.Columns.Count; i++)
                {

                    GlobalMethods.main.dataGridRecordsListBookAdmin.Columns[i].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }
                context5.SaveChanges();



            }
        }

        

        public static void ListRecordsAdmin() // emanet verilen kitaplar listesi
        {

            using(LMSContext context2= new LMSContext())
            {

               

                var quer3 = from a in context2.TblRecords // books reccorda tblrecorda kaydolan verileri admin panelinde gösterme yapıyoruz 
                            join b in context2.TblBooks on a.BookId equals b.BookId
                            join c in context2.TblUser on a.UserId equals c.UserId
                            select new
                            {
                                a.RecordId,
                                bookId = b.BookId,
                                c.UserId,
                                c.UserTypeId,
                              //  c.RankId,
                                c.FirstName,
                                c.LastName,
                                c.UserEmail,
                             

                                b.BookName,
                                b.PageCount,
                                b.Stock,
                                b.Category.CategoryName,
                                b.Author.AuthorName,b.Author.AuthorLastName,
                               
                              

                                a.AcquisitionDate,
                                a.ReturnDate,
                                
                                
                               
                            
                                
                            };

                GlobalMethods.main.dataGridRecordsListBookAdmin.ItemsSource = quer3.Take(150).ToList();
                for (int i = 0; i < GlobalMethods.main.dataGridRecordsListBookAdmin.Columns.Count; i++)
                {

                    GlobalMethods.main.dataGridRecordsListBookAdmin.Columns[i].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }

            }

        }

        public static void AllBooks() // tüm kitaplar listesi  // tblboksdaki tüm kitapları burada göstermeye çalışıyoruz 
        {
            using (LMSContext context3 = new LMSContext())
            {
                var query4 = from a in context3.TblBooks
                             join b in context3.TblAuthor on a.AuthorId equals b.AuthorId
                             join c in context3.TblCategory on a.CategoryId equals c.CategoryId

                             select new
                             {
                                 a.BookId,
                                 a.BookName,
                                 a.PageCount,
                                 a.Stock,
                                 b.AuthorName,
                                 b.AuthorLastName,
                                 c.CategoryName,
                                 a.ProductionYear,
                             };
                GlobalMethods.main.dataGridRecordsListBookAdmin.ItemsSource = query4.ToList();
                for (int i = 0; i < GlobalMethods.main.dataGridRecordsListBookAdmin.Columns.Count; i++)
                {

                    GlobalMethods.main.dataGridRecordsListBookAdmin.Columns[i].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }
            }
        }

    }
}
