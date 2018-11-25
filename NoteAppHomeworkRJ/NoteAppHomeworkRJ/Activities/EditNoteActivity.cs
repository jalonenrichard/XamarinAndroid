using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace NoteAppHomeworkRJ
{
    [Activity(Label = "EditNotesActivity")]
    class EditNoteActivity : Activity
    {
        public static Note NoteToEdit { get; set; }

        private EditText _content;
        private EditText _header;
        private NoteDao _noteDao;
        private Button _saveButton;

        private Button _deleteButton;
        //private Note note;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.noteedit_layout);
            _header = FindViewById<EditText>(Resource.Id.editTextNoteHeaderEdit);
            _content = FindViewById<EditText>(Resource.Id.editTextNoteContentEdit);
            _saveButton = FindViewById<Button>(Resource.Id.buttonNoteSave);
            _deleteButton = FindViewById<Button>(Resource.Id.buttonNoteDelete);
            _noteDao = new NoteDao(Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                "noteDatabase.db3"));

            _header.Text = NoteToEdit.Headline;
            _content.Text = NoteToEdit.Content;

            Log.Info(GetType().Name, $"----- Note header: {_header.Text}, Note Content: {_content.Text}");
            _noteDao.ConnectToDatabase();
            _saveButton.Click += delegate
            {
                if (NoteToEdit.Headline != _header.Text || NoteToEdit.Content != _content.Text)
                {
                    NoteToEdit.Headline = _header.Text;
                    NoteToEdit.Content = _content.Text;
                    UpdateNoteInDatabase();
                    Log.Info(GetType().Name, $"----- Note edited in database.");
                }

                SwitchToMainActivity();
            };
            //_deleteButton.Click += delegate
            //{
            //    Log.Info(GetType().Name, $"----- {note.Headline}, {note.Content}, {note.CreatedDateTime}");
            //};
        }

        private void UpdateNoteInDatabase()
        {
            NoteToEdit.CreatedDateTime = DateTime.Now;
            if (!NoteChecker.NoteIsEmpty(NoteToEdit))
                _noteDao.UpdateNoteInDatabase(NoteToEdit);
        }

        private void SwitchToMainActivity()
        {
            var mainActivity = new Intent(this, typeof(MainActivity));
            StartActivity(mainActivity);
        }
    }
}