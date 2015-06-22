// PlayListManagerCPP.cpp : main project file.

#include "stdafx.h"
#include <cstdint>
#include <vector>
//#include <map>
#include <unordered_map>

using namespace System;
using namespace System::Collections::Generic;

public class Song
{
public:
	int TrackIdentifier;
	std::string TrackName;
	std::string TrackFilePath;
public:
	Song(int i){
		TrackIdentifier = i;
	}
};

public class Playlist
{
public:
	std::vector<Song*>* songsOrder;
	std::unordered_map<int, Song*>* songsMap;
	int currentPlayingOrdinal;
public:
	Playlist(int n){
		currentPlayingOrdinal = -1;
		songsOrder = new std::vector<Song*>();
		songsMap = new std::unordered_map<int, Song*>();
		for (int i = 1; i <= n; ++i){
			Song* s = new Song(i);
			songsMap->insert({i,s});
			songsOrder->push_back(s);
		}
	}

	void Print(){
		for (int i = 0; i < songsOrder->size(); ++i){
			Console::Write(songsOrder->at(i)->TrackIdentifier);
			if (currentPlayingOrdinal - 1 == i)
			{
				Console::Write("*");
			}
			Console::Write(" ");
		}
		Console::WriteLine();
	}

	void Play(int ordinalNumber){
		if (validOrdinalNumber(ordinalNumber)){
			currentPlayingOrdinal = ordinalNumber;
		}
		else{
			throw gcnew Exception("Cannot play the track, incorrect ordinal Number");
		}
	}

	void Delete(int ordinalNumber){
		if (validOrdinalNumber(ordinalNumber)){
			songsOrder->erase(songsOrder->begin() + (ordinalNumber-1));
			if (ordinalNumber == currentPlayingOrdinal) currentPlayingOrdinal = -1;
			else if (ordinalNumber < currentPlayingOrdinal) currentPlayingOrdinal--;
		}
		else{
			throw gcnew Exception("Cannot Delete the track, incorrect ordinal Number");
		}
	}

	void Insert(int trackIdentifier, int ordinalNumber){
		if (ordinalNumber>0 && ordinalNumber <= (songsOrder->size() + 1))
		{
			Song *s;
			std::unordered_map<int, Song*>::iterator it;
			it = songsMap->find(trackIdentifier);
			if (it != songsMap->end()){
				s = songsMap->at(trackIdentifier);
			}
			else{
				s = new Song(trackIdentifier);
				songsMap->insert({ trackIdentifier, s });
			}
			
			if (ordinalNumber == songsOrder->size() + 1)
			{
				songsOrder->push_back(s);
				return;
			}
			if (validOrdinalNumber(ordinalNumber)){
				songsOrder->insert(songsOrder->begin() + (ordinalNumber - 1),s);
				if (ordinalNumber <= currentPlayingOrdinal)
				{
					currentPlayingOrdinal++;
				}
			}

		}
		else
		{
			throw gcnew Exception("Cannot Insert the track, incorrect ordinal Number");
		}
	}

	void Shuffle(){
		std::vector<Song*>* songs = songsOrder;
		std::vector<Song*>* songsOrderList = new std::vector<Song*>();
		int oldCurrentPlayingOrdinal = currentPlayingOrdinal;
		Random^ randomGenerator = gcnew Random();
		bool currentFound = false;
		while (songs->size() > 0)
		{
			int random = randomGenerator->Next(0, songs->size());
			Song* randomTrack = songs->at(random);
			songsOrderList->push_back(randomTrack);
			songs->erase(songs->begin() + (random));

			if (currentPlayingOrdinal >= 1 && !currentFound)
			{
				if (random + 1 == currentPlayingOrdinal)
				{
					currentFound = true;
					currentPlayingOrdinal = songsOrderList->size();
				}
				else if ((random + 1) < currentPlayingOrdinal)
				{
					currentPlayingOrdinal--;
				}
			}

		}

		if (currentPlayingOrdinal >= 1)
		{
			Song* temp = songsOrderList->at(currentPlayingOrdinal - 1);
			songsOrderList->at(currentPlayingOrdinal - 1) = songsOrderList->at(oldCurrentPlayingOrdinal - 1);
			songsOrderList->at(oldCurrentPlayingOrdinal - 1) = temp;
		}
		currentPlayingOrdinal = oldCurrentPlayingOrdinal;
		//Set To Shuffled List
		songsOrder = songsOrderList;

	}

private:
	bool validOrdinalNumber(int ordinalNumber)
	{
		if (ordinalNumber <= songsOrder->size() && ordinalNumber > 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

};

Playlist* p;
void ExecuteCommand();
int main(array<System::String ^> ^args)
{
	Console::WriteLine("Welcome To PlayList Manager");
	Console::WriteLine("Type help for list of commands");
	ExecuteCommand();
    return 0;
}

void ExecuteCommand()
{
	try
	{

		String^ command = Console::ReadLine();
		array<System::String ^>^ commandWithParameters = command->Split(' ');
		String^ commandNameHandler = commandWithParameters[0];
		String^ commandName = commandNameHandler->ToLower();
		if (commandName == "help"){
			Console::WriteLine("Use Following Commands");
			Console::WriteLine("1: Create <n>, creates a playlist with n tracks");
			Console::WriteLine("2: Play <n>, plays track at ordinal number n");
			Console::WriteLine("3: Insert <t> <x>, inserts track with identifier t at ordinal number x");
			Console::WriteLine("4: Delete <n>, deletes record at ordinal number n");
			Console::WriteLine("5: Print , prints order of playlist");
			Console::WriteLine("6: Shuffle, shuffles the playlist");
			Console::WriteLine("7: Exit , exits");
		}
		else if (commandName == "exit")
		{
			return;
		}
		else if (commandName == "create")
		{

			int N = Int32::Parse(commandWithParameters[1]);
			p = new Playlist(N);
			p->Print();
		}
		else if (commandName == "play")
		{
			int N = Int32::Parse(commandWithParameters[1]);
			p->Play(N);
			p->Print();
			
		}
		else if (commandName == "print")
		{
			p->Print();
		}
		else if (commandName == "delete")
		{
			int N = Int32::Parse(commandWithParameters[1]);
			p->Delete(N);
			p->Print();
		}
		else if (commandName == "insert")
		{
			int T = Int32::Parse(commandWithParameters[1]);
			int X = Int32::Parse(commandWithParameters[2]);
			p->Insert(T, X);
			p->Print();
		}
		else if (commandName == "shuffle")
		{
			p->Shuffle();
			p->Print();
		}
		else
		{
			Console::WriteLine("Invalid Command");
			Console::WriteLine("Type help for list of commands");
		}

		
		ExecuteCommand();
	}
	catch (Exception^ ex)
	{
		Console::ForegroundColor = ConsoleColor::Red;
		Console::WriteLine("Error : {0} ", ex->Message);
		Console::ResetColor();
		ExecuteCommand();
	}


}




