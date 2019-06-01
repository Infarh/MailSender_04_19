using System;
using System.Linq;
using MailSender.ConsoleTest.Data;

namespace MailSender.ConsoleTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //using (var db = new Data.SongsDB())
            //{
            //    foreach (var track in db.Tracks)
            //    {
            //        Console.WriteLine("{0}\t{1}\t{2}\t{3}",
            //            track.Id, track.Name, track.ArtistName, track.Length);
            //    }
            //}

            //Console.WriteLine("-------------------------------");

            //using (var db = new SongsDB())
            //{
            //    //db.Database.SqlQuery()
            //    //db.Database.Log = str => Console.WriteLine("{0}: {1}", DateTime.Now, str);

            //    foreach (var track in db.Tracks.Where(track => track.ArtistName == "Artist1"))
            //    {
            //        Console.WriteLine("{0}\t{1}\t{2}\t{3}",
            //            track.Id, track.Name, track.ArtistName, track.Length);
            //    }
            //}

            ////using (var db = new SongsDB())
            ////{
            ////    for (var i = 0; i < 1000; i++)
            ////    {
            ////        db.Tracks.Add(new Track
            ////        {
            ////            Name = $"Song-{i}",
            ////            ArtistName = "Test artist",
            ////            Length = 100 * i
            ////        });
            ////    }

            ////    db.SaveChanges();
            ////}

            //Console.WriteLine("-------------------------------");

            //using (var db = new Data.SongsDB())
            //{
            //    foreach (var track in db.Tracks)
            //    {
            //        Console.WriteLine("{0}\t{1}\t{2}\t{3}",
            //            track.Id, track.Name, track.ArtistName, track.Length);
            //    }
            //}

            var report = new GeneratedCode.GeneratedClass
            {
                DataProperty1 = "Data1",
                DataProperty2 = "Data2"
            };

            report.CreatePackage("TestReport.docx");

            //Console.ReadLine();
        }
    }
}
