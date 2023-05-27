using System;
using System.Collections.Generic;
using System.Windows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.IO;
using System.Diagnostics;
using Path = System.IO.Path;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace Image_Gallery_Application
{
    /// <summary>
    /// Interaction logic for UploadWindow.xaml
    /// </summary>
    public partial class UploadWindow : Window
    {
        

        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "Image Project";
        Queue imageList = new Queue(10);
        int i = 1;
        public UploadWindow()
        {
            InitializeComponent();
            upload.Visibility = Visibility.Hidden;
            list.Visibility = Visibility.Hidden;
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private static string CreateFolder(string folderName, DriveService service)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };
            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            Console.WriteLine("Folder ID: " + file.Id);
            return file.Id;
        }
        private static void DeleteFileFolder(string id, DriveService service)
        {
            var request = service.Files.Delete(id);
            request.Execute();
        }

        public static void FileUploadInFolder(string folderId, string path, DriveService service)
        {
            var FileMetaData = new Google.Apis.Drive.v3.Data.File()
            {

                Name = Path.GetFileName(path),
                MimeType = "image/jpeg",
                //id of parent folder 
                Parents = new List<string>
                {
                    folderId
                }
            };
            FilesResource.CreateMediaUpload request;
            //create stream and upload
            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                request.Fields = "id";
                request.Upload();
            }
            var file1 = request.ResponseBody;
        }


        private static void UploadBasicImage(string path, DriveService service)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = Path.GetFileName(path);
            fileMetadata.MimeType = "image/jpeg";
            FilesResource.CreateMediaUpload request;




            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate))
            {

                request = service.Files.Create(fileMetadata, stream, "image/jpeg");
                request.Fields = "id";
                request.Upload();
            }

            var file = request.ResponseBody;

            Console.WriteLine("File ID: " + file.Id);

        }

        private static UserCredential GetCredentials()
        {
            UserCredential credential;

            using (var stream = new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            return credential;
        }




        

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            UserCredential credential;

            credential = GetCredentials();

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            string id = CreateFolder("MyImages", service);
            //UploadBasicImage(imageList.GetFirst(), service);
            int sizeList = imageList.size;
            imageList.print();
            for (int i = 0; i < sizeList; i++)
            {
                //UploadBasicImage(imageList.GetFirst(),service);
                FileUploadInFolder(id, imageList.deQueue(), service);
            }
            Debug.WriteLine("Done");
        }
        
        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.ShowDialog();
            
            list.Visibility = Visibility.Visible;
            foreach (string item in ofd.FileNames)
            {
                if (i <= 10)
                {
                    imageList.enQueue(item);
                    list.Items.Add(item);
                    i++;
                } 
            }
            upload.Visibility = Visibility.Visible;
        }

        private void return_Click(object sender, RoutedEventArgs e)
        {
            ImageGalleryFirstWindow imageGalleryFirstWindow = new ImageGalleryFirstWindow();
            imageGalleryFirstWindow.Show();
            this.Hide();
        }
    }
}
