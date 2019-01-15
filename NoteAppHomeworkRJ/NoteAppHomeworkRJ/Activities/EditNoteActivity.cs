using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using NoteAppHomeworkRJ.Helper;
using NoteAppHomeworkRJ.Model;
using NoteAppHomeworkRJ.Service;

namespace NoteAppHomeworkRJ.Activities
{
    [Activity(Label = "@string/edit_note", Theme = "@style/AppTheme")]
    internal class EditNoteActivity : Activity
    {
        public static Note NoteToEdit { get; set; }

        private EditText _content;
        private EditText _header;
        private NoteService _noteService;
        private Button _saveButton;
        private Button _deleteButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.noteedit_layout);

            _header = FindViewById<EditText>(Resource.Id.editTextNoteHeaderEdit);
            _content = FindViewById<EditText>(Resource.Id.editTextNoteContentEdit);
            _saveButton = FindViewById<Button>(Resource.Id.buttonNoteSave);
            _deleteButton = FindViewById<Button>(Resource.Id.buttonNoteDelete);
            _noteService = new NoteService();

            _header.Text = NoteToEdit.Headline;
            _content.Text = NoteToEdit.Content;
            Log.Info(GetType().Name, $"----- Note header: {_header.Text}, Note Content: {_content.Text}");

            _saveButton.Click += delegate
            {
                if (NoteToEdit.Headline != _header.Text || NoteToEdit.Content != _content.Text)
                {
                    NoteToEdit.Headline = _header.Text;
                    NoteToEdit.Content = _content.Text;
                    UpdateNote();
                    Log.Info(GetType().Name, "----- Note edited in database.");
                }
                else
                    SwitchToMainActivity();
            };

            _deleteButton.Click += delegate
            {
                _noteService.RemoveNote(NoteToEdit);
                RunOnUiThread(() =>
                    Toast.MakeText(this, "Note removed from database", ToastLength.Short).Show());
                Log.Info(GetType().Name, "----- Note removed from database.");
                SwitchToMainActivity();
            };
        }

        private void UpdateNote()
        {
            NoteToEdit.CreatedDateTime = DateTime.Now;
            if (NoteChecker.NoteIsEmpty(NoteToEdit))
                RunOnUiThread(() =>
                    Toast.MakeText(this, "Header and Content are both empty", ToastLength.Short).Show());
            else
            {
                _noteService.EditNote(NoteToEdit);
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