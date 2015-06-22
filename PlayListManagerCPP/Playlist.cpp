#include "stdafx.h"
#include "Playlist.h"
#include "Song.h"
#include <stdint.h>

using namespace System;
using namespace System::Collections::Generic;


Playlist::Playlist(int n)
{
	songsOrder = gcnew List<Song^>();
	for (int i = 1; i <= n; ++i){
		songsOrder->Add(gcnew Song(i));
	}
}
