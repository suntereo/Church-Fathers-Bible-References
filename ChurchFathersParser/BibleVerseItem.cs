using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchFathersParser
{
    public class BibleVerseItem
    {
        public String BookName { get; set; }
        public int Chapter { get; set; }
        public int Verse { get; set; }
        public Categories Category { get; set; }

        public enum Categories
        {
            Unknown = 0,
            OldTestament = 1,
            NewTestament = 2,
            Apocrypha = 3
        }

        static public List<BibleVerseItem> AllItems { get; set; }
        static BibleVerseItem()
        {
            AllItems = System.IO.File.ReadAllLines(@"OtherData\bibletaxonomy.csv")
                .Select(l => new BibleVerseItem() { BookName = l.Split(',')[0], Chapter = int.Parse(l.Split(',')[1]), Verse = int.Parse(l.Split(',')[2]) , Category = (Categories)Enum.Parse(typeof(Categories), l.Split(',')[3]) })
                .ToList();
        }
    }
}
