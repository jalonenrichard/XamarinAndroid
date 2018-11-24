using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NoteAppHomeworkRJ
{
    [Activity(Label = "NewNotesActivity")]
    class NewNotesActivity : Activity
    {
        private NoteDao noteDao;
        private EditText header;
        private EditText content;
        private Button saveButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.noteadd_layout);
            header = FindViewById<EditText>(Resource.Id.editTextNoteHeaderAdd);
            content = FindViewById<EditText>(Resource.Id.editTextNoteContentAdd);
            saveButton = FindViewById<Button>(Resource.Id.buttonNoteSave);
            noteDao = new NoteDao(Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                "noteDatabase.db3"));

            noteDao.ConnectToDatabase();
            saveButton.Click += delegate
            {
                AddNoteToDatabase();
                SwitchToMainActivity();
            };
        }

        private void AddNoteToDatabase()
        {
            var note = new Note {Headline = header.Text, Content = content.Text, CreatedDateTime = DateTime.Now};
            noteDao.SaveNoteToDatabase(note);
        }

        private void SwitchToMainActivity()
        {
            var mainActivity = new Intent(this, typeof(MainActivity));
            StartActivity(mainActivity);
        }
    }
}