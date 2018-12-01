namespace AutoUpdater
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Cache;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;

    using global::AutoUpdater.Properties;

    internal partial class DownloadUpdateDialog : Form
    {
        private readonly string _downloadURL;

        private DateTime _startedAt;

        private string _tempFile;

        private MyWebClient _webClient;

        public DownloadUpdateDialog(string downloadURL)
        {
            InitializeComponent();

            this._downloadURL = downloadURL;
        }

        private static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            var bytes = Math.Abs(byteCount);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return $"{(Math.Sign(byteCount) * num).ToString(CultureInfo.InvariantCulture)} {suf[place]}";
        }

        private static bool CompareChecksum(string fileName, string checksum)
        {
            using (var hashAlgorithm = HashAlgorithm.Create(AutoUpdater.HashingAlgorithm))
            {
                using (var stream = File.OpenRead(fileName))
                {
                    if (hashAlgorithm != null)
                    {
                        var hash = hashAlgorithm.ComputeHash(stream);
                        var fileChecksum = BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant();

                        if (fileChecksum == checksum.ToLower()) return true;

                        MessageBox.Show(
                            Resources.FileIntegrityCheckFailedMessage,
                            Resources.FileIntegrityCheckFailedCaption,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (AutoUpdater.ReportErrors)
                        {
                            MessageBox.Show(
                                Resources.HashAlgorithmNotSupportedMessage,
                                Resources.HashAlgorithmNotSupportedCaption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }

                    return false;
                }
            }
        }

        private static string TryToFindFileName(string contentDisposition, string lookForFileName)
        {
            var fileName = String.Empty;
            if (!string.IsNullOrEmpty(contentDisposition))
            {
                var index = contentDisposition.IndexOf(lookForFileName, StringComparison.CurrentCultureIgnoreCase);
                if (index >= 0)
                    fileName = contentDisposition.Substring(index + lookForFileName.Length);
                if (fileName.StartsWith("\""))
                {
                    var file = fileName.Substring(1, fileName.Length - 1);
                    var i = file.IndexOf("\"", StringComparison.CurrentCultureIgnoreCase);
                    if (i != -1)
                    {
                        fileName = file.Substring(0, i);
                    }
                }
            }

            return fileName;
        }

        private void DownloadUpdateDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._webClient == null)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else if (this._webClient.IsBusy)
            {
                this._webClient.CancelAsync();
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void DownloadUpdateDialogLoad(object sender, EventArgs e)
        {
            this._webClient = new MyWebClient
                                  {
                                      CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore)
                                  };

            if (AutoUpdater.Proxy != null)
            {
                this._webClient.Proxy = AutoUpdater.Proxy;
            }

            var uri = new Uri(this._downloadURL);

            if (string.IsNullOrEmpty(AutoUpdater.DownloadPath))
            {
                this._tempFile = Path.GetTempFileName();
            }
            else
            {
                this._tempFile = Path.Combine(AutoUpdater.DownloadPath, $"{Guid.NewGuid().ToString()}.tmp");
                if (!Directory.Exists(AutoUpdater.DownloadPath))
                {
                    Directory.CreateDirectory(AutoUpdater.DownloadPath);
                }
            }

            this._webClient.DownloadProgressChanged += this.OnDownloadProgressChanged;

            this._webClient.DownloadFileCompleted += this.WebClientOnDownloadFileCompleted;

            this._webClient.DownloadFileAsync(uri, this._tempFile);
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (this._startedAt == default(DateTime))
            {
                this._startedAt = DateTime.Now;
            }
            else
            {
                var timeSpan = DateTime.Now - this._startedAt;
                var totalSeconds = (long)timeSpan.TotalSeconds;
                if (totalSeconds > 0)
                {
                    var bytesPerSecond = e.BytesReceived / totalSeconds;
                    labelInformation.Text = string.Format(
                        Resources.DownloadSpeedMessage,
                        BytesToString(bytesPerSecond));
                }
            }

            labelSize.Text = $@"{BytesToString(e.BytesReceived)} / {BytesToString(e.TotalBytesToReceive)}";
            progressBar.Value = e.ProgressPercentage;
        }

        private void WebClientOnDownloadFileCompleted(object sender, AsyncCompletedEventArgs asyncCompletedEventArgs)
        {
            if (asyncCompletedEventArgs.Cancelled)
            {
                return;
            }

            if (asyncCompletedEventArgs.Error != null)
            {
                MessageBox.Show(
                    asyncCompletedEventArgs.Error.Message,
                    asyncCompletedEventArgs.Error.GetType().ToString(),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this._webClient = null;
                this.Close();
                return;
            }

            if (!string.IsNullOrEmpty(AutoUpdater.Checksum))
            {
                if (!CompareChecksum(this._tempFile, AutoUpdater.Checksum))
                {
                    this._webClient = null;
                    this.Close();
                    return;
                }
            }

            string fileName;
            var contentDisposition = this._webClient.ResponseHeaders["Content-Disposition"] ?? string.Empty;
            if (string.IsNullOrEmpty(contentDisposition))
            {
                fileName = Path.GetFileName(this._webClient.ResponseUri.LocalPath);
            }
            else
            {
                fileName = TryToFindFileName(contentDisposition, "filename=");
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = TryToFindFileName(contentDisposition, "filename*=UTF-8''");
                }
            }

            var tempPath = Path.Combine(
                string.IsNullOrEmpty(AutoUpdater.DownloadPath) ? Path.GetTempPath() : AutoUpdater.DownloadPath,
                fileName);

            try
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }

                File.Move(this._tempFile, tempPath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, e.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this._webClient = null;
                this.Close();
                return;
            }

            var processStartInfo = new ProcessStartInfo
                                       {
                                           FileName = tempPath,
                                           UseShellExecute = true,
                                           Arguments = AutoUpdater.InstallerArgs.Replace(
                                               "%path%",
                                               Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName))
                                       };

            var extension = Path.GetExtension(tempPath);
            if (extension.Equals(".zip", StringComparison.OrdinalIgnoreCase))
            {
                var installerPath = Path.Combine(Path.GetDirectoryName(tempPath), "ZipExtractor.exe");
                File.WriteAllBytes(installerPath, Resources.ZipExtractor);
                var arguments = new StringBuilder(
                    $"\"{tempPath}\" \"{Process.GetCurrentProcess().MainModule.FileName}\"");
                var args = Environment.GetCommandLineArgs();
                for (var i = 1; i < args.Length; i++)
                {
                    if (i.Equals(1))
                    {
                        arguments.Append(" \"");
                    }

                    arguments.Append(args[i]);
                    arguments.Append(i.Equals(args.Length - 1) ? "\"" : " ");
                }

                processStartInfo = new ProcessStartInfo
                                       {
                                           FileName = installerPath,
                                           UseShellExecute = true,
                                           Arguments = arguments.ToString()
                                       };
            }
            else if (extension.Equals(".msi", StringComparison.OrdinalIgnoreCase))
            {
                processStartInfo = new ProcessStartInfo { FileName = "msiexec", Arguments = $"/i \"{tempPath}\"" };
                if (!string.IsNullOrEmpty(AutoUpdater.InstallerArgs))
                {
                    processStartInfo.Arguments += " " + AutoUpdater.InstallerArgs;
                }
            }

            if (AutoUpdater.RunUpdateAsAdmin)
            {
                processStartInfo.Verb = "runas";
            }

            try
            {
                Process.Start(processStartInfo);
            }
            catch (Win32Exception exception)
            {
                this._webClient = null;
                if (exception.NativeErrorCode != 1223)
                    throw;
            }

            this.Close();
        }
    }

    /// <inheritdoc />
    public class MyWebClient : WebClient
    {
        /// <summary>
        ///     Response Uri after any redirects.
        /// </summary>
        public Uri ResponseUri;

        /// <inheritdoc />
        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            var webResponse = base.GetWebResponse(request, result);
            this.ResponseUri = webResponse.ResponseUri;
            return webResponse;
        }
    }
}