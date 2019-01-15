namespace NoteAppHomeworkRJ.Helper
{
    public static class ObjectTypeHelper
    {
        /// <summary>
        /// https://stackoverflow.com/questions/6594250/type-cast-from-java-lang-object-to-native-clr-type-in-monodroid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Cast<T>(this Java.Lang.Object obj) where T : class
        {
            var propertyInfo = obj.GetType().GetProperty("Instance");
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
        }
    }
}