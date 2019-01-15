using System;
using SQLite;

namespace NoteAppHomeworkRJ.Model
{
    public class Note
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("_id")]
        public int Id { get; set; }

        public string Headline { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}