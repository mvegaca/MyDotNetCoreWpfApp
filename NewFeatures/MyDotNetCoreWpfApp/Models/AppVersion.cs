namespace MyDotNetCoreWpfApp.Models
{
    public class AppVersion
    {
        public ushort Major { get; set; }

        public ushort Minor { get; set; }

        public ushort Build { get; set; }

        public ushort Revision { get; set; }

        public override string ToString()
            => $"{Major}.{Minor}.{Build}.{Revision}";
    }
}
