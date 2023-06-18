using LMSProje.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace LMSProje.Operations
{
    public static class BooksRecors
    {


        private static LMSContext bookscontext = new LMSContext();
        public static void addAProvalAdmin()
        {
            using (bookscontext = new LMSContext())
            {
                TblRecords record1 = new TblRecords();

                TblAdminApporoval apporoval = new TblAdminApporoval();


              
                
                record1 = bookscontext.TblRecords.FirstOrDefault();

                var selectedUser = GlobalMethods.main.dataGridListRecords.SelectedItem;
                var selectedItem = selectedUser;                                                         // bu üç satır hocann satırı dikat et 

                if (selectedItem != null)
                {


                  

                    if (record1.BookId != null)
                    {
                        var BookId = (int)selectedItem.GetType().GetProperty("bookId").GetValue(selectedItem, null);
                        apporoval.BookId = BookId;
                    }

                    var AcquisitionDate = (DateTime)selectedItem.GetType().GetProperty("AcquisitionDate").GetValue(selectedItem, null);
                    apporoval.AcquisitionDate = AcquisitionDate;

                    var ReturnDate = (DateTime)selectedItem.GetType().GetProperty("ReturnDate").GetValue(selectedItem, null);
                    apporoval.ReturnDate = ReturnDate;
                    
                    apporoval.UserId = LoginOperations.loggerduser.UserId;
                    apporoval.RecordId = record1.RecordId;
                    // apporoval.Id = apporoval.Id;
                    //apporoval.Id++;
                  

                    if (apporoval.RecordId == record1.RecordId)
                    {
                        bookscontext.TblAdminApporoval.Add(apporoval);
                        bookscontext.SaveChanges();
                    }
                    
                }



               
                
            }
        }

        public static void DeleteSelectedBookRecord() // seçili kıtabı iade etme 
        {
            using (bookscontext = new LMSContext())
            {
                TblRecords record1 = new TblRecords();
                 
               // TblAdminApporoval apporoval = new TblAdminApporoval();
               

                //  record1.UserId
                var vrUser = LoginOperations.loggerduser.UserId; // burda linqu sorusunda where ile ararken login yapılan kulanıcıya göre listelemesini sağladık 

                var selectedUser = GlobalMethods.main.dataGridListRecords.SelectedItem;
                var selectedItem = selectedUser;                                                         // bu üç satır hocann satırı dikat et 

                if (selectedItem != null)
                {


                    var RecordId = (int)selectedItem.GetType().GetProperty("RecordId").GetValue(selectedItem, null);
                    record1.RecordId = RecordId;
                    if (record1.BookId != null)
                    {
                        var BookId = (int)selectedItem.GetType().GetProperty("bookId").GetValue(selectedItem, null);
                        record1.BookId = BookId;
                    }

                    var AcquisitionDate = (DateTime)selectedItem.GetType().GetProperty("AcquisitionDate").GetValue(selectedItem, null);
                    record1.AcquisitionDate = AcquisitionDate;
                    var ReturnDate = (DateTime)selectedItem.GetType().GetProperty("ReturnDate").GetValue(selectedItem, null);
                    record1.ReturnDate = ReturnDate;

                    bookscontext.Remove(record1);
                }

                var quer3 = from a in bookscontext.TblRecords
                            join b in bookscontext.TblBooks on a.BookId equals b.BookId
                            join c in bookscontext.TblUser on a.UserId equals c.UserId
                            where c.UserId == vrUser
                            select new
                            {
                                a.RecordId,
                                bookId = b.BookId,
                                b.BookName,
                                b.CategoryId,
                                b.AuthorId,
                                c.UserTypeId,
                                c.UserId,
                                c.FirstName,
                                c.LastName,
                                c.UserEmail,
                                a.AcquisitionDate,
                                a.ReturnDate,
                            };

                //AdminLogin.kitapayarı();

                GlobalMethods.main.dataGridListRecords.ItemsSource = quer3.Take(100).ToList();
                GlobalMethods.main.dataGridListRecords.Items.Refresh(); // refres koyuyoruz ama yine düzemiyor : seçilen kitap silinince items refresh yapmıyor 

                for (int i = 0; i < GlobalMethods.main.dataGridListRecords.Columns.Count; i++)
                {

                    GlobalMethods.main.dataGridListRecords.Columns[i].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }

                //  GlobalMethods.main.dataGridListRecords.Items.Refresh();
                bookscontext.SaveChanges();
                //   bookscontext.


            }
        }

        public static void emantVerilenKitap() // seçilen kitabı records tabelina gönderme :
        {
            using (bookscontext = new LMSContext())
            {
                TblDateSeting retunDate = bookscontext.TblDateSeting.FirstOrDefault();

            
              

                TblRecords record = new TblRecords();

                record.UserId = LoginOperations.loggerduser.UserId;
                record.AcquisitionDate = DateTimeHelper.ServerTime;// kitap alma 
                record.ReturnDate = retunDate.ReturnDateSeting; // kitap iade tarihi admin tarafından değiştirilebilir hale getirildi TBLDATESETİN DEN :
                var id = GlobalMethods.main.dataGridBooks;// id ile kitapl listesini alıyoruz 

                if (id.SelectedItem != null) // seçilen ıtems boş değil ise :
                {
                    var selectedRow = id.ItemContainerGenerator.ContainerFromItem(id.SelectedItem) as DataGridRow; //seçili ögeyi elde etmeye çalışıyruz ıtemscontanerve container (seçili ıtms datagridrow ile selected rowa gönderiyoruz )
                    if (selectedRow != null)
                    {
                        var dataItem = selectedRow.Item;
                        var firstColumnProperty = dataItem.GetType().GetProperty("BookID"); // dateıtem ile seçili bookıd özeliini alırız ve 
                        record.BookId = (int)firstColumnProperty.GetValue(dataItem, null);
                       
                        bookscontext.TblRecords.Add(record);
                        
                       

                    }

                }

                if (id.SelectedItem != null)
                {
                    var selectedRow = id.ItemContainerGenerator.ContainerFromItem(id.SelectedItem) as DataGridRow;
                    if (selectedRow != null)
                    {
                        var dataItem = selectedRow.Item;
                        var firstColumnProperty = dataItem.GetType().GetProperty("BookID");
                        int bookId = (int)firstColumnProperty.GetValue(dataItem, null);

                        TblBooks book = bookscontext.TblBooks.FirstOrDefault(b => b.BookId == bookId);
                        if (book != null)
                        {
                            // Stok sayısını azalt
                           
                            if (book.Stock > 0)
                            {
                                book.Stock--;
                                MessageBox.Show("başarılı birşekilde kitabı teslim aldınız ");
                            }
                            else if(book.Stock <= 0)
                            {
                                MessageBox.Show("Stoklarda yok lütfen yeni bir kitap seçin");
                            }

                            bookscontext.SaveChanges();
                        }
                    }
                }
                bookscontext.SaveChanges();




                
               

                

            }
            //AdminLogin.kitapayarı();
        }



        public static void ListOfRecordBooks() // bookson me butonuna basarak görmeyi sağlıyor //üzeimdeki kitapları görme
        {
            //  string UserName = GlobalMethods.main.txtBooksOnMe.Text;// burda isme göre arama yapıyorduk ama bunu ıserıd göre listeliyoruz 
            var vrUser = LoginOperations.loggerduser.UserId; // login olarak girilen user ıd seine göre llisteme yapacağız :;:;:;
                                                             // list recorda textbox var o vrUsere göre kistelemden önce ki olan 


            bookscontext = new LMSContext();

            var quer2 = from a in bookscontext.TblRecords
                        join b in bookscontext.TblBooks on a.BookId equals b.BookId
                        join c in bookscontext.TblUser on a.UserId equals c.UserId


                        where c.UserId == vrUser

                        select new
                        {
                            a.RecordId,
                            bookId = b.BookId,
                            b.BookName,
                            b.CategoryId,
                            b.AuthorId,
                            c.UserTypeId,
                            c.UserId,
                            c.FirstName,
                            c.LastName,
                            c.UserEmail,
                            a.AcquisitionDate,
                            a.ReturnDate,
                        };

            //AdminLogin.kitapayarı();

            GlobalMethods.main.dataGridListRecords.ItemsSource = quer2.Take(100).ToList();


            for (int i = 0; i < GlobalMethods.main.dataGridListRecords.Columns.Count; i++)
            {

                GlobalMethods.main.dataGridListRecords.Columns[i].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
            //   GlobalMethods.main.dataGridListRecords.Items.Refresh();
        }



    }



}

