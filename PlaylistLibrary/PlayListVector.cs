using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListManagerCSharp
{
    public class PlaylistVector : IPlaylist
    {
        private Dictionary<int, Song> songsMap; //all songs added to system are stored here , deleted songs are not removed;
        private List<Song> songsOrder;
        private int currentPlayingOrdinal;
        public string playListName; // not used for demo;

        public ICollection<Song> songsCollection
        {
            get
            {
                return songsOrder;
            }
        }

        public int CurrentPlayingTrackIdentifier
        {
            get
            {
                return currentPlayingOrdinal;
            }
        }
        public PlaylistVector(int n)
        {
            songsOrder = new List<Song>();
            currentPlayingOrdinal = -1;
            songsMap = new Dictionary<int, Song>();
            for (int i = 1; i <= n; ++i)
            {
                Song s = new Song(i);
                songsMap[i] = s;
                songsOrder.Add(s);
            }
        }

        public void Play(int ordinalNumber)
        {
            if (validOrdinalNumber(ordinalNumber))
            {
                currentPlayingOrdinal = ordinalNumber;
            }
            else
            {
                throw new Exception("Cannot play the track, incorrect ordinal Number");
            }
        }

        public void Print()
        {
            for (int i = 0; i < songsOrder.Count; ++i)
            {
                Console.Write(songsOrder[i].TrackIdentifier);
                if (currentPlayingOrdinal - 1 == i)
                {
                    Console.Write("*");
                }
                Console.Write(" ");
            }
            Console.WriteLine();
        }

        public void Delete(int ordinalNumber)
        {
            if (validOrdinalNumber(ordinalNumber))
            {
                songsOrder.RemoveAt(ordinalNumber - 1);
                if (ordinalNumber == currentPlayingOrdinal) currentPlayingOrdinal = -1;
                else if (ordinalNumber < currentPlayingOrdinal) currentPlayingOrdinal--;
            }
            else
            {
                throw new Exception("Cannot delete the track, incorrect ordinal Number");
            }
        }

        public void Insert(int trackIdentifier, int ordinalNumber)
        {
            if (!songsMap.ContainsKey(trackIdentifier))
            {
                songsMap[trackIdentifier] = new Song(trackIdentifier);
            }
            if (ordinalNumber == songsOrder.Count + 1)
            {
                songsOrder.Add(songsMap[trackIdentifier]);
                return;
            }
            if (validOrdinalNumber(ordinalNumber)){
                songsOrder.Insert(ordinalNumber-1, songsMap[trackIdentifier]);
                if (ordinalNumber <= currentPlayingOrdinal)
                {
                    currentPlayingOrdinal++;
                }
            }
            else
            {
                throw new Exception("Cannot Insert the track, incorrect ordinal Number");
            }
        }

        public void Shuffle()
        {
            List<Song> songs = songsOrder;
            List<Song> songsOrderList = new List<Song>();
            int oldCurrentPlayingOrdinal = currentPlayingOrdinal;
            Random randomGenerator = new Random();
            bool currentFound = false;
            while (songs.Count > 0)
            {
                int random = randomGenerator.Next(0, songs.Count);
                Song randomTrack = songs[random];
                songsOrderList.Add(randomTrack);
                songs.RemoveAt(random);

                if (currentPlayingOrdinal>=1 && !currentFound)
                {
                    if (random + 1 == currentPlayingOrdinal)
                    {
                        currentFound = true;
                        currentPlayingOrdinal = songsOrderList.Count;
                    }
                    else if ((random+1) < currentPlayingOrdinal)
                    {
                        currentPlayingOrdinal--;
                    }
                }

            }

            if (currentPlayingOrdinal >= 1)
            {
                Song temp = songsOrderList[currentPlayingOrdinal - 1];
                songsOrderList[currentPlayingOrdinal - 1] = songsOrderList[oldCurrentPlayingOrdinal - 1];
                songsOrderList[oldCurrentPlayingOrdinal - 1] = temp;
            }
            currentPlayingOrdinal = oldCurrentPlayingOrdinal;
            //Set To Shuffled List
            songsOrder = songsOrderList;
        }


        private bool validOrdinalNumber(int ordinalNumber)
        {
            if (ordinalNumber <= songsOrder.Count && ordinalNumber > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
