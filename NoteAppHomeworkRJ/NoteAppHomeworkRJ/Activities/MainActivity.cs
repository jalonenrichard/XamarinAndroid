using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using Newtonsoft.Json;
using Environment = System.Environment;
using static Android.Widget.AdapterView;

namespace NoteAppHomeworkRJ
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private List<Note> _noteList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            var noteDao = new NoteDao(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "noteDatabase.db3"));
            noteDao.CreateNewNoteTable();
            var allNotes = noteDao.GetAllNotesFromDatabase();
            try
            {
                _noteList = allNotes.ToList();
            }
            catch (NullReferenceException e)
            {
                Log.Info(GetType().Name, $"----- {e.Message}");
            }

            var listView = FindViewById<ListView>(Resource.Id.listView1);
            var editNotesButton = FindViewById<Button>(Resource.Id.buttonEditNote);

            listView.Adapter = new CustomAdapter(this, _noteList);

            listView.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                Note note = listView.GetItemAtPosition(e.Position).Cast<Note>();
                EditNoteActivity.NoteToEdit = note;
                Log.Info(GetType().Name, $"----- Note Header: {note.Headline}, Note Content: {note.Content}");
                var intent = new Intent(this, typeof(EditNoteActivity));
                //intent.PutExtra("NoteHeader", note.Headline);
                //intent.PutExtra("NoteContent", note.Content);
                StartActivity(intent);
            };

            editNotesButton.Click += delegate
            {
                var editNoteActivity = new Intent(this, typeof(NewNotesActivity));
                StartActivity(editNoteActivity);
            };
        }
    }
}