using LMSProje.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static LMSProje.Operations.Appint;

namespace LMSProje.Operations
{
    public static class BookOperations
    {
        private static LMSContext bookContext = new LMSContext();
        public static void ListBook()
        {
            bookContext = new LMSContext();

          


          
            GlobalMethods.recordlistChange();

            string bookName = GlobalMethods.main.txtBookName.Text;  // textbox = search 
            string authorName = GlobalMethods.main.txtAuthorName.Text;
            string authorLastName = GlobalMethods.main.txtAuthorLastName.Text;
            string CategoryName = GlobalMethods.main.txtCategory.Text;

           

            var query = from a in bookContext.TblBooks // ilişkisel veri tabanında birçok veriyi tek bir tabelde birleştirme
                        join b in bookContext.TblAuthor on a.AuthorId equals b.AuthorId
                        join c in bookContext.TblCategory on a.CategoryId equals c.CategoryId
                        where a.BookName.Contains(bookName) && b.AuthorName.Contains(authorName) && b.AuthorLastName.Contains(authorLastName) && c.CategoryName.Contains(CategoryName)
                        select new
                        {
                            BookID = a.BookId,
                            BookName = a.BookName,
                            PageCount = a.PageCount,
                            BookStock = a.Stock,
                            AuthorName = b.AuthorName,
                            b.AuthorLastName,
                            CategoryID = c.CategoryName,
                            a.ProductionYear,
                        };
          




          

            GlobalMethods.main.dataGridBooks.ItemsSource = query.ToList();
            GlobalMethods.main.dataGridBooks.Items.Refresh();

            
           





            for (int i = 0; i < GlobalMethods.main.dataGridBooks.Columns.Count; i++) // tbl recordsun gözükmesine gerek olmadığı için bu kod satırını yazdık 
            {

              GlobalMethods.main.dataGridBooks.Columns[i].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            } 

            
            
        }

        public static void enumList()
        {
            bookContext= new LMSContext(); // her enumlist yaptığımda yeni bir context açarak datagridi günceliyorum;

            var query = from a in bookContext.TblBooks 
                       join b in bookContext.TblAuthor on a.AuthorId equals b.AuthorId
                        join c in bookContext.TblCategory on a.CategoryId equals c.CategoryId
                        

                        select new
                        {
                            BookID = a.BookId,
                            BookName = a.BookName,
                            PageCount = a.PageCount,
                            BookStock = a.Stock,
                           AuthorName = b.AuthorName,
                           b.AuthorLastName,
                            CategoryID = c.CategoryName,
                            a.ProductionYear,
                        };



            GlobalMethods.main.dataGridBooks.Items.Refresh();
                //  GlobalMethods.main.dataGridBooks.Items.Clear();

                switch ((((sortingOption)GlobalMethods.main.cmbSortignBooks.SelectedItem).whichSort))
                {
                    case WhichENUM.SortByBookNameAsc:
                    query = query.OrderBy(x => x.BookName);

                        break;
                    case WhichENUM.SortByBookNameDesc:
                    query = query.OrderByDescending(c => c.BookName);

                        break;
                    case WhichENUM.SortByPageCountAsc:
                    query = query.OrderBy(z => z.PageCount);

                        break;
                    case WhichENUM.SortByPageCountDesc:
                    query = query.OrderByDescending(tr => tr.PageCount);
                        break;
                    case WhichENUM.SortByCategoryIdAsc:
                    query = query.OrderBy(pr => pr.CategoryID);
                        break;
                    case WhichENUM.SortByCategoryIdDesc:
                    query = query.OrderByDescending(pr => pr.CategoryID);
                        break;
                    default:
                        break;


                }
            GlobalMethods.main.dataGridBooks.ItemsSource = query.ToList();
               
                GlobalMethods.main.dataGridBooks.Items.Refresh();
            
        }

      
    }

}
