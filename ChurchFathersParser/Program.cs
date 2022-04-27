using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChurchFathersParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(Properties.Settings.Default.WorkingFolder); // set in app.config to point to folder containing LogosExport, OtherData and where Results folder is created.

            #region data check
            //var names = BibleVerseItem.AllItems.Select(p => p.BookName).Distinct();
            //foreach (var name in names)
            //{
            //    if (!Resource.ResourceList.Any(p => p.FullName == name))
            //    {
            //        Console.WriteLine("Resource.ResourceList & BibleVerseItem.AllItems mismatch for: {0}", name);
            //        throw new Exception("Handle!");
            //    }
            //}
            #endregion

            List<String> fileNames = new List<String>();
            fileNames.AddRange(Directory.EnumerateFiles(@"LogosExport\Ante-Nicene", "*.html", SearchOption.TopDirectoryOnly).ToList());
            Scan(fileNames, "Ante-Nicene Fathers");

            fileNames = new List<String>();
            fileNames.AddRange(Directory.EnumerateFiles(@"LogosExport\Nicene Fathers", "*.html", SearchOption.TopDirectoryOnly).ToList());
            Scan(fileNames, "Nicene Fathers");

            fileNames = new List<String>();
            fileNames.AddRange(Directory.EnumerateFiles(@"LogosExport\Ante-Nicene", "*.html", SearchOption.TopDirectoryOnly).ToList());
            fileNames.AddRange(Directory.EnumerateFiles(@"LogosExport\Nicene Fathers", "*.html", SearchOption.TopDirectoryOnly).ToList());
            Scan(fileNames, "Ante-Nicene + Nicene Fathers");

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        static public void Scan(List<String> fileNames, String writeToFolder)
        {
            List<ParsedResult> results = new List<ParsedResult>();

            foreach (String fileName in fileNames)
            {
                Console.WriteLine(fileName);

                String friendlyName = Regex.Match(Path.GetFileName(fileName), @"(.*?)(.html)").Groups[1].Value;
                if (fileName.Substring(2, 0) == "-")
                    friendlyName = Regex.Match(Path.GetFileName(fileName), @"([0-9]{0,3}-)(.*?)(.html)").Groups[2].Value;

                String file = System.IO.File.ReadAllText(fileName);

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(file);

                List<HtmlNode> paragraphs = htmlDoc.DocumentNode.SelectNodes("//p").ToList();
                foreach (var paragraph in paragraphs)
                {
                    if (paragraph.InnerHtml.Contains(@"/logosref/Bible"))
                    {
                        var bibleNodes = paragraph.SelectNodes(".//a").Where(p => p.GetAttributeValue("href", String.Empty).Contains(@"/logosref/Bible")).ToList();
                        var fatherNodes = paragraph.SelectNodes(".//a").Where(p => p.GetAttributeValue("href", String.Empty).Contains(@"/logosres/")).ToList();

                        foreach (var bibleNode in bibleNodes)
                        {
                            foreach (var fatherNode in fatherNodes)
                            {
                                int? endVerse = null;

                                ParsedResult result = new ParsedResult();
                                result.Father = friendlyName;

                                result.LogosBibleLinkText = "\"" + bibleNode.InnerText.Replace("&ndash;", "-") + "\"";
                                result.LogosBibleLink = bibleNode.GetAttributeValue("href", string.Empty);

                                String truncatedBibleRef = result.LogosBibleLink.Substring(30); // remove https://ref.ly/logosref/Bible.

                                String strEndVerse = Regex.Match(truncatedBibleRef, @"([0-9]{0,1}[A-Z]{1,1}[a-z]{1,2})(\d{1,3}).(\d{1,3})-(\d{1,3})").Groups[4].Value; // this first one checks for end verse after "-"
                                if (!String.IsNullOrEmpty(strEndVerse))
                                    endVerse = int.Parse(strEndVerse);

                                var parsed = Regex.Match(truncatedBibleRef, @"([0-9]{0,1}[A-Z]{1,1}[a-z]{1,2})(\d{1,3}).(\d{1,3})"); // this will always work (with/without end verse)

                                result.BibleBookAbbr = parsed.Groups[1].Value;
                                if (!String.IsNullOrEmpty(parsed.Groups[2].Value)) { result.BibleChapter = int.Parse(parsed.Groups[2].Value); }
                                if (!String.IsNullOrEmpty(parsed.Groups[3].Value)) { result.BibleVerse = int.Parse(parsed.Groups[3].Value); }

                                if (String.IsNullOrEmpty(result.BibleBookAbbr))
                                {
                                    var parsed2 = Regex.Match(truncatedBibleRef, @"([0-9]{0,1}[A-Z]{1,1}[a-z]{1,2})(\d{1,3})"); // some don't have a verse (e.g., Ge27 for Gen 27:1)

                                    result.BibleBookAbbr = parsed2.Groups[1].Value;
                                    if (!String.IsNullOrEmpty(parsed2.Groups[2].Value)) { result.BibleChapter = int.Parse(parsed2.Groups[2].Value); }
                                    if (!String.IsNullOrEmpty(parsed2.Groups[3].Value)) { result.BibleVerse = int.Parse(parsed2.Groups[3].Value); }

                                    if (!String.IsNullOrEmpty(result.BibleBookAbbr))
                                    {
                                        if (result.BibleChapter == 0)
                                            result.BibleChapter = 1;

                                        if (result.BibleVerse == 0)
                                            result.BibleVerse = 1;
                                    }
                                }

                                if (String.IsNullOrEmpty(result.BibleBookAbbr))
                                {
                                    Console.WriteLine("Need to handle: {0}", truncatedBibleRef);
                                }

                                var foundBook = Resource.ResourceList.FirstOrDefault(p => Regex.IsMatch(result.BibleBookAbbr, p.RegExSearch));
                                if (foundBook == null)
                                {
                                    Console.WriteLine("Unknown book: {0}", result.BibleBookAbbr);

                                    result.BibleBookCategory = Resource.Categories.Unknown;
                                    result.BibleBook = "** UNKNOWN **";
                                    result.BibleBookOrder = 9999;
                                }
                                else
                                {
                                    result.BibleBookCategory = foundBook.Category;
                                    result.BibleBook = foundBook.FullName;
                                    result.BibleBookOrder = foundBook.Order;
                                }

                                result.FatherPageNumber = "\"" + fatherNode.InnerText.Replace("&ndash;", "-") + "\"";

                                result.LogosFatherLink = fatherNode.GetAttributeValue("href", string.Empty);

                                result.LogosResourceTitle = Regex.Match(result.LogosFatherLink, @"(https:\/\/ref.ly\/logosres\/)(.*?)(\?ref=Page)").Groups[2].Value;

                                results.Add(result);

                                if (endVerse != null)
                                {
                                    int startVerse = result.BibleVerse;
                                    for (int i = startVerse + 1; i <= endVerse.Value; i++)
                                    {
                                        var copy = result.DeepCopy();
                                        copy.BibleVerse = i;
                                        results.Add(copy);
                                    }
                                }
                            }
                        }
                    }
                }

            }

            if (!Directory.Exists("Results"))
                Directory.CreateDirectory("Results");

            writeToFolder = "Results\\" + writeToFolder + "\\";

            if (!Directory.Exists(writeToFolder))
                Directory.CreateDirectory(writeToFolder);

            #region report

            List<String> report = new List<String>();

            List<BibleVerseItem> missingNTVerses = null;
            {
                var bibleItems = results
                    .Where(p => p.BibleBookCategory == Resource.Categories.NewTestament).ToList();

                int totalNTVerses = BibleVerseItem.AllItems.Where(p => p.Category == BibleVerseItem.Categories.NewTestament).Count();
                missingNTVerses = BibleVerseItem.AllItems.Where(p => p.Category == BibleVerseItem.Categories.NewTestament && !bibleItems.Any(s => s.BibleBook == p.BookName && s.BibleChapter == p.Chapter && s.BibleVerse == p.Verse)).ToList();

                int missingNTVersesCount = missingNTVerses.Count();
                int coveredNTVerses = totalNTVerses - missingNTVersesCount;
                double missingNTPct = 100 * (double)missingNTVersesCount / (double)totalNTVerses;
                double coveredNTPct = 100 * (double)coveredNTVerses / (double)totalNTVerses;

                report.Add("**** " + writeToFolder.ToUpper() + " ****");
                report.Add("");
                report.Add("NT Results ------------------------");

                report.Add(String.Format("Total # NT verses: {0}", totalNTVerses));
                report.Add("");

                report.Add(String.Format("Total # times any NT verse linked in footnotes: {0}", results.Where(p => p.BibleBookCategory == Resource.Categories.NewTestament).Count()));
                report.Add("");

                report.Add(String.Format("Total # unique NT verses linked in footnotes: {0}", coveredNTVerses));
                report.Add(String.Format("Total % NT verses linked: {0:N2}%", coveredNTPct));
                report.Add("");

                report.Add(String.Format("Total # unique NT verses missing from footnotes: {0}", missingNTVersesCount));
                report.Add(String.Format("Total % NT verses missing: {0:N2}%", missingNTPct));

            }

            List<BibleVerseItem> missingOTVerses = null;
            {
                var bibleItems = results
                    .Where(p => p.BibleBookCategory == Resource.Categories.OldTestament).ToList();

                int totalOTVerses = BibleVerseItem.AllItems.Where(p => p.Category == BibleVerseItem.Categories.OldTestament).Count();
                missingOTVerses = BibleVerseItem.AllItems.Where(p => p.Category == BibleVerseItem.Categories.OldTestament && !bibleItems.Any(s => s.BibleBook == p.BookName && s.BibleChapter == p.Chapter && s.BibleVerse == p.Verse)).ToList();

                int missingOTVersesCount = missingOTVerses.Count();
                int coveredOTVerses = totalOTVerses - missingOTVersesCount;

                double missingOTPct = 100 * (double)missingOTVersesCount / (double)totalOTVerses;
                double coveredOTPct = 100 * (double)coveredOTVerses / (double)totalOTVerses;

                report.Add("");
                report.Add("OT Results ------------------------");

                report.Add(String.Format("Total # OT verses: {0}", totalOTVerses));
                report.Add("");

                report.Add(String.Format("Total # times any OT verse linked in footnotes: {0}", results.Where(p => p.BibleBookCategory == Resource.Categories.OldTestament).Count()));
                report.Add("");

                report.Add(String.Format("Total # unique OT verses linked in footnotes: {0}", coveredOTVerses));
                report.Add(String.Format("Total % OT verses linked: {0:N2}%", coveredOTPct));
                report.Add("");

                report.Add(String.Format("Total # unique OT verses missing from footnotes: {0}", missingOTVersesCount));
                report.Add(String.Format("Total % OT verses missing: {0:N2}%", missingOTPct));
            }

            String strReport = String.Join("\n", report);
            Console.WriteLine(strReport);
            File.WriteAllText(writeToFolder + "_Report.txt", strReport);

            #endregion

            Console.WriteLine("Writing files...");

            using (StreamWriter writer = File.CreateText(writeToFolder + "All.csv"))
                CsvSerializer.Serialize<ParsedResult>(writer, results.OrderBy(p => p.BibleBookOrder).ThenBy(p => p.BibleChapter).ThenBy(p => p.BibleVerse));

            using (StreamWriter writer = File.CreateText(writeToFolder + "New Testament - Found.csv"))
                CsvSerializer.Serialize<ParsedResult>(writer, results.Where(p => p.BibleBookCategory == Resource.Categories.NewTestament).OrderBy(p => p.BibleBookOrder).ThenBy(p => p.BibleChapter).ThenBy(p => p.BibleVerse));

            using (StreamWriter writer = File.CreateText(writeToFolder + "New Testament - Missing.csv"))
                CsvSerializer.Serialize<BibleVerseItem>(writer, missingNTVerses);

            using (StreamWriter writer = File.CreateText(writeToFolder + "Old Testament - Found.csv"))
                CsvSerializer.Serialize<ParsedResult>(writer, results.Where(p => p.BibleBookCategory == Resource.Categories.OldTestament).OrderBy(p => p.BibleBookOrder).ThenBy(p => p.BibleChapter).ThenBy(p => p.BibleVerse));

            using (StreamWriter writer = File.CreateText(writeToFolder + "Old Testament - Missing.csv"))
                CsvSerializer.Serialize<BibleVerseItem>(writer, missingOTVerses);

            using (StreamWriter writer = File.CreateText(writeToFolder + "Apocrypha - Found.csv"))
                CsvSerializer.Serialize<ParsedResult>(writer, results.Where(p => p.BibleBookCategory == Resource.Categories.Apocrypha).OrderBy(p => p.BibleBookOrder).ThenBy(p => p.BibleChapter).ThenBy(p => p.BibleVerse));
        }
    }
}
