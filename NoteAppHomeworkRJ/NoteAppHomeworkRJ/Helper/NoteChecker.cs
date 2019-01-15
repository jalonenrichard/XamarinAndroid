using NoteAppHomeworkRJ.Model;

namespace NoteAppHomeworkRJ.Helper
{
    public static class NoteChecker
    {
        public static bool NoteIsEmpty(Note note)
        {
            return note.Headline.Trim() == string.Empty && note.Content.Trim() == string.Empty;
        }
    }
}