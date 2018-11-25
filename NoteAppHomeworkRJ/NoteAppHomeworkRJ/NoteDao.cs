using System;
using System.Collections.Generic;
using Android.Util;
using SQLite;

namespace NoteAppHomeworkRJ
{
    internal class NoteDao
    {
        private readonly string _connectionString;
        private SQLiteConnection _dbConnection;

        public NoteDao(string connectionString)
        {
            _connectionString = connectionString;
            Log.Info(GetType().Name, $"----- Connection string: {_connectionString}");
        }

        public void ConnectToDatabase()
        {
            _dbConnection = new SQLiteConnection(_connectionString);
            Log.Info(GetType().Name, "----- Connected to database.");
        }

        public void CreateNewNoteTable()
        {
            try
            {
                ConnectToDatabase();
                _dbConnection.CreateTable<Note>();
                Log.Info(GetType().Name, "----- Note database created");
            }
            catch (Exception e)
            {
                Log.Info(GetType().Name, e.Message);
            }
        }

        public TableQuery<Note> GetAllNotesFromDatabase()
        {
            try
            {
                return _dbConnection.Table<Note>();
            }
            catch (NullReferenceException e)
            {
                Log.Info(GetType().Name, e.Message);
                return null;
            }
        }

        public void SaveNoteToDatabase(Note note)
        {
            try
            {
                note.Headline = note.Headline.Trim();
                note.Content = note.Content.Trim();
                _dbConnection.Insert(note);
            }
            catch (Exception e)
            {
                Log.Info(GetType().Name, e.Message);
            }
        }

        public void RemoveNoteFromDatabase(Note note)
        {
            try
            {
                _dbConnection.Delete(note);
            }
            catch (Exception e)
            {
                Log.Info(GetType().Name, e.Message);
            }
        }

        public void UpdateNoteInDatabase(Note note)
        {
            try
            {
                note.Headline = note.Headline.Trim();
                note.Content = note.Content.Trim();
                _dbConnection.Update(note);
            }
            catch (Exception e)
            {
                Log.Info(GetType().Name, e.Message);
            }
        }
    }
}