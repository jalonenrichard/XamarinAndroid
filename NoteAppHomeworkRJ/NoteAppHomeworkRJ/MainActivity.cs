using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Environment = Android.OS.Environment;

namespace NoteAppHomeworkRJ
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        List<Note> noteList;

        private static readonly log4net.ILog log
            = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            NoteDao noteDao = new NoteDao(Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                "noteDatabase.db3"));
            noteDao.CreateNewNoteTable();
            var allNotes = noteDao.GetAllNotesFromDatabase();
            try
            {
                noteList = allNotes.ToList();
            }
            catch (NullReferenceException e)
            {
                log.Info(e.Message);
            }

            var listView = FindViewById<ListView>(Resource.Id.listView1);
            var editNotesButton = FindViewById<Button>(Resource.Id.buttonEditNote);

            listView.Adapter = new CustomAdapter(this, noteList);
            listView.ItemClick += (sender, e) => { Toast.MakeText(this, $"Vajutasid", ToastLength.Short).Show(); };

            editNotesButton.Click += delegate
            {
                var editNoteActivity = new Intent(this, typeof(NewNotesActivity));
                StartActivity(editNoteActivity);
            };
        }
    }
}