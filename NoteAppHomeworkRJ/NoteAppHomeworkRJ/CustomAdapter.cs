using System;
using System.Collections.Generic;
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
    class CustomAdapter : BaseAdapter<Note>
    {
        List<Note> items;
        Activity context;

        public CustomAdapter(Activity context, List<Note> items) : base()
        {
            this.items = items;
            this.context = context;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.customrow_layout, null);
            view.FindViewById<TextView>(Resource.Id.textViewNoteHeader).Text = items[position].Headline;
            view.FindViewById<TextView>(Resource.Id.textViewNoteContent).Text = items[position].Content;
            view.FindViewById<TextView>(Resource.Id.textViewNoteDate).Text = items[position].CreatedDateTime.ToString();
            return view;
        }

        public override int Count
        {
            get
            {
                if (items != null)
                    return items.Count;
                else
                    return 0;
            }
        }

        public override Note this[int position] => items[position];
    }
}