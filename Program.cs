using System;
using System.Windows.Forms;
using HoaQuaNoiGian.Forms;
using HoaQuaNoiGian.Managers;

namespace HoaQuaNoiGian
{
    internal static class Program
    {
        /// <summary>
        /// Điểm khởi chạy chính của ứng dụng Hoa Quả Nổi Giận.
        /// Bẫy lỗi toàn ứng dụng để ngăn crash ứng dụng đột ngột 
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Bẫy lỗi toàn cục ứng dụng
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
            {
                Logger.LogError("Lỗi không lường trước ở giao diện WinForms UI", e.Exception);
                MessageBox.Show($"Đã xảy ra lỗi không lường trước:\n{e.Exception.Message}\n\nLỗi đã được ghi lại tại Data/error.log", "Cảnh báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    Logger.LogError("Lỗi hệ thống UnhandledException", ex);
                }
            };

            ApplicationConfiguration.Initialize();
            Application.Run(new frmSplash());
        }
    }
}