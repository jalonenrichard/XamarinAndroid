using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using NoteAppHomeworkRJ.Service;
using NoteAppHomeworkRJ.Helper;
using NoteAppHomeworkRJ.Model;

namespace NoteAppHomeworkRJ.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        private List<Note> _noteList;
        private NoteService _noteService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var listView = FindViewById<ListView>(Resource.Id.listView1);
            var newNoteButton = FindViewById<Button>(Resource.Id.buttonNewNote);

            _noteService = new NoteService();
            _noteService.CreateNewNoteTable();
            var allNotes = _noteService.GetAllNotes();
            try
            {
                _noteList = allNotes.ToList();
            }
            catch (NullReferenceException e)
            {
                Log.Info(GetType().Name, $"----- {e.Message}");
            }

            listView.Adapter = new CustomAdapter(this, _noteList);

            listView.ItemClick += (sender, e) =>
            {
                Note note = listView.GetItemAtPosition(e.Position).Cast<Note>();
                EditNoteActivity.NoteToEdit = note;
                Log.Info(GetType().Name, $"----- Note Header: {note.Headline}, Note Content: {note.Content}");
                var intent = new Intent(this, typeof(EditNoteActivity));
                StartActivity(intent);
            };

            newNoteButton.Click += delegate
            {
                var editNoteActivity = new Intent(this, typeof(NewNotesActivity));
                StartActivity(editNoteActivity);
            };
        }
    }
}