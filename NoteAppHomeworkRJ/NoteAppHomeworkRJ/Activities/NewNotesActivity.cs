using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Environment = System.Environment;

namespace NoteAppHomeworkRJ
{
    [Activity(Label = "@string/new_note", Theme = "@style/AppTheme")]
    internal class NewNotesActivity : Activity
    {
        private EditText _content;
        private EditText _header;
        private NoteDao _noteDao;
        private Button _saveButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.noteadd_layout);

            _header = FindViewById<EditText>(Resource.Id.editTextNoteHeaderAdd);
            _content = FindViewById<EditText>(Resource.Id.editTextNoteContentAdd);
            _saveButton = FindViewById<Button>(Resource.Id.buttonNoteSave);

            _noteDao = new NoteDao(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "noteDatabase.db3"));
            _noteDao.ConnectToDatabase();

            _saveButton.Click += delegate { AddNoteToDatabase(); };
        }

        private void AddNoteToDatabase()
        {
            var note = new Note {Headline = _header.Text, Content = _content.Text, CreatedDateTime = DateTime.Now};
            if (NoteChecker.NoteIsEmpty(note))
                RunOnUiThread(() =>
                    Toast.MakeText(this, "Header and Content are both empty", ToastLength.Short).Show());
            else
            {
                _noteDao.SaveNoteToDatabase(note);
                SwitchToMainActivity();
            }
        }

        private void SwitchToMainActivity()
        {
            var mainActivity = new Intent(this, typeof(MainActivity));
            StartActivity(mainActivity);
        }
    }
}