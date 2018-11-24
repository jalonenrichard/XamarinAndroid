using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using Environment = System.Environment;

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
            listView.ItemClick += (sender, e) => { Toast.MakeText(this, "Vajutasid", ToastLength.Short).Show(); };

            editNotesButton.Click += delegate
            {
                var editNoteActivity = new Intent(this, typeof(NewNotesActivity));
                StartActivity(editNoteActivity);
            };
        }
    }
}