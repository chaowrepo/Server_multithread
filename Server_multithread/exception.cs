using System;
using System.IO;
using System.Text;

namespace Server_multithread {
    public class exception {
        public string DateTime;
        public string UserName;
        public string OperatingSystem;
        public string Title;
        public string StackTrace;

        public exception(byte[] data) {
            MemoryStream ms = new MemoryStream(data);
            BinaryReader br = new BinaryReader(ms);

            Int16 DateTimeLength = br.ReadByte();
            Int16 UserNameLength = br.ReadByte();
            Int16 OperatingSystemLength = br.ReadByte();
            Int16 TitleLength = br.ReadByte();
            Int16 StackTraceLength = br.ReadByte();

            DateTime = Encoding.UTF8.GetString(br.ReadBytes(DateTimeLength));
            UserName = Encoding.UTF8.GetString(br.ReadBytes(UserNameLength));
            OperatingSystem = Encoding.UTF8.GetString(br.ReadBytes(OperatingSystemLength));
            Title = Encoding.UTF8.GetString(br.ReadBytes(TitleLength));
            StackTrace = Encoding.UTF8.GetString(br.ReadBytes(StackTraceLength));
        }
    }
}