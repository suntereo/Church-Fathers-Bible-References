using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchFathersParser
{
    public class Resource
    {
        public enum Categories
        {
            Unknown = 0,
            OldTestament = 1,
            NewTestament = 2,
            Apocrypha = 3
        }

        public String RegExSearch { get; set; }
        public String FullName { get; set; }
        public Categories Category { get; set; }
        public int Order { get; set; }

        //public class ChapterVerseItem
        //{
        //    public int Chapter { get; set; }
        //    public int Verses { get; set; }
        //}
        //public int TotalChapters { get; set; }
        //public List<ChapterVerseItem> ChapterVerseItems { get; set; }

        static public List<Resource> ResourceList { get; set; }
        static Resource()
        {
            ResourceList = new List<Resource>();
            ResourceList.Add(new Resource() { Order = 1, RegExSearch = "^Ge$", FullName = "Genesis", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 2, RegExSearch = "^Ex$", FullName = "Exodus", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 3, RegExSearch = "^Le$", FullName = "Leviticus", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 4, RegExSearch = "^Nu$", FullName = "Numbers", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 5, RegExSearch = "^Dt$", FullName = "Deuteronomy", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 6, RegExSearch = "^Jos$", FullName = "Joshua", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 7, RegExSearch = "^Jdg|Jug$", FullName = "Judges", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 8, RegExSearch = "^Ru$", FullName = "Ruth", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 9, RegExSearch = "^1Sa$", FullName = "1 Samuel", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 10, RegExSearch = "^2Sa$", FullName = "2 Samuel", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 11, RegExSearch = "^1Ki$", FullName = "1 Kings", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 12, RegExSearch = "^2Ki$", FullName = "2 Kings", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 13, RegExSearch = "^1Ch$", FullName = "1 Chronicles", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 14, RegExSearch = "^2Ch$", FullName = "2 Chronicles", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 15, RegExSearch = "^Ez|Ezr$", FullName = "Ezra", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 16, RegExSearch = "^Ne$", FullName = "Nehemiah", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 17, RegExSearch = "^Es$", FullName = "Esther", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 18, RegExSearch = "^Job$", FullName = "Job", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 19, RegExSearch = "^Ps$", FullName = "Psalms", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 20, RegExSearch = "^Pr$", FullName = "Proverbs", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 21, RegExSearch = "^Ec$", FullName = "Ecclesiastes", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 22, RegExSearch = "^So$", FullName = "Song of Solomon", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 23, RegExSearch = "^Is$", FullName = "Isaiah", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 24, RegExSearch = "^Je$", FullName = "Jeremiah", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 25, RegExSearch = "^La$", FullName = "Lamentations", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 26, RegExSearch = "^Eze$", FullName = "Ezekiel", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 27, RegExSearch = "^Da$", FullName = "Daniel", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 28, RegExSearch = "^Ho$", FullName = "Hosea", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 29, RegExSearch = "^Joe$", FullName = "Joel", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 30, RegExSearch = "^Am$", FullName = "Amos", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 31, RegExSearch = "^Ob$", FullName = "Obadiah", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 32, RegExSearch = "^Jon$", FullName = "Jonah", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 33, RegExSearch = "^Mic$", FullName = "Micah", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 34, RegExSearch = "^Na$", FullName = "Nahum", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 35, RegExSearch = "^Hab$", FullName = "Habakkuk", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 36, RegExSearch = "^Zep$", FullName = "Zephaniah", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 37, RegExSearch = "^Hag$", FullName = "Haggai", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 38, RegExSearch = "^Zec$", FullName = "Zechariah", Category = Categories.OldTestament });
            ResourceList.Add(new Resource() { Order = 39, RegExSearch = "^Mal$", FullName = "Malachi", Category = Categories.OldTestament });

            ResourceList.Add(new Resource() { Order = 40, RegExSearch = "^Mt$", FullName = "Matthew", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 41, RegExSearch = "^Mk$", FullName = "Mark", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 42, RegExSearch = "^Lk$", FullName = "Luke", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 43, RegExSearch = "^Jn$", FullName = "John", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 44, RegExSearch = "^Ac$", FullName = "Acts", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 45, RegExSearch = "^Ro$", FullName = "Romans", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 46, RegExSearch = "^1Co$", FullName = "1 Corinthians", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 47, RegExSearch = "^2Co$", FullName = "2 Corinthians", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 48, RegExSearch = "^Ga$", FullName = "Galatians", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 49, RegExSearch = "^Eph$", FullName = "Ephesians", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 50, RegExSearch = "^Php$", FullName = "Philippians", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 51, RegExSearch = "^Col$", FullName = "Colossians", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 52, RegExSearch = "^1Th$", FullName = "1 Thessalonians", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 53, RegExSearch = "^2Th$", FullName = "2 Thessalonians", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 54, RegExSearch = "^1Ti$", FullName = "1 Timothy", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 55, RegExSearch = "^2Ti$", FullName = "2 Timothy", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 56, RegExSearch = "^Tt$", FullName = "Titus", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 57, RegExSearch = "^Phm$", FullName = "Philemon", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 58, RegExSearch = "^Heb$", FullName = "Hebrews", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 59, RegExSearch = "^Jas$", FullName = "James", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 60, RegExSearch = "^1Pe$", FullName = "1 Peter", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 61, RegExSearch = "^2Pe$", FullName = "2 Peter", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 62, RegExSearch = "^1Jn$", FullName = "1 John", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 63, RegExSearch = "^2Jn$", FullName = "2 John", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 64, RegExSearch = "^3Jn$", FullName = "3 John", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 65, RegExSearch = "^Jd|Jud$", FullName = "Jude", Category = Categories.NewTestament });
            ResourceList.Add(new Resource() { Order = 66, RegExSearch = "^Re$", FullName = "Revelation", Category = Categories.NewTestament });

            ResourceList.Add(new Resource() { Order = 67, RegExSearch = "^1Esd$", FullName = "1 Esdras", Category = Categories.Apocrypha });
            ResourceList.Add(new Resource() { Order = 68, RegExSearch = "^2Esd$", FullName = "2 Esdras", Category = Categories.Apocrypha });
            ResourceList.Add(new Resource() { Order = 69, RegExSearch = "^Tob$", FullName = "Tobit", Category = Categories.Apocrypha });
            ResourceList.Add(new Resource() { Order = 70, RegExSearch = "^Jdt$", FullName = "Judith", Category = Categories.Apocrypha });
            ResourceList.Add(new Resource() { Order = 71, RegExSearch = "^Wis$", FullName = "Wisdom of Solomon", Category = Categories.Apocrypha });
            ResourceList.Add(new Resource() { Order = 72, RegExSearch = "^Bar$", FullName = "Baruch", Category = Categories.Apocrypha });
            ResourceList.Add(new Resource() { Order = 73, RegExSearch = "^Sir$", FullName = "Sirach", Category = Categories.Apocrypha });
            ResourceList.Add(new Resource() { Order = 74, RegExSearch = "^Sus$", FullName = "Susanna", Category = Categories.Apocrypha });
            ResourceList.Add(new Resource() { Order = 75, RegExSearch = "^1Mac$", FullName = "1 Maccabees", Category = Categories.Apocrypha });
            ResourceList.Add(new Resource() { Order = 76, RegExSearch = "^2Mac$", FullName = "2 Maccabees", Category = Categories.Apocrypha });
            ResourceList.Add(new Resource() { Order = 76, RegExSearch = "^Bel$", FullName = "Bel and the Dragon", Category = Categories.Apocrypha });
            ResourceList.Add(new Resource() { Order = 76, RegExSearch = "^Thr$", FullName = "Song of Three Youths", Category = Categories.Apocrypha });
        }
    }
}
