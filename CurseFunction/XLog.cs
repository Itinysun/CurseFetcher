using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CurseFunction
{
    public class XLog
    {
        private string folderPath;
        private int buffSize;
        private BufferContiner[] continer;
        private int continerSize;
        private int contineIndex;
        private TaskFactory tf;
        /// <summary>
        /// log level lower then this will be droped
        /// </summary>
        public AsyncLogLevel outputLevel;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathForFileSave">a parent path,path will be appended a  \logs dir end</param>
        /// <param name="logsNumForEachFile">define how many logs in which file,suggested num is around how many logs create pre second</param>
        /// <param name="logsContinerNum">nums of logs write buffer</param>
        public XLog(string pathForFileSave = null, int logsNumForEachFile = 100, int logsContinerNum = 0)
        {
            folderPath = pathForFileSave ?? @"\logs";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            buffSize = logsNumForEachFile;
            tf = new TaskFactory();
            continerSize = logsContinerNum == 0 ? Convert.ToInt32(Math.Ceiling((double)(10000 / logsNumForEachFile))) : logsContinerNum;
            continer = new BufferContiner[++continerSize];
            contineIndex = 0;
            continer[0] = new BufferContiner(buffSize);
            outputLevel = AsyncLogLevel.ALL;
        }
        /// <summary>
        /// add a log,and log can be auto saved based on logsNumForEachFile num
        /// </summary>
        /// <param name="message">log content</param>
        /// <param name="title">title</param>
        /// <param name="level">level</param>
        /// <param name="obj">extend filed</param>
        public void Add(string message, string title = "", AsyncLogLevel level = AsyncLogLevel.INFO, object obj = null)
        {
            try
            {
                if ((int)outputLevel > (int)level)
                    return;
                bool isfull = false;
                lock (continer)
                {
                    isfull = continer[contineIndex].TryAdd(new AsyncLogEventArgs(message, title, level, obj));
                }
                if (isfull)
                {
                    Dump();
                }
            }
            catch
            {

            }

        }
        /// <summary>
        /// flush current log continer buffer
        /// </summary>
        public void Dump()
        {
            string logstring = null;
            lock (continer[contineIndex])
            {
                logstring = continer[contineIndex].ToString();
                contineIndex = (contineIndex == continerSize - 1) ? 0 : contineIndex + 1;
                continer[contineIndex] = new BufferContiner(buffSize);
            }
            if(!string.IsNullOrEmpty(logstring))
                Dump(logstring,contineIndex.ToString());
        }
        private void Dump(string logs,string id)
        {
            if (logs != null)
            {
                tf.StartNew(new Action(() =>
                {
                    Random ran = new Random();
                    int RandKey = ran.Next(1000, 9999);
                    string logFileName = Path.Combine(folderPath, string.Format("{0}-{1}-{2}.log", DateTime.Now.ToString("yyyyMMdd_HHmmss_ffffff"),RandKey,id));
                    using (FileStream fs = new FileStream(logFileName, FileMode.OpenOrCreate))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                        {
                            sw.Write(logs);
                        }
                    }
                }));
            }
        }

        private class BufferContiner
        {
            int index;
            int size;
            AsyncLogEventArgs[] buffer;
            public BufferContiner(int _size)
            {
                size = _size;
                Clear();
            }
            void Clear()
            {
                index = 0;
                buffer = new AsyncLogEventArgs[size];
            }
            public bool TryAdd(AsyncLogEventArgs e)
            {
                buffer[index++] = e;
                if (index == size)
                    return true;
                return false;
            }
            public override string ToString()
            {
                if (index == 0)
                    return null;
                StringBuilder sb = new StringBuilder(index);
                for (int i = 0; i < index; i++)
                {
                    sb.AppendFormat("<{0}> {1}{2}", i, buffer[i], Environment.NewLine);
                }
                return sb.ToString();
            }
        }
        public class AsyncLogEventArgs : EventArgs
        {
            public AsyncLogLevel level;
            public string message;
            public string title;
            public DateTime time;
            public object tagObj;
            public AsyncLogEventArgs(string msg, string t = "", AsyncLogLevel lv = AsyncLogLevel.INFO, object obj = null)
            {
                message = msg;
                time = DateTime.Now;
                title = t;
                level = lv;
                tagObj = obj;
            }
            public override string ToString()
            {
                return string.Format("[{0}] [{1}] [{2}] {3}", time, level, title, message);
            }
        }
        public enum AsyncLogLevel
        {
            ALL = 0,
            DEBUG = 1,
            INFO = 2,
            ERROR = 3,
            WARN = 4,
            FATAL = 5
        }
        public delegate void asyncLogHandler(object sender, AsyncLogEventArgs e);
    }
}
