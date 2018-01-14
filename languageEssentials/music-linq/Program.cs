using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            var mtVernon = Artists.Where(artist => artist.Hometown == "Mount Vernon").Single();
            System.Console.WriteLine(mtVernon.ArtistName);

            var mtVern = from Artist in Artists
                         where Artist.Hometown == "Mount Vernon"
                         select Artist.ArtistName[0];
            System.Console.WriteLine(mtVern);
            //Who is the youngest artist in our collection of artists?
            var youngest = Artists.OrderBy(artist => artist.Age).First();
            System.Console.WriteLine(youngest.ArtistName);

            //Display all artists with 'William' somewhere in their real name
            var william = Artists.Where(artist => artist.RealName.Contains("William"));
            System.Console.WriteLine("Artist's whose real name contains William:");
            foreach (var artist in william)
            {
                System.Console.WriteLine("  " + artist.ArtistName);
            }
            // Display all groups with names less than 8 characters in length.
            var groupsLessThanEight = Groups.Where(group => group.GroupName.Length < 8);
            System.Console.WriteLine("Groups whose names have less than 8 characters:");
            foreach (var group in groupsLessThanEight)
            {
                System.Console.WriteLine("  " + group.GroupName);
            }
            //Display the 3 oldest artists from Atlanta
            var threeOldest = Artists.Where(artist => artist.Hometown == "Atlanta").OrderByDescending(artist => artist.Age).Take(3);
            System.Console.WriteLine("The 3 oldest artists from Atlanta are: ");
            foreach (var artist in threeOldest)
            {
                System.Console.WriteLine("  " + artist.ArtistName + " - " + artist.Age);
            }
            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            var notNewYork = Groups.Join(Artists, group => group.Id, artist => artist.GroupId, (group, artist) =>
            {
                if (artist.Hometown != "New York City")
                {
                    return group.GroupName;
                }
                else
                {
                    return null;
                }
            }).Distinct();
            foreach (var group in notNewYork)
            {
                System.Console.WriteLine(group);
            }
            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            var WuTang = Groups.Join(Artists, group => group.Id, artist => artist.GroupId, (group, artist) =>
            {
                if (group.GroupName == "Wu-Tang Clan")
                {
                    return artist.ArtistName;
                }
                else
                {
                    return null;
                }
            }).Distinct();
            foreach (var artist in WuTang)
            {
                System.Console.WriteLine(artist);
            }
        }
    }
}