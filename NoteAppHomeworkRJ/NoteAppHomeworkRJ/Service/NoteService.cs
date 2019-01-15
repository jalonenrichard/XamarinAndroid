using System.IO;
using NoteAppHomeworkRJ.Dao;
using NoteAppHomeworkRJ.Model;
using SQLite;
using Environment = System.Environment;

namespace NoteAppHomeworkRJ.Service
{
    internal class NoteService
    {
        private readonly NoteDao _noteDao;

        public NoteService()
        {
            _noteDao = new NoteDao(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "noteDatabase.db3"));
            _noteDao.ConnectToDatabase();
        }

        internal void CreateNewNoteTable()
        {
            _noteDao.CreateNewNoteTable();
        }

        public TableQuery<Note> GetAllNotes()
        {
            return _noteDao.GetAllNotesFromDatabase();
        }

        public void SaveNote(Note note)
        {
            _noteDao.SaveNoteToDatabase(note);
        }

        public void RemoveNote(Note note)
        {
            _noteDao.RemoveNoteFromDatabase(note);
        }

        public void EditNote(Note note)
        {
            _noteDao.UpdateNoteInDatabase(note);
        }
    }
}