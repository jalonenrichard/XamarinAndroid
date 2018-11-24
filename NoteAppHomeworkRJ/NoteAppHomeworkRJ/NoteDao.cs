using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Environment = Android.OS.Environment;

namespace NoteAppHomeworkRJ
{
    class NoteDao
    {
        private readonly string connectionString;
        private SQLiteConnection dbConnection;

        private static readonly log4net.ILog log
            = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public NoteDao(string connectionString)
        {
            this.connectionString = connectionString;
            log.Info($"----- Connection string: {this.connectionString}");
        }

        public void ConnectToDatabase()
        {
            dbConnection = new SQLiteConnection(connectionString);
            log.Info("----- Connected to database.");
        }

        public void CreateNewNoteTable()
        {
            try
            {
                ConnectToDatabase();
                dbConnection.CreateTable<Note>();
                log.Info("----- Database created.");
            }
            catch (Exception e)
            {
                log.Info(e.Message);
            }
        }

        public TableQuery<Note> GetAllNotesFromDatabase()
        {
            try
            {
                return dbConnection.Table<Note>();
            }
            catch (NullReferenceException e)
            {
                log.Info(e.Message);
                return null;
            }
        }

        public void SaveNoteToDatabase(Note note)
        {
            try
            {
                dbConnection.Insert(note);
            }
            catch (Exception e)
            {
                log.Info(e.Message);
            }
        }

        public void SaveMultipleNotesToDatabase(List<Note> noteList)
        {
            foreach (var note in noteList)
            {
                SaveNoteToDatabase(note);
            }
        }
    }
}