﻿using System.Collections.Generic;
using System.Globalization;
using Android.App;
using Android.Views;
using Android.Widget;
using NoteAppHomeworkRJ.Model;

namespace NoteAppHomeworkRJ.Helper
{
    internal class CustomAdapter : BaseAdapter<Note>
    {
        private readonly Activity _context;
        private readonly List<Note> _items;

        public CustomAdapter(Activity context, List<Note> items)
        {
            _items = items;
            _context = context;
        }

        public override int Count => _items?.Count ?? 0;

        public override Note this[int position] => _items[position];

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.customrow_layout, null);
            view.FindViewById<TextView>(Resource.Id.textViewNoteHeader).Text = _items[position].Headline;
            view.FindViewById<TextView>(Resource.Id.textViewNoteContent).Text = _items[position].Content;
            view.FindViewById<TextView>(Resource.Id.textViewNoteDate).Text =
                _items[position].CreatedDateTime.ToString(CultureInfo.CurrentCulture);
            return view;
        }
    }
}