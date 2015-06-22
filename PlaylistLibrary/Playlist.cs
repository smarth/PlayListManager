using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListManagerCSharp
{
    public class Playlist : IPlaylist
    {
        private Dictionary<int, Song> songsMap; //all songs added to system are stored here , deleted songs are not removed;
        private LinkedList<Song> songsOrder;
        private int currentPlayingOrdinal;
        public string playListName; // not used for demo;

        public  ICollection<Song> songsCollection
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

        public Playlist(int n)
        {
            songsOrder = new LinkedList<Song>();
            currentPlayingOrdinal = -1;
            songsMap = new Dictionary<int, Song>();
            for (int i = 1; i <= n; ++i)
            {
                Song s = new Song(i);
                songsMap[i]=s;
                songsOrder.AddLast(s);
            }
        }

        public void Print()
        {
            LinkedListNode<Song> playListHead = songsOrder.First;
            int count = 0;
            while (playListHead != null)
            {
                count++;
                String trackNumber = playListHead.Value.TrackIdentifier.ToString();
                if (count == currentPlayingOrdinal)
                {
                    trackNumber = trackNumber + "*";
                }
                Console.Write(trackNumber+" ");
                playListHead = playListHead.Next;
            }
            Console.WriteLine();

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

        public void Delete(int ordinalNumber)
        {
            if (validOrdinalNumber(ordinalNumber))
            {
                if (ordinalNumber == 1)
                {
                    if (currentPlayingOrdinal == 1) currentPlayingOrdinal = -1;
                    else if (currentPlayingOrdinal > 1) currentPlayingOrdinal--;
                    songsOrder.RemoveFirst();
                }
                else if (ordinalNumber == songsOrder.Count)
                {
                    if (currentPlayingOrdinal == songsOrder.Count) currentPlayingOrdinal = -1;
                    songsOrder.RemoveLast();
                }
                else
                {
                    LinkedListNode<Song> playListHead = songsOrder.First;
                    int count = 1;
                    while (playListHead.Next != null)
                    {
                        count++;
                        if (count == ordinalNumber)
                        {
                            if (currentPlayingOrdinal == ordinalNumber) currentPlayingOrdinal = -1;
                            else if (currentPlayingOrdinal > count) currentPlayingOrdinal--;
                            songsOrder.Remove(playListHead.Next);
                            break;
                        }
                        playListHead = playListHead.Next;

                    }
                }
            }
            else
            {
                throw new Exception("Cannot delete the track, incorrect ordinal Number");
            }
        }

        public void Insert(int trackIdentifier, int ordinalNumber)
        {
            if (ordinalNumber>0 && ordinalNumber<= (songsOrder.Count+1))
            {
                //Add songs to Dict
                if (!songsMap.ContainsKey(trackIdentifier))
                {
                    songsMap[trackIdentifier] = new Song(trackIdentifier);
                }

                if (ordinalNumber == 1)
                {
                    songsOrder.AddFirst(songsMap[trackIdentifier]);
                    if (currentPlayingOrdinal>= 1) currentPlayingOrdinal++;
                }
                else if (ordinalNumber == songsOrder.Count+1)
                {
                    songsOrder.AddLast(songsMap[trackIdentifier]);
                }
                else
                {
                    LinkedListNode<Song> playListHead = songsOrder.First;
                    int count = 1;
                    while (playListHead.Next != null)
                    {
                        count++;
                        if (count == ordinalNumber)
                        {
                            songsOrder.AddAfter(playListHead, songsMap[trackIdentifier]);
                            if (currentPlayingOrdinal >= count) currentPlayingOrdinal++;
                            break;
                        }
                        playListHead=playListHead.Next;
                    }

                }

            }
            else
            {
                throw new Exception("Cannot Insert the track, incorrect ordinal Number");
            }
        }

        public void Shuffle()
        {
            List<Song> songs  = songsOrder.ToList();
            songsOrder = new LinkedList<Song>();
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
                    else if((random+1) < currentPlayingOrdinal)
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
            
            foreach (Song s in songsOrderList)
            {
                songsOrder.AddLast(s);
            }
            currentPlayingOrdinal = oldCurrentPlayingOrdinal;
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
