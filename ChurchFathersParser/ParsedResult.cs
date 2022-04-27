using System;

namespace ChurchFathersParser
{
    public class ParsedResult
    {
        public String LogosBibleLinkText { get; set; }
        public String LogosBibleLink { get; set; }

        public String Father { get; set; }
        public String LogosResourceTitle { get; set; }

        public String LogosFatherLink { get; set; }
        public String FatherPageNumber { get; set; }

        public int BibleBookOrder { get; set; }
        public String BibleBook { get; set; }
        public Resource.Categories BibleBookCategory { get; set; }

        public String BibleBookAbbr { get; set; }
        public int BibleChapter { get; set; }
        public int BibleVerse { get; set; }

        public ParsedResult DeepCopy()
        {
            return (ParsedResult)MemberwiseClone();
        }
    }
}
