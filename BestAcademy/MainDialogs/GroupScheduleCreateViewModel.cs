namespace BestAcademy.MainDialogs
{
    public class GroupScheduleCreateViewModel
    {
        public int GroupId { get; set; }
        public int LessonId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public string LessonName { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public string FullName { get { return $"{SubjectName} ({TeacherName})"; } }
    }
}