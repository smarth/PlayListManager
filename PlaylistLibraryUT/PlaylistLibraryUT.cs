using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayListManagerCSharp;
using System.Collections;
using System.Collections.Generic;

namespace PlaylistLibraryUT
{
    [TestClass]
    public class PlaylistLibraryUT
    {
        [TestMethod]
        public void CreateTest()
        {
            IPlaylist pl = new PlaylistVector(5); //12345
            Assert.AreEqual(pl.songsCollection.Count, 5);
            IEnumerator<Song> enumerator = pl.songsCollection.GetEnumerator();
            List<int> expectedOrder = new List<int>(){1,2,3,4,5};
            int i = 0;
            while (enumerator.MoveNext() && enumerator.Current.TrackIdentifier == expectedOrder[i++])
            {
                
            }
            Assert.AreEqual(i, 5);
            Assert.AreEqual(pl.CurrentPlayingTrackIdentifier, -1);
        }

        [TestMethod]
        public void PlayTest()
        {
            IPlaylist pl = new PlaylistVector(5); //12345
            pl.Play(2);
            Assert.AreEqual(pl.CurrentPlayingTrackIdentifier, 2);
        }

        [TestMethod]
        public void InsertTest()
        {
            IPlaylist pl = new PlaylistVector(5);
            pl.Play(2);
            pl.Insert(6, 2);
            IEnumerator<Song> enumerator = pl.songsCollection.GetEnumerator();
            List<int> expectedOrder = new List<int>() { 1, 6, 2, 3, 4, 5 };
            int i = 0;
            while (enumerator.MoveNext() && enumerator.Current.TrackIdentifier == expectedOrder[i++])
            {

            }
            Assert.AreEqual(i, 6);
            Assert.AreEqual(pl.CurrentPlayingTrackIdentifier, 3);
        }

        [TestMethod]
        public void DeleteTest()
        {
            IPlaylist pl = new PlaylistVector(5);
            pl.Play(2);
            pl.Delete(2);
            IEnumerator<Song> enumerator = pl.songsCollection.GetEnumerator();
            List<int> expectedOrder = new List<int>() { 1, 3, 4, 5 };
            int i = 0;
            while (enumerator.MoveNext() && enumerator.Current.TrackIdentifier == expectedOrder[i++])
            {

            }
            Assert.AreEqual(i, 4);
            Assert.AreEqual(pl.CurrentPlayingTrackIdentifier, -1);
        }

        [TestMethod]
        public void DeleteTestPlayCheck()
        {
            IPlaylist pl = new PlaylistVector(5);
            pl.Play(2);
            pl.Delete(1);
            IEnumerator<Song> enumerator = pl.songsCollection.GetEnumerator();
            List<int> expectedOrder = new List<int>() { 2, 3, 4, 5 };
            int i = 0;
            while (enumerator.MoveNext() && enumerator.Current.TrackIdentifier == expectedOrder[i++])
            {

            }
            Assert.AreEqual(i, 4);
            Assert.AreEqual(pl.CurrentPlayingTrackIdentifier, 1);
        }

        /// <summary>
        /// Mainly checks that current track retains its position, 
        /// testing for shuffle can be probability based
        /// </summary>
        [TestMethod]
        public void ShuffleCheck()
        {
            IPlaylist pl = new PlaylistVector(5);
            pl.Play(2);
            Assert.AreEqual(pl.CurrentPlayingTrackIdentifier, 2);
            pl.Shuffle();
            IEnumerator<Song> enumerator = pl.songsCollection.GetEnumerator();
            int i = 1;
            while (i <= 2)
            {
                enumerator.MoveNext();
                i++;
            }
            Assert.AreEqual(enumerator.Current.TrackIdentifier, 2);

        }
        [TestMethod]
        public void CreateTestLL()
        {
            IPlaylist pl = new Playlist(5); //12345
            Assert.AreEqual(pl.songsCollection.Count, 5);
            IEnumerator<Song> enumerator = pl.songsCollection.GetEnumerator();
            List<int> expectedOrder = new List<int>() { 1, 2, 3, 4, 5 };
            int i = 0;
            while (enumerator.MoveNext() && enumerator.Current.TrackIdentifier == expectedOrder[i++])
            {

            }
            Assert.AreEqual(i, 5);
            Assert.AreEqual(pl.CurrentPlayingTrackIdentifier, -1);
        }

        [TestMethod]
        public void PlayTestLL()
        {
            IPlaylist pl = new Playlist(5); //12345
            pl.Play(2);
            Assert.AreEqual(pl.CurrentPlayingTrackIdentifier, 2);
        }

        [TestMethod]
        public void InsertTestLL()
        {
            IPlaylist pl = new Playlist(5);
            pl.Play(2);
            pl.Insert(6, 2);
            IEnumerator<Song> enumerator = pl.songsCollection.GetEnumerator();
            List<int> expectedOrder = new List<int>() { 1, 6, 2, 3, 4, 5 };
            int i = 0;
            while (enumerator.MoveNext() && enumerator.Current.TrackIdentifier == expectedOrder[i++])
            {

            }
            Assert.AreEqual(i, 6);
            Assert.AreEqual(pl.CurrentPlayingTrackIdentifier, 3);
        }

        [TestMethod]
        public void DeleteTestLL()
        {
            IPlaylist pl = new Playlist(5);
            pl.Play(2);
            pl.Delete(2);
            IEnumerator<Song> enumerator = pl.songsCollection.GetEnumerator();
            List<int> expectedOrder = new List<int>() { 1, 3, 4, 5 };
            int i = 0;
            while (enumerator.MoveNext() && enumerator.Current.TrackIdentifier == expectedOrder[i++])
            {

            }
            Assert.AreEqual(i, 4);
            Assert.AreEqual(pl.CurrentPlayingTrackIdentifier, -1);
        }

        [TestMethod]
        public void DeleteTestPlayCheckLL()
        {
            IPlaylist pl = new Playlist(5);
            pl.Play(2);
            pl.Delete(1);
            IEnumerator<Song> enumerator = pl.songsCollection.GetEnumerator();
            List<int> expectedOrder = new List<int>() { 2, 3, 4, 5 };
            int i = 0;
            while (enumerator.MoveNext() && enumerator.Current.TrackIdentifier == expectedOrder[i++])
            {

            }
            Assert.AreEqual(i, 4);
            Assert.AreEqual(pl.CurrentPlayingTrackIdentifier, 1);
        }

        /// <summary>
        /// Mainly checks that current track retains its position, 
        /// testing for shuffle can be probability based
        /// </summary>
        [TestMethod]
        public void ShuffleCheckLL()
        {
            IPlaylist pl = new Playlist(5);
            pl.Play(2);
            Assert.AreEqual(pl.CurrentPlayingTrackIdentifier, 2);
            pl.Shuffle();
            IEnumerator<Song> enumerator = pl.songsCollection.GetEnumerator();
            int i = 1;
            while (i <= 2)
            {
                enumerator.MoveNext();
                i++;
            }
            Assert.AreEqual(enumerator.Current.TrackIdentifier, 2);

        }

    }
}
