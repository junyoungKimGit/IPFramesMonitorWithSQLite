using System;
using System.Threading;
using System.Data.SQLite;

namespace IPFramesMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine($"{currentTime} ::");

            string strConn = @"Data Source=E:\work\db\ipframes.db";

            SQLiteConnection conn = new SQLiteConnection(strConn);


            while (true)
            { 
                conn.Open();
                /* 분단위 체크 */
                String sql = "select sum(i), sum(p) from Frames WHERE TimeStamp > datetime('now', 'localtime', '-1 minutes');";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();

                DateTime currentTime = DateTime.Now;

                if (reader.Read())
                {

                    Console.WriteLine("{0} :: i frames={1}, p frames={2}", currentTime, reader["sum(i)"], reader["sum(p)"]);
                }

                reader.Close();
                cmd.Dispose();
                conn.Close();

                Thread.Sleep(1000 * 60);    // 1 minute

                //todo
                //60분마다 2시간 이전의 data를 삭제한다.
            }

        }
    }
}
