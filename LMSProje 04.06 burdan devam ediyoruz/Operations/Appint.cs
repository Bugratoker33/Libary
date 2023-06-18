using LMSProje.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LMSProje.Operations
{
    public static class Appint
    { 

        public enum WhichENUM
        {
            [Description("Sort By Books Name Ascending")]
            SortByBookNameAsc,
            [Description("Sort By Books Name Descending")]
            SortByBookNameDesc,
            [Description("Sort By Books Page Ascending")]
            SortByPageCountAsc,
            [Description("Sort By Books Page Descending")]
            SortByPageCountDesc,
            [Description("Sort By Books CategoryId Ascending")]
            SortByCategoryIdAsc,
            [Description("Sort By Books CategoryId Descending")]
            SortByCategoryIdDesc
        }

        public class sortingOption
        {
            public WhichENUM whichSort { get; set; }
            public string srDescription { get; set; }
        }
      
        private static List<sortingOption> lstSortingOptions;

        private static void initSortingOptions()
        {
            lstSortingOptions = new List<sortingOption>();

            foreach (WhichENUM sort in Enum.GetValues(typeof(WhichENUM)))
            {
                lstSortingOptions.Add(new sortingOption { srDescription = StringValueOfEnum(sort), whichSort = sort });
            }
        }
      
        static string StringValueOfEnum(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }


        public static void initApp(MainWindow main)
        {
            ObservableCollection<TblUserTypes> userRank = new ObservableCollection<TblUserTypes>();
            userRank.Add(new TblUserTypes() { UserTypeId = 0, UserTpeName = "please pick user type" });

            using (LMSContext context = new LMSContext())
            {
                var vrUsertype = context.TblUserTypes;

                foreach (var item in vrUsertype)
                {
                    userRank.Add(item);
                }


            }
            main.cmbBoxUserRank.ItemsSource = userRank;
            main.cmbBoxUserRank.DisplayMemberPath = "UserTpeName";
            main.cmbBoxUserRank.SelectedIndex = 0;
          
            initSortingOptions();

            main.cmbSortignBooks.ItemsSource = lstSortingOptions;
            main.cmbSortignBooks.DisplayMemberPath = "srDescription";
          //  main.cmbSortignBooks.SelectedIndex = 4;

        }
    }
}
