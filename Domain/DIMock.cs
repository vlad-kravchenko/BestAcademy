using System.Linq;

namespace Domain
{
    public static class DIMock
    {
        public static AcademyContext AcademyContext { get; private set; }

        public static void Init()
        {
            Weeks = 35;
            AcademyContext = new AcademyContext();
            var a = AcademyContext.Groups.ToList();
        }

        public static int Weeks { get; private set; }
    }
}