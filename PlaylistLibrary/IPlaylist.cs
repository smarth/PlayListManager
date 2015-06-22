using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListManagerCSharp
{
    public interface IPlaylist
    {
        ICollection<Song> songsCollection
        {
            get;
        }

        int CurrentPlayingTrackIdentifier
        {
            get;
        }
        /// <summary>
        /// This method prints the songs in playlist
        /// </summary>
        void Print();

        /// <summary>
        /// Sets current playing song to ordinalNumber, throws exception if invalid ordinalNumber
        /// </summary>
        /// <param name="ordinalNumber">1 based index of PlayList</param>
        void Play(int ordinalNumber);

        /// <summary>
        /// Inserts a track with identifier at ordinal number, throws exception if invalid ordinalNumber.
        /// Maintains the current playing track # also
        /// </summary>
        /// <param name="trackIdentifier">Identifier Of Song</param>
        /// <param name="ordinalNumber">1 based index of playlist</param>
        void Insert(int trackIdentifier, int ordinalNumber);

        /// <summary>
        /// Deletes the track at ordinalNumber
        /// </summary>
        /// <param name="ordinalNumber">1 based index of playlist</param>
        void Delete(int ordinalNumber);

        /// <summary>
        /// Shuffles the playlist , keeping current playing track at its position
        /// </summary>
        void Shuffle();
        
    }
}
