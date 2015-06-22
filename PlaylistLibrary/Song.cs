using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListManagerCSharp
{
    public class Song
    {
        public int TrackIdentifier; // a unique ID for track
        public string TrackName; // not needed for demo
        public string filePath; // not needed for demo purpose

        public Song(int trackIdentifier)
        {
            this.TrackIdentifier = trackIdentifier;
            this.TrackName = Convert.ToString(trackIdentifier); // setting track name same as number for demo
        }

        /// <summary>
        /// No need to override for this demo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {

            Song s = obj as Song;
            return (this.TrackIdentifier == s.TrackIdentifier);
        }

        public override int GetHashCode()
        {
            return this.TrackIdentifier.GetHashCode();
        }
    }
}
